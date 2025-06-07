using Android.Content;
using Android.OS;
using Application = Android.App.Application;

// ReSharper disable once CheckNamespace
namespace Maui.AppStores;

/// <inheritdoc />
public sealed class AppStoreInfoImplementation : IAppStoreInfo
{
    /// <inheritdoc />
    public AppStoreInformation? CachedInformation { get; set; }

    /// <inheritdoc />
    public Task<AppStoreInformation> GetInformationAsync(
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(CachedInformation ??= new AppStoreInformation
        {
            Title = string.Empty,
            Description = string.Empty,
            ReleaseNotes = string.Empty,
            ApplicationSizeInBytes = 0L,
            LatestVersion = AppInfo.Current.Version,
            InternalStoreUri = new Uri($"market://details?id={AppStoreInfo.Options.PackageName}"),
            InternalReviewUri = new Uri($"market://details?id={AppStoreInfo.Options.PackageName}"),
            ExternalStoreUri = new Uri($"https://play.google.com/store/apps/details?id={AppStoreInfo.Options.PackageName}"),
            Type = GetAppStoreType(),
        });
    }

    /// <inheritdoc />
    Task<bool> IAppStoreInfo.OpenApplicationInStoreAsync(CancellationToken cancellationToken)
    {
        return OpenApplicationInStoreAsync(cancellationToken);
    }

    /// <inheritdoc />
    Task<bool> IAppStoreInfo.OpenStoreReviewPage(CancellationToken cancellationToken)
    {
        return OpenApplicationInStoreAsync(cancellationToken);
    }
    
    private static Intent GetRateIntent(string url)
    {
        var intent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(url));
        intent.AddFlags(ActivityFlags.NoHistory);
        intent.AddFlags(ActivityFlags.MultipleTask);
        intent.AddFlags((int)Build.VERSION.SdkInt >= 21 
            ? ActivityFlags.NewDocument
            : ActivityFlags.ClearWhenTaskReset);
        intent.SetFlags(ActivityFlags.ClearTop);
        intent.SetFlags(ActivityFlags.NewTask);
        
        return intent;
    }
    
    private static Task<bool> OpenApplicationInStoreAsync(CancellationToken _ = default)
    {
        try
        {
            var intent = GetRateIntent($"market://details?id={AppStoreInfo.Options.PackageName}");
            intent.SetPackage("com.android.vending");
            
            Application.Context.StartActivity(intent);
            return Task.FromResult(true);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("Unable to launch app store: " + ex.Message);
        }
        
        try
        {
            var intent = GetRateIntent($"https://play.google.com/store/apps/details?id={AppStoreInfo.Options.PackageName}");
            
            Application.Context.StartActivity(intent);
            return Task.FromResult(true);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("Unable to launch app store: " + ex.Message);
        }

        return Task.FromResult(false);
    }

    /// <summary>
    /// Retrieves the package name of the installer (Google Play Store or other sources).
    /// </summary>
    /// <returns>The installer package name, or null if unavailable.</returns>
    private static AppStoreType GetAppStoreType()
    {
        try
        {
            var packageManager = Application.Context.PackageManager;
            var packageName = Application.Context.PackageName;
            if (packageManager == null || string.IsNullOrEmpty(packageName))
            {
                System.Diagnostics.Debug.WriteLine("PackageManager or PackageName is null.");
                return AppStoreType.Unknown;
            }
            
            // Android 11 (API 30) introduced InstallSourceInfo.
            string? installer = OperatingSystem.IsAndroidVersionAtLeast(30)
                ? packageManager.GetInstallSourceInfo(packageName).InstallingPackageName
                : packageManager.GetInstallerPackageName(packageName);

            return installer switch
            {
                // Google Play (all tracks - prod, closed, internal, etc.)
                "com.android.vending"                                   => AppStoreType.GooglePlay,
                
                // Amazon Appstore
                "com.amazon.venezia" or "com.amazon.appstore"           => AppStoreType.AmazonAppstore,
                
                // Huawei AppGallery
                "com.huawei.appmarket"                                  => AppStoreType.HuaweiAppGallery,
                
                // Samsung Galaxy Store
                "com.sec.android.app.samsungapps"                       => AppStoreType.SamsungGalaxyStore,
                
                // Sideload / ADB / unknown market
                "com.google.android.packageinstaller" or null           => AppStoreType.ManualInstallation,

                _                                                       => AppStoreType.Unknown,
            };
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error getting install source: {ex.Message}");
            
            return AppStoreType.Unknown; // Default to null if an error occurs
        }
    }
}