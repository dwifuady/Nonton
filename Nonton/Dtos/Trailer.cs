using System.Text.Json.Serialization;

namespace Nonton.Dtos;

public class Trailer
{
    [JsonPropertyName("source")]
    public string? Source { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }
}
