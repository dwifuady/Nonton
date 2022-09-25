namespace Nonton.Features.Meta.Models;
public class SeriesMeta : DefaultMeta, ISeriesMeta
{
    public SeriesMeta(string id, string name, string? imdbId, string? description, string? year, List<string>? cast,
        List<string>? director, List<string>? genres, string? imdbRating, DateTime? released, string? slug,
        string? type, List<string>? writer, string? poster, string? background, string? logo, string? runtime,
        List<(int SeasonNumber, string SeasonDescription)> seasons, List<Episode>? episodes) : base(id, name, imdbId,
        description, year, cast, director, genres, imdbRating, released, slug, type, writer, poster, background, logo,
        runtime)
    {
        Seasons = seasons;
        Episodes = episodes;
    }

    public List<(int SeasonNumber, string SeasonDescription)> Seasons { get; }
    public List<Episode>? Episodes { get; }
}

