namespace Maui.AppStores;

/// <summary>
/// Represents options for the <see cref="AppStoreInfo"/>.
/// </summary>
public class AppStoreInfoOptions
{
    /// <summary>
    /// This value is used to differentiate between multiple update or request processes within your app.
    /// </summary>
    public const string DefaultCountryCode = "us";
    
    /// <summary>
    /// Gets and sets the country code to use when looking up the app in the public store. <br/>
    /// Note: This is currently only needed/used by the Apple implementations (defaults to "us").
    /// </summary>
    public string CountryCode { get; set; } = DefaultCountryCode;
    
    /// <summary>
    /// Gets and sets the package name to use when looking up the app in the public store. <br/>
    /// Uses AppInfo.Current.PackageName as the default value.
    /// </summary>
    public string PackageName { get; set; } = AppInfo.Current.PackageName;
    
    /// <summary>
    /// Gets and sets the current version of the app. <br/>
    /// Uses AppInfo.Current.Version as the default value.
    /// </summary>
    public Version CurrentVersion { get; set; } = AppInfo.Current.Version;
}