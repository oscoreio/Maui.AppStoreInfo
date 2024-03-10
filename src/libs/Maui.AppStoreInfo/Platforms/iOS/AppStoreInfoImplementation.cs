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
        using var client = AppStoreInfo.Options.HttpClientFactory();
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
#if __IOS__
            InternalStoreUri = new Uri($"itms-apps://itunes.apple.com/app/id{AppStoreInfo.Options.PackageName}"),
#elif __TVOS__
			InternalStoreUri = new Uri($"com.apple.TVAppStore://itunes.apple.com/app/id{AppStoreInfo.Options.PackageName}"),
#elif __MACOS__
			InternalStoreUri = new Uri($"macappstore://itunes.apple.com/app/id{AppStoreInfo.Options.PackageName}?mt=12"),
#endif
#if __IOS__
            InternalReviewUri = new Uri($"itms-apps://itunes.apple.com/app/id{AppStoreInfo.Options.PackageName}?action=write-review"),
#elif __TVOS__
			InternalReviewUri = new Uri($"com.apple.TVAppStore://itunes.apple.com/app/id{AppStoreInfo.Options.PackageName}?action=write-review"),
#elif __MACOS__
			InternalReviewUri = new Uri($"macappstore://itunes.apple.com/app/id{AppStoreInfo.Options.PackageName}?action=write-review"),
#endif
            ExternalStoreUri = new Uri(result.TrackViewUrl),
        };
    }
}