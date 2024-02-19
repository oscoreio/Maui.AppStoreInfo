namespace Maui.AppStores;

/// <summary>
/// Interface for checking if the current app is the latest version available in the public store.
/// </summary>
public interface IAppStoreInfo
{
    /// <summary>
    /// Gets the version number of the current app's latest version available in the public store.
    /// </summary>
    /// <returns>The current app's latest version number.</returns>
    Task<Version> GetLatestVersionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Opens the current app page in the public store.
    /// </summary>
    Task OpenApplicationInStoreAsync(CancellationToken cancellationToken = default);
}