namespace Maui.AppStores;

/// <summary>
/// Interface for checking the current app information of the public store.
/// </summary>
public interface IAppStoreInfo
{
    /// <summary>
    /// Returns the cached information about the current application available in the public store.
    /// </summary>
    public AppStoreInformation? CachedInformation { get; set; }
    
    /// <summary>
    /// Gets the information about the current application available in the public store.
    /// </summary>
    /// <returns>The current app's latest version number.</returns>
    Task<AppStoreInformation> GetInformationAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the version number of the current app's latest version available in the public store.
    /// </summary>
    /// <returns>The current app's latest version number.</returns>
    public async Task<Version> GetLatestVersionAsync(
        CancellationToken cancellationToken = default)
    {
        CachedInformation = await GetInformationAsync(cancellationToken).ConfigureAwait(false);

        return CachedInformation.LatestVersion;
    }
    
    /// <summary>
    /// Opens the current app page in the public store using internal store uri.
    /// </summary>
    public async Task<bool> OpenApplicationInStoreAsync(CancellationToken cancellationToken = default)
    {
        CachedInformation ??= await GetInformationAsync(cancellationToken).ConfigureAwait(false);

        return await Launcher.Default.OpenAsync(CachedInformation.InternalStoreUri).ConfigureAwait(false);
    }
    
    /// <summary>
    /// Opens the store review page.
    /// </summary>
    public async Task<bool> OpenStoreReviewPage(CancellationToken cancellationToken = default)
    {
        CachedInformation ??= await GetInformationAsync(cancellationToken).ConfigureAwait(false);

        return await Launcher.Default.OpenAsync(CachedInformation.InternalReviewUri).ConfigureAwait(false);
    }
    
    /// <summary>
    /// Checks if the current app is the latest version available in the public store. <br/>
    /// Uses AppStoreInfo.Options.CurrentVersion(with AppInfo.Current.Version as default)
    /// as the current version if none is provided.
    /// </summary>
    /// <returns>True if the current app is the latest version available, false otherwise.</returns>
    public async Task<bool> IsUsingLatestVersionAsync(
        Version? currentVersion = null,
        CancellationToken cancellationToken = default)
    {
        currentVersion ??= AppStoreInfo.Options.CurrentVersion;
        
        var latestVersion = await GetLatestVersionAsync(
            cancellationToken).ConfigureAwait(false);

        return currentVersion >= latestVersion;
    }
}