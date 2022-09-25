namespace Nonton.Features.Meta.Models;

public interface IMeta
{
    public MetaTypeEnum MetaType { get; }
    string Id { get; } 
    string Name { get; }
    string? ImdbId { get; }
    string? Description { get; }
    string? Year { get; }
    List<string>? Cast { get; }
    string? Country { get; set; }
    List<string>? Director { get; }
    List<string>? Genres { get; }
    string? ImdbRating { get; }
    DateTime? Released { get; }
    string? Slug { get; }
    string? Type { get; }
    List<string>? Writer { get; }
    string? Poster { get; }
    string? Background { get; }
    string? Logo { get; }
    string? Runtime { get; }
}
