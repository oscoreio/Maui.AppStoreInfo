using Windows.Services.Store;

// ReSharper disable once CheckNamespace
namespace Maui.AppStores;

/// <inheritdoc />
public sealed class AppStoreInfoImplementation : IAppStoreInfo
{
    /// <inheritdoc />
    public async Task<Version> GetLatestVersionAsync(CancellationToken cancellationToken = default)
    {
        var context = StoreContext.GetDefault();
        var updates = await context.GetAppAndOptionalStorePackageUpdatesAsync();
        if (updates.Count == 0 ||
            updates[0]?.Package?.Id?.Version is not {} packageVersion)
        {
            return AppInfo.Current.Version;
        }

        return new Version(
            major: packageVersion.Major,
            minor: packageVersion.Minor,
            build: packageVersion.Build,
            revision: packageVersion.Revision);
    }

    /// <inheritdoc />
    public async Task OpenApplicationInStoreAsync(CancellationToken cancellationToken = default)
    {
        var context = StoreContext.GetDefault();
        var product = await context.GetStoreProductForCurrentAppAsync();
        var storeId = product.Product.StoreId;

        var url = Guid.TryParse(storeId, out var appId)
            ? $"ms-windows-store://pdp/?AppId={appId}"
            : $"ms-windows-store://pdp/?ProductId={storeId}";
        
        _ = await Launcher.Default.OpenAsync(new Uri(url)).ConfigureAwait(false);
    }
}