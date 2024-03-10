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
    
    private static Task<bool> OpenApplicationInStoreAsync(CancellationToken cancellationToken)
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
}