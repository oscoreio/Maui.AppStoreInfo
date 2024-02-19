using System.Text.Json;

// ReSharper disable once CheckNamespace
namespace Maui.AppStores.Internal;

internal static class HttpClientExtensions
{
    public static async Task<IList<LookupResult>> LookupAsync(
        this HttpClient client,
        string bundleIdentifier,
        string? countryCode = null,
        CancellationToken cancellationToken = default)
    {
        client = client ?? throw new ArgumentNullException(nameof(client));
        countryCode = string.IsNullOrWhiteSpace(countryCode)
            ? "gb"
            : countryCode;

        var response = await client.GetAsync(
            new Uri($"https://itunes.apple.com/{countryCode}/lookup?bundleId={bundleIdentifier}&cache={Guid.NewGuid()}"),
            cancellationToken).ConfigureAwait(false);
        var json = await response.Content.ReadAsStringAsync(
            cancellationToken).ConfigureAwait(false);
        var lookup = JsonSerializer.Deserialize(
            json,
            SourceGenerationContext.Default.LookupResponse) ?? new LookupResponse();
        
        return lookup.Results;
    }
}