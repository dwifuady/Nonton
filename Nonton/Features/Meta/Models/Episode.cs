namespace Nonton.Features.Meta.Models;
public class Episode
{
    public Episode(string id, int season, int number, string? name, DateTime? released, string? tvdbId, string? rating, string? overview, string? thumbnail)
    {
        Id = id;
        Season = season;
        Number = number;
        Name = name;
        Released = released;
        TvdbId = tvdbId;
        Rating = rating;
        Overview = overview;
        Thumbnail = thumbnail;
    }

    public string Id { get;}
    public int Season { get; }
    public int Number { get; }
    public string? Name { get;}
    public DateTime? Released { get; }
    public string? TvdbId { get; }
    public string? Rating { get; }
    public string? Overview { get; }
    public string? Thumbnail { get; }
}
