using Xamarin.Google.Android.Play.Core.AppUpdate;
using Xamarin.Google.Android.Play.Core.Install.Model;
using Xamarin.Google.Android.Play.Core.Tasks;
using Activity = Android.App.Activity;

// ReSharper disable once CheckNamespace
namespace Maui.InAppUpdates.Internal;

public class AppUpdateSuccessListener(
    IAppUpdateManager appUpdateManager,
    Activity activity,
    int updateRequest)
    : Java.Lang.Object, IOnSuccessListener
{
    public InstallStateUpdatedListener? InstallStateUpdatedListener { get; private set; }

    public void OnSuccess(Java.Lang.Object p0)
    {
        if (p0 is not AppUpdateInfo info)
        {
            return;
        }

        Handler.Options.DebugAction($"AVAILABLE VERSION CODE {info.AvailableVersionCode()}");

        var updateAvailability = info.UpdateAvailability();
        var updatePriority = info.UpdatePriority();
        var isImmediateUpdatesAllowed = info.IsUpdateTypeAllowed(AppUpdateType.Immediate);
        var isFlexibleUpdatesAllowed = info.IsUpdateTypeAllowed(AppUpdateType.Flexible);
        switch (updateAvailability)
        {
            case UpdateAvailability.UpdateAvailable or
                UpdateAvailability.DeveloperTriggeredUpdateInProgress
                when updatePriority >= Handler.Options.ImmediateUpdatePriority &&
                     isImmediateUpdatesAllowed:
            {
                _ = appUpdateManager.StartUpdateFlowForResult(
                    info,
                    AppUpdateType.Immediate,
                    activity,
                    updateRequest);
                break;
            }

            case UpdateAvailability.UpdateAvailable or
                UpdateAvailability.DeveloperTriggeredUpdateInProgress
                when isFlexibleUpdatesAllowed:
            {
                InstallStateUpdatedListener ??= new InstallStateUpdatedListener();
                appUpdateManager.RegisterListener(InstallStateUpdatedListener);

                _ = appUpdateManager.StartUpdateFlowForResult(
                    info,
                    AppUpdateType.Flexible,
                    activity,
                    updateRequest);
                break;
            }

            case UpdateAvailability.UpdateNotAvailable:
            case UpdateAvailability.Unknown:
                Handler.Options.DebugAction($"UPDATE NOT AVAILABLE {info.AvailableVersionCode()}");
                break;
        }
    }
}