namespace Maui.AppStores;

/// <summary>
/// Extension methods for <see cref="IAppStoreInfo"/>.
/// </summary>
public static class AppStoreInfoExtensions
{
    /// <summary>
    /// Checks if the current app is the latest version available in the public store. <br/>
    /// Uses AppInfo.Current.VersionString as the current version if none is provided.
    /// </summary>
    /// <returns>True if the current app is the latest version available, false otherwise.</returns>
    public static async Task<bool> IsUsingLatestVersionAsync(
        this IAppStoreInfo appStoreInfo,
        Version? currentVersion = null,
        CancellationToken cancellationToken = default)
    {
        appStoreInfo = appStoreInfo ?? throw new ArgumentNullException(nameof(appStoreInfo));
        currentVersion ??= AppInfo.Current.Version;
        
        var latestVersion = await appStoreInfo.GetLatestVersionAsync(
            cancellationToken).ConfigureAwait(false);

        return currentVersion >= latestVersion;
    }
}