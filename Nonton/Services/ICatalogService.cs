using Nonton.Dtos;
using Nonton.Dtos.Manifest;

namespace Nonton.Services;
public interface ICatalogService
{
    Task<Discover?> GetMovies();
    Task<Discover> GetMoviesByCatalog(Addon addon, string catalogId);
    Task<Discover> GetMoviesByCatalog(string catalogId);
    Task<Discover> GetMoviesByCatalogGenre(string catalogId, string genre);

}

