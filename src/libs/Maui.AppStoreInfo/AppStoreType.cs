namespace Maui.AppStores;

/// <summary>
/// 
/// </summary>
public enum AppStoreType
{
    /// <summary>No store / unknown channel.</summary>
    Unknown = 0,

    /// <summary>Google Play production track.</summary>
    GooglePlay,

    /// <summary>Apple App Store production.</summary>
    AppleAppStore,

    /// <summary>Apple TestFlight beta distribution.</summary>
    AppleTestFlight,

    /// <summary>Microsoft Store for Windows desktop.</summary>
    MicrosoftStore,

    /// <summary>Amazon Appstore for Android devices.</summary>
    AmazonAppstore,

    /// <summary>Huawei AppGallery for Huawei devices.</summary>
    HuaweiAppGallery,

    /// <summary>Samsung Galaxy Store for Samsung devices.</summary>
    SamsungGalaxyStore,
    
    /// <summary>Manual installation (sideload, ADB, etc.)</summary>
    ManualInstallation,
}