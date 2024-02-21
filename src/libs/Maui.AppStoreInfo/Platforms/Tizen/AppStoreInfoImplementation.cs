// ReSharper disable once CheckNamespace
namespace Maui.AppStores;

/// <inheritdoc />
internal sealed class AppStoreInfoImplementation : IAppStoreInfo
{
    /// <inheritdoc />
    public AppStoreInformation? CachedInformation { get; set; }

    /// <inheritdoc />
    public Task<AppStoreInformation> GetInformationAsync(
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(CachedInformation ??= new AppStoreInformation());
    }
}