using Android.Content;
using Application = Android.App.Application;
using Net = Android.Net;

// ReSharper disable once CheckNamespace
namespace Maui.AppStores;

/// <inheritdoc />
public sealed class AppStoreInfoImplementation : IAppStoreInfo
{
    /// <inheritdoc />
    public Task<Version> GetLatestVersionAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(AppInfo.Current.Version);
    }

    /// <inheritdoc />
    public Task OpenApplicationInStoreAsync(CancellationToken cancellationToken = default)
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

        return Task.CompletedTask;
    }
}