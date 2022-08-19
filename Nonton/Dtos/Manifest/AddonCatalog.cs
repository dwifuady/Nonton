namespace Nonton.Dtos.Manifest;
using System.Text.Json.Serialization;

public class AddonCatalog
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}