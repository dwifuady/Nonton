namespace Nonton.Features.Meta.Models;
public class MovieMeta : DefaultMeta, IMovieMeta
{
    public MovieMeta(string id, string name, string? imdbId, string? description, string? year, List<string>? cast,
        List<string>? director, List<string>? genres, string? imdbRating, DateTime? released, string? slug,
        string? type, List<string>? writer, string? poster, string? background, string? logo, string? runtime,
        List<(string Type, string Source)> trailers) : base(id, name, imdbId, description, year, cast, director, genres,
        imdbRating, released, slug, type, writer, poster, background, logo, runtime)
    {
        Trailers = trailers;
    }

    public List<(string Type, string Source)> Trailers { get; }
}
