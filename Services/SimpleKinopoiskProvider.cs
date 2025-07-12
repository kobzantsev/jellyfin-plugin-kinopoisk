using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Jellyfin.Plugin.KinopoiskDev.ApiModels;
using Jellyfin.Plugin.KinopoiskDev.Configuration;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Entities.Movies;
using MediaBrowser.Controller.Entities.TV;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.Providers;
using Microsoft.Extensions.Logging;

namespace Jellyfin.Plugin.KinopoiskDev.Services
{
    /// <summary>
    /// Simple movie metadata provider for Kinopoisk API.
    /// </summary>
    public class SimpleKinopoiskMovieProvider : IRemoteMetadataProvider<Movie, MovieInfo>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<SimpleKinopoiskMovieProvider> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleKinopoiskMovieProvider"/> class.
        /// </summary>
        /// <param name="httpClientFactory">The HTTP client factory.</param>
        /// <param name="logger">The logger.</param>
        public SimpleKinopoiskMovieProvider(IHttpClientFactory httpClientFactory, ILogger<SimpleKinopoiskMovieProvider> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        /// <inheritdoc />
        public string Name => "Kinopoisk";

        /// <inheritdoc />
        public async Task<IEnumerable<RemoteSearchResult>> GetSearchResults(MovieInfo searchInfo, CancellationToken cancellationToken)
        {
            var results = new List<RemoteSearchResult>();

            if (string.IsNullOrEmpty(searchInfo.Name))
            {
                return results;
            }

            var config = Plugin.Instance?.Configuration ?? new PluginConfiguration();
            
            try
            {
                using var httpClient = _httpClientFactory.CreateClient();
                var query = Uri.EscapeDataString(searchInfo.Name);
                var url = $"{config.ApiUrl}/movie/search?page=1&limit={config.MaxSearchResults}&query={query}";
                
                if (searchInfo.Year.HasValue)
                {
                    url += $"&year={searchInfo.Year.Value}";
                }

                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Add("X-API-KEY", config.ApiKey);

                var response = await httpClient.GetAsync(url, cancellationToken).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
                var searchResponse = JsonSerializer.Deserialize<KinopoiskSearchResponse>(content);

                if (searchResponse?.Movies != null)
                {
                    foreach (var movie in searchResponse.Movies.Where(m => m.Type == "movie"))
                    {
                        var result = new RemoteSearchResult
                        {
                            Name = GetPreferredTitle(movie, config),
                            ProductionYear = movie.Year,
                            Overview = movie.Description ?? movie.ShortDescription
                        };

                        result.SetProviderId(Name, movie.Id.ToString(CultureInfo.InvariantCulture));

                        if (movie.Poster?.Url != null)
                        {
                            result.ImageUrl = movie.Poster.Url;
                        }

                        results.Add(result);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching for movie: {Name}", searchInfo.Name);
            }

            return results;
        }

        /// <inheritdoc />
        public async Task<MetadataResult<Movie>> GetMetadata(MovieInfo info, CancellationToken cancellationToken)
        {
            var result = new MetadataResult<Movie>();

            var kinopoiskId = info.GetProviderId(Name);
            if (string.IsNullOrEmpty(kinopoiskId))
            {
                var searchResults = await GetSearchResults(info, cancellationToken).ConfigureAwait(false);
                var firstResult = searchResults.FirstOrDefault();
                if (firstResult != null)
                {
                    kinopoiskId = firstResult.GetProviderId(Name);
                }
            }

            if (string.IsNullOrEmpty(kinopoiskId) || !int.TryParse(kinopoiskId, out var id))
            {
                return result;
            }

            var config = Plugin.Instance?.Configuration ?? new PluginConfiguration();

            try
            {
                using var httpClient = _httpClientFactory.CreateClient();
                var url = $"{config.ApiUrl}/movie/{id}";

                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Add("X-API-KEY", config.ApiKey);

                var response = await httpClient.GetAsync(url, cancellationToken).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
                var movie = JsonSerializer.Deserialize<KinopoiskMovie>(content);

                if (movie != null)
                {
                    result.Item = new Movie
                    {
                        Name = GetPreferredTitle(movie, config),
                        OriginalTitle = movie.AlternativeName,
                        Overview = movie.Description ?? movie.ShortDescription,
                        ProductionYear = movie.Year,
                        PremiereDate = movie.Year.HasValue ? new DateTime(movie.Year.Value, 1, 1) : null,
                        RunTimeTicks = movie.MovieLength.HasValue ? TimeSpan.FromMinutes(movie.MovieLength.Value).Ticks : null,
                        OfficialRating = movie.RatingMpaa,
                        CommunityRating = movie.Rating?.Kp > 0 ? (float?)movie.Rating.Kp : null
                    };

                    result.Item.SetProviderId(Name, movie.Id.ToString(CultureInfo.InvariantCulture));

                    if (movie.Rating?.Imdb > 0)
                    {
                        result.Item.SetProviderId(MetadataProvider.Imdb, movie.Rating.Imdb.ToString(CultureInfo.InvariantCulture));
                    }

                    if (movie.Genres?.Count > 0)
                    {
                        result.Item.Genres = movie.Genres.Select(g => g.Name).ToArray();
                    }

                    if (movie.Countries?.Count > 0)
                    {
                        result.Item.ProductionLocations = movie.Countries.Select(c => c.Name).ToArray();
                    }

                    result.HasMetadata = true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting metadata for movie ID: {Id}", id);
            }

            return result;
        }

        /// <inheritdoc />
        public Task<HttpResponseMessage> GetImageResponse(string url, CancellationToken cancellationToken)
        {
            using var httpClient = _httpClientFactory.CreateClient();
            return httpClient.GetAsync(url, cancellationToken);
        }

        private static string GetPreferredTitle(KinopoiskMovie movie, PluginConfiguration config)
        {
            if (config.PreferRussianMetadata)
            {
                return !string.IsNullOrEmpty(movie.Name) ? movie.Name : movie.AlternativeName ?? "Unknown";
            }
            
            return !string.IsNullOrEmpty(movie.AlternativeName) ? movie.AlternativeName : movie.Name ?? "Unknown";
        }
    }

    /// <summary>
    /// Simple image provider for Kinopoisk API.
    /// </summary>
    public class SimpleKinopoiskImageProvider : IRemoteImageProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleKinopoiskImageProvider"/> class.
        /// </summary>
        /// <param name="httpClientFactory">The HTTP client factory.</param>
        public SimpleKinopoiskImageProvider(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <inheritdoc />
        public string Name => "Kinopoisk";

        /// <inheritdoc />
        public bool Supports(BaseItem item)
        {
            return item is Movie or Series;
        }

        /// <inheritdoc />
        public IEnumerable<ImageType> GetSupportedImages(BaseItem item)
        {
            return new List<ImageType> { ImageType.Primary, ImageType.Backdrop };
        }

        /// <inheritdoc />
        public async Task<IEnumerable<RemoteImageInfo>> GetImages(BaseItem item, CancellationToken cancellationToken)
        {
            var images = new List<RemoteImageInfo>();
            var kinopoiskId = item.GetProviderId("Kinopoisk");
            
            if (string.IsNullOrEmpty(kinopoiskId) || !int.TryParse(kinopoiskId, out var id))
            {
                return images;
            }

            var config = Plugin.Instance?.Configuration ?? new PluginConfiguration();

            try
            {
                using var httpClient = _httpClientFactory.CreateClient();
                var url = $"{config.ApiUrl}/movie/{id}";

                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Add("X-API-KEY", config.ApiKey);

                var response = await httpClient.GetAsync(url, cancellationToken).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
                var movie = JsonSerializer.Deserialize<KinopoiskMovie>(content);

                if (movie != null)
                {
                    if (!string.IsNullOrEmpty(movie.Poster?.Url))
                    {
                        images.Add(new RemoteImageInfo
                        {
                            ProviderName = Name,
                            Type = ImageType.Primary,
                            Url = movie.Poster.Url
                        });
                    }

                    if (!string.IsNullOrEmpty(movie.Backdrop?.Url))
                    {
                        images.Add(new RemoteImageInfo
                        {
                            ProviderName = Name,
                            Type = ImageType.Backdrop,
                            Url = movie.Backdrop.Url
                        });
                    }
                }
            }
            catch
            {
                // Ignore errors
            }

            return images;
        }

        /// <inheritdoc />
        public Task<HttpResponseMessage> GetImageResponse(string url, CancellationToken cancellationToken)
        {
            using var httpClient = _httpClientFactory.CreateClient();
            return httpClient.GetAsync(url, cancellationToken);
        }
    }
}
