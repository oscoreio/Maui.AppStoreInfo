using Android.Content;
using Application = Android.App.Application;
using Net = Android.Net;

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
            ExternalStoreUri = new Uri($"https://play.google.com/store/apps/details?id={AppStoreInfo.Options.PackageName}"),
        });
    }

    /// <inheritdoc />
    Task<bool> IAppStoreInfo.OpenApplicationInStoreAsync(CancellationToken cancellationToken)
    {
        try
        {
            var intent = new Intent(
                Intent.ActionView,
                Net.Uri.Parse($"market://details?id={AppStoreInfo.Options.PackageName}"));
            intent.SetPackage("com.android.vending");
            intent.SetFlags(ActivityFlags.NewTask);
            Application.Context.StartActivity(intent);
        }
        catch (ActivityNotFoundException)
        {
            var intent = new Intent(
                Intent.ActionView,
                Net.Uri.Parse($"https://play.google.com/store/apps/details?id={AppStoreInfo.Options.PackageName}"));
            intent.SetFlags(ActivityFlags.NewTask);
            Application.Context.StartActivity(intent);
        }

        return Task.FromResult(true);
    }
}