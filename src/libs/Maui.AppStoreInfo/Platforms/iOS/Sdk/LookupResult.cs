using System.Text.Json.Serialization;

// ReSharper disable once CheckNamespace
namespace Maui.AppStores.Internal;

internal sealed class LookupResult
{
    [JsonPropertyName("features")]
    public List<string> Features { get; set; } = [];

    [JsonPropertyName("supportedDevices")]
    public List<string> SupportedDevices { get; set; } = [];

    [JsonPropertyName("isGameCenterEnabled")]
    public bool IsGameCenterEnabled { get; set; }

    [JsonPropertyName("advisories")]
    public List<object> Advisories { get; set; } = [];

    [JsonPropertyName("appletvScreenshotUrls")]
    public List<object> AppleTvScreenshotUrls { get; set; } = [];

    [JsonPropertyName("screenshotUrls")]
    public List<string> ScreenshotUrls { get; set; } = [];

    [JsonPropertyName("ipadScreenshotUrls")]
    public List<string> IpadScreenshotUrls { get; set; } = [];

    [JsonPropertyName("artworkUrl60")]
    public string ArtworkUrl60 { get; set; } = string.Empty;

    [JsonPropertyName("artworkUrl512")]
    public string ArtworkUrl512 { get; set; } = string.Empty;

    [JsonPropertyName("artworkUrl100")]
    public string ArtworkUrl100 { get; set; } = string.Empty;

    [JsonPropertyName("artistViewUrl")]
    public string ArtistViewUrl { get; set; } = string.Empty;

    [JsonPropertyName("kind")]
    public string Kind { get; set; } = string.Empty;

    [JsonPropertyName("currency")]
    public string Currency { get; set; } = string.Empty;

    [JsonPropertyName("currentVersionReleaseDate")]
    public DateTime CurrentVersionReleaseDate { get; set; }

    [JsonPropertyName("releaseNotes")]
    public string ReleaseNotes { get; set; } = string.Empty;

    [JsonPropertyName("minimumOsVersion")]
    public string MinimumOsVersion { get; set; } = string.Empty;

    [JsonPropertyName("artistId")]
    public int ArtistId { get; set; }

    [JsonPropertyName("artistName")]
    public string ArtistName { get; set; } = string.Empty;

    [JsonPropertyName("genres")]
    public List<string> Genres { get; set; } = [];

    [JsonPropertyName("price")]
    public double Price { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("sellerName")]
    public string SellerName { get; set; } = string.Empty;

    [JsonPropertyName("primaryGenreName")]
    public string PrimaryGenreName { get; set; } = string.Empty;

    [JsonPropertyName("primaryGenreId")]
    public int PrimaryGenreId { get; set; }

    [JsonPropertyName("releaseDate")]
    public DateTime ReleaseDate { get; set; }

    [JsonPropertyName("bundleId")]
    public string BundleId { get; set; } = string.Empty;

    [JsonPropertyName("trackId")]
    public long TrackId { get; set; }

    [JsonPropertyName("trackName")]
    public string TrackName { get; set; } = string.Empty;

    [JsonPropertyName("genreIds")]
    public List<string> GenreIds { get; set; } = [];

    [JsonPropertyName("averageUserRatingForCurrentVersion")]
    public int AverageUserRatingForCurrentVersion { get; set; }

    [JsonPropertyName("averageUserRating")]
    public int AverageUserRating { get; set; }

    [JsonPropertyName("trackCensoredName")]
    public string TrackCensoredName { get; set; } = string.Empty;

    [JsonPropertyName("languageCodesISO2A")]
    public List<string> LanguageCodesIso2A { get; set; } = [];

    [JsonPropertyName("fileSizeBytes")]
    public string FileSizeBytes { get; set; } = string.Empty;

    [JsonPropertyName("sellerUrl")]
    public string SellerUrl { get; set; } = string.Empty;

    [JsonPropertyName("formattedPrice")]
    public string FormattedPrice { get; set; } = string.Empty;

    [JsonPropertyName("contentAdvisoryRating")]
    public string ContentAdvisoryRating { get; set; } = string.Empty;

    [JsonPropertyName("userRatingCountForCurrentVersion")]
    public int UserRatingCountForCurrentVersion { get; set; }

    [JsonPropertyName("trackViewUrl")]
    public string TrackViewUrl { get; set; } = string.Empty;

    [JsonPropertyName("trackContentRating")]
    public string TrackContentRating { get; set; } = string.Empty;

    [JsonPropertyName("isVppDeviceBasedLicensingEnabled")]
    public bool IsVppDeviceBasedLicensingEnabled { get; set; }

    [JsonPropertyName("version")]
    public string Version { get; set; } = string.Empty;

    [JsonPropertyName("wrapperType")]
    public string WrapperType { get; set; } = string.Empty;

    [JsonPropertyName("userRatingCount")]
    public int UserRatingCount { get; set; }
}