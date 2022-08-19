namespace Nonton.Dtos;

using System.Text.Json.Serialization;

public class Link
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("category")]
    public string? Category { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }
}