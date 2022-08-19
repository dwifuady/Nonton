namespace Nonton.Dtos.Manifest;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Manifest
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("version")]
    public string? Version { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("resources")]
    public List<string>? Resources { get; set; }

    [JsonPropertyName("types")]
    public List<string>? Types { get; set; }

    [JsonPropertyName("idPrefixes")]
    public List<string>? IdPrefixes { get; set; }

    [JsonPropertyName("addonCatalogs")]
    public List<AddonCatalog>? AddonCatalogs { get; set; }

    [JsonPropertyName("catalogs")]
    public List<Catalog>? Catalogs { get; set; }
}
