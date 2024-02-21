using Maui.AppStores.Internal;

// ReSharper disable once CheckNamespace
namespace Maui.AppStores;

/// <inheritdoc />
internal sealed class AppStoreInfoImplementation : IAppStoreInfo
{
    /// <inheritdoc />
    public AppStoreInformation? CachedInformation { get; set; }

    /// <inheritdoc />
    public async Task<AppStoreInformation> GetInformationAsync(
        CancellationToken cancellationToken = default)
    {
        using var client = new HttpClient();
        var lookup = await client.LookupAsync(
            bundleIdentifier: AppStoreInfo.Options.PackageName,
            countryCode: AppStoreInfo.Options.CountryCode,
            cancellationToken: cancellationToken).ConfigureAwait(false);
            
        var result =
            lookup.FirstOrDefault() ??
            throw new InvalidOperationException(
               $"No lookup result found for bundle identifier '{AppStoreInfo.Options.PackageName}' " +
               $"and country code '{AppStoreInfo.Options.CountryCode}'.");
        
        return CachedInformation ??= new AppStoreInformation
        {
            Title = string.Empty,
            Description = result.Description,
            ReleaseNotes = result.ReleaseNotes,
            ApplicationSizeInBytes = long.TryParse(result.FileSizeBytes, out var size)
                ? size
                : 0L,
            LatestVersion = Version.Parse(result.Version),
            InternalStoreUri = new Uri(result.TrackViewUrl),
            ExternalStoreUri = new Uri(result.TrackViewUrl),
        };
    }
}