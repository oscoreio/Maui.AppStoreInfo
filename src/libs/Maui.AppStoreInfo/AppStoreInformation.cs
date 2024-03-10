namespace Maui.AppStores;

/// <summary>
/// 
/// </summary>
public class AppStoreInformation
{
    /// <summary>
    /// The latest version.
    /// </summary>
    public Version LatestVersion { get; init; } = new(major: 0, minor: 0, build: 0, revision: 0);
    
    /// <summary>
    /// The external store URL(Accessible from the Internet).
    /// </summary>
    public Uri ExternalStoreUri { get; init; } = new("about:blank");
    
    /// <summary>
    /// The internal store URL(Available for opening within a specific platform).
    /// </summary>
    public Uri InternalStoreUri { get; init; } = new("about:blank");
    
    /// <summary>
    /// The internal store review URL(Available for opening within a specific platform).
    /// </summary>
    public Uri InternalReviewUri { get; init; } = new("about:blank");
    
    /// <summary>
    /// The title in App Store.
    /// </summary>
    public string Title { get; init; } = string.Empty;
    
    /// <summary>
    /// The description in App Store.
    /// </summary>
    public string Description { get; init; } = string.Empty;
    
    /// <summary>
    /// The release notes in App Store.
    /// </summary>
    public string ReleaseNotes { get; init; } = string.Empty;
    
    /// <summary>
    /// Application size in bytes in App Store.
    /// </summary>
    public long ApplicationSizeInBytes { get; init; }
}