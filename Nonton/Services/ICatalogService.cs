using Nonton.Dtos;

namespace Nonton.Services;
public interface ICatalogService
{
    Task<Discover?> GetMovieCatalogDefault();
    Task<Discover?> GetCatalog(string type, string id);
    Task<Discover> GetCatalogByGenre(string type, string id, string genre);

}

