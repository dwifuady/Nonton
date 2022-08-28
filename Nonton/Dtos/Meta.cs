namespace Nonton.Dtos;

using System.Text.Json.Serialization;

public class Meta
{
    [JsonPropertyName("imdb_id")]
    public string? ImdbId { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("cast")]
    public List<string>? Cast { get; set; }

    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("director")]
    public List<string>? Director { get; set; }

    //[JsonPropertyName("dvdRelease")]
    //public DateTime DvdRelease { get; set; }

    [JsonPropertyName("genre")]
    public List<string>? Genre { get; set; }

    [JsonPropertyName("imdbRating")]
    public string? ImdbRating { get; set; }

    [JsonPropertyName("released")]
    public DateTime Released { get; set; }

    [JsonPropertyName("slug")]
    public string? Slug { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("writer")]
    public List<string>? Writer { get; set; }

    [JsonPropertyName("year")]
    public string? Year { get; set; }

    [JsonPropertyName("popularities")]
    public Popularities? Popularities { get; set; }

    [JsonPropertyName("poster")]
    public string? Poster { get; set; }

    [JsonPropertyName("trailers")]
    public List<Trailer>? Trailers { get; set; }

    [JsonPropertyName("background")]
    public string? Background { get; set; }

    [JsonPropertyName("logo")]
    public string? Logo { get; set; }

    [JsonPropertyName("popularity")]
    public double? Popularity { get; set; }

    [JsonPropertyName("id")] 
    public string Id { get; set; } = null!;

    [JsonPropertyName("genres")]
    public List<string>? Genres { get; set; }

    [JsonPropertyName("releaseInfo")]
    public string? ReleaseInfo { get; set; }

    [JsonPropertyName("trailerStreams")]
    public List<TrailerStream>? TrailerStreams { get; set; }

    [JsonPropertyName("links")]
    public List<Link>? Links { get; set; }

    [JsonPropertyName("behaviorHints")]
    public BehaviorHints? BehaviorHints { get; set; }

    [JsonPropertyName("runtime")]
    public string? Runtime { get; set; }

    [JsonPropertyName("videos")]
    public List<Video>? Videos { get; set; }
}