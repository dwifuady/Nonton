namespace Nonton.Features.Addons.Dtos;
using System.Text.Json.Serialization;

public class TrailerStreamDto
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("ytId")]
    public string? YtId { get; set; }
}