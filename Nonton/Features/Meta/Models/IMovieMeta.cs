namespace Nonton.Features.Meta.Models;
public interface IMovieMeta : IMeta
{
    List<(string Type, string Source)> Trailers { get; }
}
