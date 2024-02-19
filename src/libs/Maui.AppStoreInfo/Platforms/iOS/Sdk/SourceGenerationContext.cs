using System.Text.Json.Serialization;

// ReSharper disable once CheckNamespace
namespace Maui.AppStores.Internal;

[JsonSerializable(typeof(LookupResponse))]
internal sealed partial class SourceGenerationContext : JsonSerializerContext;