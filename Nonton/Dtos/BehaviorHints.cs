namespace Nonton.Dtos;

using System.Text.Json.Serialization;

public class BehaviorHints
{
    [JsonPropertyName("defaultVideoId")]
    public string? DefaultVideoId { get; set; }

    [JsonPropertyName("hasScheduledVideos")]
    public bool HasScheduledVideos { get; set; }
}