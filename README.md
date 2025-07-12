# Jellyfin Kinopoisk Metadata Provider

A comprehensive Jellyfin plugin that fetches metadata from the Kinopoisk API for movies and TV shows, providing rich Russian language metadata including ratings, cast, crew, plot information, and images.

## Features

- 🎬 **Movie and TV Series Support**: Complete metadata for both movies and TV shows
- 🇷🇺 **Russian Metadata**: Native support for Russian titles, descriptions, and cast information
- ⭐ **Ratings Integration**: Kinopoisk and IMDb ratings
- 🖼️ **Image Support**: Automatic poster and backdrop downloads
- 👥 **Cast & Crew**: Detailed information about actors, directors, writers, and other crew
- 🎭 **Genre & Country**: Proper genre and production country mapping
- 🔧 **Configurable**: Extensive configuration options through Jellyfin dashboard
- 📝 **Logging**: Debug logging support for troubleshooting

## Installation

### From Release

1. Download the latest `Jellyfin.Plugin.KinopoiskDev.dll` from releases
2. Copy the DLL to your Jellyfin plugins directory:
   - Linux: `/var/lib/jellyfin/plugins/`
   - Windows: `C:\ProgramData\Jellyfin\Server\plugins\`
   - Docker: `/config/plugins/`
3. Restart your Jellyfin server
4. Go to Dashboard → Plugins → Kinopoisk Metadata Provider to configure

### Build from Source

```bash
git clone <repository-url>
cd Jellyfin.Plugin.KinopoiskDev.Proper
dotnet build --configuration Release
```

The compiled plugin will be in `bin/Release/net8.0/Jellyfin.Plugin.KinopoiskDev.dll`

## Configuration

The plugin comes pre-configured with a default API key: `XN7WBQA-MF24EYN-Q4SWZHF-7AR2715`

### Configuration Options

| Setting | Description | Default |
|---------|-------------|---------|
| **API Key** | Your Kinopoisk API key | `XN7WBQA-MF24EYN-Q4SWZHF-7AR2715` |
| **API URL** | Base URL for Kinopoisk API | `https://api.kinopoisk.dev/v1.4` |
| **Request Timeout** | API request timeout in seconds | `30` |
| **Max Search Results** | Maximum search results to return | `10` |
| **Prefer Russian Metadata** | Prefer Russian titles over English | `true` |
| **Enable Debug Logging** | Enable detailed logging | `false` |

### Getting Your Own API Key

1. Visit [api.kinopoisk.dev](https://api.kinopoisk.dev/)
2. Register for a free account
3. Generate your API key
4. Replace the default key in plugin configuration

## Usage

### Automatic Metadata Fetching

1. **Enable the Provider**: Go to Dashboard → Libraries → [Your Library] → Metadata
2. **Configure Priority**: Set "Kinopoisk" as your preferred metadata provider
3. **Enable Image Downloads**: Check "Kinopoisk" in image providers section
4. **Scan Library**: The plugin will automatically fetch metadata during library scans

### Manual Identification

1. Right-click on a movie/show → "Identify"
2. Search for the title or enter Kinopoisk ID
3. Select the correct match from search results

### Supported Metadata

#### Movies
- Title (Russian/English)
- Plot/Overview
- Release Date
- Runtime
- Genres
- Countries
- Cast & Crew
- Ratings (Kinopoisk/IMDb)
- MPAA Rating
- Posters & Backdrops

#### TV Series
- Title (Russian/English)
- Plot/Overview
- Air Dates (Start/End)
- Status (Ongoing/Completed/Cancelled)
- Episode Runtime
- Genres
- Countries
- Cast & Crew
- Ratings (Kinopoisk/IMDb)
- Posters & Backdrops

## API Integration

This plugin uses the official [Kinopoisk API v1.4](https://api.kinopoisk.dev/documentation) which provides:

- Comprehensive movie and TV show database
- High-quality Russian language metadata
- Professional cast and crew information
- Multiple image types and resolutions
- Cross-references with IMDb

## Troubleshooting

### Enable Debug Logging

1. Go to plugin configuration
2. Enable "Debug Logging"
3. Check Jellyfin logs for detailed API interactions

### Common Issues

**No metadata found:**
- Verify API key is correct
- Check if movie/show exists on Kinopoisk
- Try searching with Russian title
- Enable debug logging to see API requests

**Images not downloading:**
- Ensure image providers are enabled in library settings
- Check that Kinopoisk is selected as image provider
- Verify API responses contain image URLs

**Slow performance:**
- Increase request timeout if needed
- Reduce max search results
- Check network connectivity to API

### Log Examples

```
[DBG] Searching Kinopoisk API: https://api.kinopoisk.dev/v1.4/movie/search?page=1&limit=10&query=Inception
[DBG] Found 5 movies for query 'Inception'
[DBG] Retrieved movie: Начало (2010)
```

## Development

### Project Structure

```
Jellyfin.Plugin.KinopoiskDev.Proper/
├── ApiModels/           # API response models
├── Configuration/       # Plugin configuration
├── ExternalIds/        # External ID mappings
├── Providers/          # Metadata providers
├── Services/           # API client services
├── Plugin.cs           # Main plugin class
└── README.md
```

### Building

```bash
dotnet restore
dotnet build --configuration Release
```

### Testing

Test the plugin with various movie and TV show titles:

```bash
# Russian titles
curl -H "X-API-KEY: YOUR_KEY" "https://api.kinopoisk.dev/v1.4/movie/search?query=Брат"

# English titles  
curl -H "X-API-KEY: YOUR_KEY" "https://api.kinopoisk.dev/v1.4/movie/search?query=Brother"
```

## Requirements

- .NET 8.0 or later
- Jellyfin 10.9.0 or later
- Internet connection for API access
- Valid Kinopoisk API key

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## License

This project is open source and available under standard licensing terms.

## Credits

- Built using the official [Jellyfin Plugin Template](https://github.com/jellyfin/jellyfin-plugin-template)
- Integrates with [Kinopoisk API](https://api.kinopoisk.dev/)
- Thanks to the Jellyfin community for plugin development resources

## Version History

### 1.0.0
- Initial release
- Movie and TV series metadata support
- Image provider implementation
- Configurable Russian/English metadata preference
- Debug logging support
- Pre-configured with working API key
