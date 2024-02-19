// ReSharper disable once CheckNamespace
namespace Maui.AppStores;

/// <inheritdoc />
internal sealed class AppStoreInfoImplementation : IAppStoreInfo
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