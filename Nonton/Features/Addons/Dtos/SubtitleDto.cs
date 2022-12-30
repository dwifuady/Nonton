using System.Text.Json.Serialization;
namespace Nonton.Features.Addons.Dtos;

public class SubtitleDto
{
    [JsonPropertyName("subtitles")]
    public List<Subtitle>? Subtitles { get; set; }
}

public class Subtitle
{
    [JsonPropertyName("lang")]
    public string? Lang { get; set; }

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }
}