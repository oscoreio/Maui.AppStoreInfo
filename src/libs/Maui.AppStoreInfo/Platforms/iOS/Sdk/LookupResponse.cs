using System.Text.Json.Serialization;

// ReSharper disable once CheckNamespace
namespace Maui.AppStores.Internal;

internal sealed class LookupResponse
{
    [JsonPropertyName("resultCount")]
    public int ResultCount { get; set; }

    [JsonPropertyName("results")]
    public List<LookupResult> Results { get; set; } = [];
}