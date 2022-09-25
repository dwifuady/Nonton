namespace Nonton.Features.Meta.Models;
public class DefaultMeta : IMeta
{
    public DefaultMeta(string id, string name, string? imdbId, string? description, string? year, List<string>? cast, List<string>? director, List<string>? genres, string? imdbRating, DateTime? released, string? slug, string? type, List<string>? writer, string? poster, string? background, string? logo, string? runtime)
    {
        MetaType = MetaTypeEnum.Movie;
        Id = id;
        Name = name;
        ImdbId = imdbId;
        Description = description;
        Year = year;
        Cast = cast;
        Director = director;
        Genres = genres;
        ImdbRating = imdbRating;
        Released = released;
        Slug = slug;
        Type = type;
        Writer = writer;
        Poster = poster;
        Background = background;
        Logo = logo;
        Runtime = runtime;
    }

    public MetaTypeEnum MetaType { get; }
    public string Id { get; }
    public string Name { get; }
    public string? ImdbId { get; }
    public string? Description { get; }
    public string? Year { get; }
    public List<string>? Cast { get; }
    public string? Country { get; set; }
    public List<string>? Director { get; }
    public List<string>? Genres { get; }
    public string? ImdbRating { get; }
    public DateTime? Released { get; }
    public string? Slug { get; }
    public string? Type { get; }
    public List<string>? Writer { get; }
    public string? Poster { get; }
    public string? Background { get; }
    public string? Logo { get; }
    public string? Runtime { get; }
}