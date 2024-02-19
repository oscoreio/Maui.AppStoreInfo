#if !__IOS__
// ReSharper disable once CheckNamespace
namespace Maui.AppStores;

/// <inheritdoc />
internal sealed class EmptyAppStoreInfoImplementation : IAppStoreInfo
{
    /// <inheritdoc />
    public Task<Version> GetLatestVersionAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(new Version(0, 0));
    }

    /// <inheritdoc />
    public Task OpenApplicationInStoreAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
#endif