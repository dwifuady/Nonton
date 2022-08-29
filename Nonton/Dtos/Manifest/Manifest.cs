using static Nonton.Commons.AddonResourcesParser;

namespace Nonton.Dtos.Manifest;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Manifest
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("logo")]
    public string? Logo { get; set; }

    [JsonPropertyName("version")]
    public string? Version { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("types")]
    public List<string>? Types { get; set; }

    [JsonPropertyName("idPrefixes")]
    public List<string>? IdPrefixes { get; set; }

    [JsonPropertyName("addonCatalogs")]
    public List<AddonCatalog>? AddonCatalogs { get; set; }

    [JsonPropertyName("catalogs")]
    public List<Catalog>? Catalogs { get; set; }

    [JsonPropertyName("background")]
    public string? Background { get; set; }

    [JsonPropertyName("behaviorHints")]
    public BehaviorHints? BehaviorHints { get; set; }

    [JsonPropertyName("resources")]
    public object? ResourcesObject { get; set; }

    [JsonPropertyName("icon")]
    public string? Icon { get; set; }

    [JsonPropertyName("contactEmail")]
    public string? ContactEmail { get; set; }
    
    [JsonPropertyName("isFree")]
    public bool? IsFree { get; set; }

    [JsonPropertyName("idProperty")]
    public List<string>? IdProperty { get; set; }

    [JsonPropertyName("favicon")]
    public string? Favicon { get; set; }

    [JsonPropertyName("configurable")]
    public bool? Configurable { get; set; }

    [JsonIgnore]
    public List<string>? ResourcesString
    {
        get
        {
            TryParseJson<List<string>>(ResourcesObject?.ToString(), out var resourcesString);
            return resourcesString;
        }
    }

    [JsonIgnore]
    public List<Resource>? Resources
    {
        get
        {
            TryParseJson<List<Resource>>(ResourcesObject?.ToString(), out var resources);
            return resources;
        }
    }
}
