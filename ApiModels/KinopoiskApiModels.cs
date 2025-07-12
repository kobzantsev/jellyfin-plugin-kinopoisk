using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Jellyfin.Plugin.KinopoiskDev.ApiModels;

/// <summary>
/// Represents a response from the Kinopoisk API for movie searches.
/// </summary>
public class KinopoiskSearchResponse
{
    /// <summary>
    /// Gets or sets the list of movies.
    /// </summary>
    [JsonPropertyName("docs")]
    public List<KinopoiskMovie> Movies { get; set; } = new();

    /// <summary>
    /// Gets or sets the total number of results.
    /// </summary>
    [JsonPropertyName("total")]
    public int Total { get; set; }

    /// <summary>
    /// Gets or sets the current page.
    /// </summary>
    [JsonPropertyName("page")]
    public int Page { get; set; }

    /// <summary>
    /// Gets or sets the number of results per page.
    /// </summary>
    [JsonPropertyName("limit")]
    public int Limit { get; set; }

    /// <summary>
    /// Gets or sets the total number of pages.
    /// </summary>
    [JsonPropertyName("pages")]
    public int Pages { get; set; }
}

/// <summary>
/// Represents a movie from the Kinopoisk API.
/// </summary>
public class KinopoiskMovie
{
    /// <summary>
    /// Gets or sets the movie ID.
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the Russian name.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the alternative (English) name.
    /// </summary>
    [JsonPropertyName("alternativeName")]
    public string? AlternativeName { get; set; }

    /// <summary>
    /// Gets or sets the type (movie, tv-series, etc.).
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Gets or sets the release year.
    /// </summary>
    [JsonPropertyName("year")]
    public int? Year { get; set; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the short description.
    /// </summary>
    [JsonPropertyName("shortDescription")]
    public string? ShortDescription { get; set; }

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Gets or sets the rating information.
    /// </summary>
    [JsonPropertyName("rating")]
    public KinopoiskRating? Rating { get; set; }

    /// <summary>
    /// Gets or sets the vote counts.
    /// </summary>
    [JsonPropertyName("votes")]
    public KinopoiskVotes? Votes { get; set; }

    /// <summary>
    /// Gets or sets the movie length in minutes.
    /// </summary>
    [JsonPropertyName("movieLength")]
    public int? MovieLength { get; set; }

    /// <summary>
    /// Gets or sets the series length in minutes.
    /// </summary>
    [JsonPropertyName("seriesLength")]
    public int? SeriesLength { get; set; }

    /// <summary>
    /// Gets or sets the total series length in minutes.
    /// </summary>
    [JsonPropertyName("totalSeriesLength")]
    public int? TotalSeriesLength { get; set; }

    /// <summary>
    /// Gets or sets the MPAA rating.
    /// </summary>
    [JsonPropertyName("ratingMpaa")]
    public string? RatingMpaa { get; set; }

    /// <summary>
    /// Gets or sets the age rating.
    /// </summary>
    [JsonPropertyName("ageRating")]
    public int? AgeRating { get; set; }

    /// <summary>
    /// Gets or sets the genres.
    /// </summary>
    [JsonPropertyName("genres")]
    public List<KinopoiskGenre> Genres { get; set; } = new();

    /// <summary>
    /// Gets or sets the countries.
    /// </summary>
    [JsonPropertyName("countries")]
    public List<KinopoiskCountry> Countries { get; set; } = new();

    /// <summary>
    /// Gets or sets the release years range.
    /// </summary>
    [JsonPropertyName("releaseYears")]
    public List<KinopoiskReleaseYear> ReleaseYears { get; set; } = new();

    /// <summary>
    /// Gets or sets the poster information.
    /// </summary>
    [JsonPropertyName("poster")]
    public KinopoiskPoster? Poster { get; set; }

    /// <summary>
    /// Gets or sets the backdrop information.
    /// </summary>
    [JsonPropertyName("backdrop")]
    public KinopoiskBackdrop? Backdrop { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this is a series.
    /// </summary>
    [JsonPropertyName("isSeries")]
    public bool IsSeries { get; set; }

    /// <summary>
    /// Gets or sets the persons (cast and crew).
    /// </summary>
    [JsonPropertyName("persons")]
    public List<KinopoiskPerson> Persons { get; set; } = new();
}

/// <summary>
/// Represents rating information.
/// </summary>
public class KinopoiskRating
{
    /// <summary>
    /// Gets or sets the Kinopoisk rating.
    /// </summary>
    [JsonPropertyName("kp")]
    public double Kp { get; set; }

    /// <summary>
    /// Gets or sets the IMDb rating.
    /// </summary>
    [JsonPropertyName("imdb")]
    public double Imdb { get; set; }

    /// <summary>
    /// Gets or sets the film critics rating.
    /// </summary>
    [JsonPropertyName("filmCritics")]
    public double FilmCritics { get; set; }

    /// <summary>
    /// Gets or sets the Russian film critics rating.
    /// </summary>
    [JsonPropertyName("russianFilmCritics")]
    public double RussianFilmCritics { get; set; }
}

/// <summary>
/// Represents vote counts.
/// </summary>
public class KinopoiskVotes
{
    /// <summary>
    /// Gets or sets the Kinopoisk votes.
    /// </summary>
    [JsonPropertyName("kp")]
    public int Kp { get; set; }

    /// <summary>
    /// Gets or sets the IMDb votes.
    /// </summary>
    [JsonPropertyName("imdb")]
    public int Imdb { get; set; }
}

/// <summary>
/// Represents a genre.
/// </summary>
public class KinopoiskGenre
{
    /// <summary>
    /// Gets or sets the genre name.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}

/// <summary>
/// Represents a country.
/// </summary>
public class KinopoiskCountry
{
    /// <summary>
    /// Gets or sets the country name.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}

/// <summary>
/// Represents a release year range.
/// </summary>
public class KinopoiskReleaseYear
{
    /// <summary>
    /// Gets or sets the start year.
    /// </summary>
    [JsonPropertyName("start")]
    public int Start { get; set; }

    /// <summary>
    /// Gets or sets the end year.
    /// </summary>
    [JsonPropertyName("end")]
    public int? End { get; set; }
}

/// <summary>
/// Represents poster information.
/// </summary>
public class KinopoiskPoster
{
    /// <summary>
    /// Gets or sets the poster URL.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>
    /// Gets or sets the preview URL.
    /// </summary>
    [JsonPropertyName("previewUrl")]
    public string? PreviewUrl { get; set; }
}

/// <summary>
/// Represents backdrop information.
/// </summary>
public class KinopoiskBackdrop
{
    /// <summary>
    /// Gets or sets the backdrop URL.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>
    /// Gets or sets the preview URL.
    /// </summary>
    [JsonPropertyName("previewUrl")]
    public string? PreviewUrl { get; set; }
}

/// <summary>
/// Represents a person (cast or crew member).
/// </summary>
public class KinopoiskPerson
{
    /// <summary>
    /// Gets or sets the person ID.
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the person name.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the person's English name.
    /// </summary>
    [JsonPropertyName("enName")]
    public string? EnName { get; set; }

    /// <summary>
    /// Gets or sets the person's profession.
    /// </summary>
    [JsonPropertyName("profession")]
    public string? Profession { get; set; }

    /// <summary>
    /// Gets or sets the person's character name (for actors).
    /// </summary>
    [JsonPropertyName("enProfession")]
    public string? EnProfession { get; set; }

    /// <summary>
    /// Gets or sets the person's description.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the person's photo.
    /// </summary>
    [JsonPropertyName("photo")]
    public string? Photo { get; set; }
}
