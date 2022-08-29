namespace Nonton.Dtos;

using System.Text.Json.Serialization;

public class BehaviorHints
{
    [JsonPropertyName("defaultVideoId")]
    public string? DefaultVideoId { get; set; }

    [JsonPropertyName("hasScheduledVideos")]
    public bool HasScheduledVideos { get; set; }

    [JsonPropertyName("bingeGroup")]
    public string? BingeGroup { get; set; }

    [JsonPropertyName("adult")]
    public bool Adult { get; set; }

    [JsonPropertyName("configurable")]
    public bool? Configurable { get; set; }

    [JsonPropertyName("configurationRequired")]
    public bool? ConfigurationRequired { get; set; }
}