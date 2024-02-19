namespace Maui.AppStores;

/// <summary>
/// Extensions for <see cref="MauiAppBuilder"/>.
/// </summary>
public static class MauiAppBuilderExtensions
{
    /// <summary>
    /// Adds App Store Info service to the application.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="setupAction"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static MauiAppBuilder UseAppStoreInfo(
        this MauiAppBuilder builder,
        Action<AppStoreInfoOptions>? setupAction = null) 
    {
        builder = builder ?? throw new ArgumentNullException(nameof(builder));
        
        setupAction?.Invoke(AppStoreInfo.Options);
        
        builder.Services.AddSingleton<IAppStoreInfo>(static _ => AppStoreInfo.Current);
        
        return builder;
    }
}
