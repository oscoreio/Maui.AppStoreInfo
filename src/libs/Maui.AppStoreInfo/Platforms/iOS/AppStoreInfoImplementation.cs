using Maui.AppStores.Internal;

// ReSharper disable once CheckNamespace
namespace Maui.AppStores;

/// <inheritdoc />
internal sealed class AppStoreInfoImplementation : IAppStoreInfo
{
    private LookupResult? _result;

    /// <inheritdoc />
    public async Task<Version> GetLatestVersionAsync(CancellationToken cancellationToken = default)
    {
        _result = await LookupApplicationAsync(cancellationToken).ConfigureAwait(false);

        return Version.Parse(_result.Version);
    }

    /// <inheritdoc />
    public async Task OpenApplicationInStoreAsync(CancellationToken cancellationToken = default)
    {
        _result ??= await LookupApplicationAsync(cancellationToken).ConfigureAwait(false);

        _ = await Launcher.Default.OpenAsync(new Uri($"{_result.TrackViewUrl}")).ConfigureAwait(false);
    }

    private static async Task<LookupResult> LookupApplicationAsync(CancellationToken cancellationToken = default)
    {
        using var client = new HttpClient();
        var lookup = await client.LookupAsync(
            bundleIdentifier: AppStoreInfo.Options.PackageName,
            countryCode: AppStoreInfo.Options.CountryCode,
            cancellationToken: cancellationToken).ConfigureAwait(false);
            
        return lookup.FirstOrDefault() ??
               throw new InvalidOperationException(
                   $"No lookup result found for bundle identifier '{AppStoreInfo.Options.PackageName}' " +
                   $"and country code '{AppStoreInfo.Options.CountryCode}'.");
    }
}