using MediaBrowser.Model.Plugins;

namespace Jellyfin.Plugin.KinopoiskDev.Configuration;

/// <summary>
/// Plugin configuration.
/// </summary>
public class PluginConfiguration : BasePluginConfiguration
{
    /// <summary>
    /// Gets or sets the API key.
    /// </summary>
    public string ApiKey { get; set; } = "XN7WBQA-MF24EYN-Q4SWZHF-7AR2715";

    /// <summary>
    /// Gets or sets the API base URL.
    /// </summary>
    public string ApiUrl { get; set; } = "https://api.kinopoisk.dev/v1.4";

    /// <summary>
    /// Gets or sets the request timeout in seconds.
    /// </summary>
    public int RequestTimeoutSeconds { get; set; } = 30;

    /// <summary>
    /// Gets or sets a value indicating whether to enable debug logging.
    /// </summary>
    public bool EnableDebugLogging { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether to prefer Russian metadata.
    /// </summary>
    public bool PreferRussianMetadata { get; set; } = true;

    /// <summary>
    /// Gets or sets the maximum number of search results to return.
    /// </summary>
    public int MaxSearchResults { get; set; } = 10;
}
