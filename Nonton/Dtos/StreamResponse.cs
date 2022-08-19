namespace Nonton.Dtos;

using System.Text.Json.Serialization;

public class StreamResponse
{
    [JsonPropertyName("streams")]
    public List<Stream>? Streams { get; set; }

    [JsonPropertyName("cacheMaxAge")]
    public int CacheMaxAge { get; set; }

    [JsonPropertyName("staleRevalidate")]
    public int StaleRevalidate { get; set; }

    [JsonPropertyName("staleError")]
    public int StaleError { get; set; }
}