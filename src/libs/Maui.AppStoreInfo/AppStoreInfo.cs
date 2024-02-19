namespace Maui.AppStores;

/// <inheritdoc cref="IAppStoreInfo" />
public static class AppStoreInfo
{
    private static IAppStoreInfo? _currentImplementation;

    /// <summary>
    /// Options for the <see cref="AppStoreInfo"/>.
    /// </summary>
    public static AppStoreInfoOptions Options { get; set; } = new();
    
    /// <summary>
    /// Provides the default implementation for static usage of this API.
    /// </summary>
    public static IAppStoreInfo Current =>
        _currentImplementation ??= new AppStoreInfoImplementation();
}