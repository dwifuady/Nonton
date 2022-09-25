namespace Nonton.Features.Meta.Models;

public interface ISeriesMeta : IMeta
{
    List<(int SeasonNumber, string SeasonDescription)> Seasons { get; }
    List<Episode>? Episodes { get; }
}