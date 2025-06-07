using Windows.ApplicationModel;
using Windows.Services.Store;
using AppInfo = Microsoft.Maui.ApplicationModel.AppInfo;

// ReSharper disable once CheckNamespace
namespace Maui.AppStores;

/// <inheritdoc />
public sealed class AppStoreInfoImplementation : IAppStoreInfo
{
    /// <inheritdoc />
    public AppStoreInformation? CachedInformation { get; set; }

    /// <inheritdoc />
    public async Task<AppStoreInformation> GetInformationAsync(
        CancellationToken cancellationToken = default)
    {
        var context = StoreContext.GetDefault();
        var updates = await context.GetAppAndOptionalStorePackageUpdatesAsync();
        var product = await context.GetStoreProductForCurrentAppAsync();
        var storeId = product.Product.StoreId;
        var isAppId = Guid.TryParse(storeId, out var appId);
        
        return CachedInformation ??= new AppStoreInformation
        {
            Title = product.Product.Title,
            Description = product.Product.Description,
            ReleaseNotes = string.Empty,
            ApplicationSizeInBytes = 0L,
            LatestVersion =
                updates.Count == 0 ||
                updates[0]?.Package?.Id?.Version is not {} packageVersion
                    ? AppInfo.Current.Version
                    : new Version(
                        major: packageVersion.Major,
                        minor: packageVersion.Minor,
                        build: packageVersion.Build,
                        revision: packageVersion.Revision),
            InternalStoreUri = new Uri(isAppId
                ? $"ms-windows-store://pdp/?AppId={appId}"
                : $"ms-windows-store://pdp/?ProductId={storeId}"),
            ExternalStoreUri = product.Product.LinkUri,
            InternalReviewUri = new Uri(isAppId
                ? $"ms-windows-store://review/?AppId={appId}"
                : $"ms-windows-store://review/?ProductId={storeId}"),
            Type = Package.Current.SignatureKind == PackageSignatureKind.Store
                ? AppStoreType.MicrosoftStore
                : AppStoreType.ManualInstallation,
        };
    }
}