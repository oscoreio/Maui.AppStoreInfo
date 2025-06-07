using Foundation;
using Maui.AppStores.Internal;
using StoreKit;

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
            Type = GetAppStoreType(),
        };
    }
    
    /// <inheritdoc />
    async Task<bool> IAppStoreInfo.OpenApplicationInStoreAsync(CancellationToken cancellationToken)
    {
        CachedInformation ??= await GetInformationAsync(cancellationToken).ConfigureAwait(false);

        return await Launcher.Default.OpenAsync(CachedInformation.ExternalStoreUri).ConfigureAwait(false);
    }
    
    /// <summary>
    /// Retrieves the receipt URL path to determine if the app was installed via the App Store or TestFlight.
    /// </summary>
    /// <returns>The receipt path, or null if unavailable.</returns>
    private static AppStoreType GetAppStoreType()
    {
        // TODO: Modern way — StoreKit 2 - Seems MAUI is not supported in .NET 9 still
        // https://developer.apple.com/documentation/foundation/bundle/appstorereceipturl
        
        // A missing receipt means the app was deployed directly (Xcode, Enterprise, sideload).
        var receiptUrl = NSBundle.MainBundle.AppStoreReceiptUrl;
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (receiptUrl is null)
        {
            return AppStoreType.ManualInstallation;
        }
        
        // App Store receipts end with “…/receipt”, TestFlight with “…/sandboxReceipt”.
        return receiptUrl.LastPathComponent?.Equals("sandboxReceipt", StringComparison.OrdinalIgnoreCase) == true
            ? AppStoreType.AppleTestFlight
            : AppStoreType.AppleAppStore;
    }
}