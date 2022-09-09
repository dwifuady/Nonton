namespace Nonton.Features.Addons.Dtos;

using System.Text.Json.Serialization;

public class Stream
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("infoHash")]
    public string? InfoHash { get; set; }

    [JsonPropertyName("tag")]
    public string? Tag { get; set; }

    [JsonPropertyName("externalUrl")]
    public string? ExternalUrl { get; set; }

    [JsonPropertyName("androidTvUrl")]
    public string? AndroidTvUrl { get; set; }

    [JsonPropertyName("tizenUrl")]
    public string? TizenUrl { get; set; }

    [JsonPropertyName("webosUrl")]
    public string? WebosUrl { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("behaviorHints")]
    public BehaviorHints? BehaviorHints { get; set; }
}