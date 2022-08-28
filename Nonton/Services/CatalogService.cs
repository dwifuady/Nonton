using Nonton.Api;
using Nonton.Dtos;
using Nonton.Dtos.Manifest;
using Refit;

namespace Nonton.Services;
public class CatalogService : ICatalogService
{
    public async Task<Discover> GetDiscoverItem(Addon addon, string type, string catalogId, string genre = "")
    {
        var api = RestService.For<IStremioApi>(addon.BaseUri);

        if (string.IsNullOrWhiteSpace(genre))
            return await api.GetCatalogByCatalogId(type, catalogId);

        return await api.GetCatalogByGenre(type, catalogId, genre);
    }

    public async Task<Discover> Search(Addon addon, string type, string catalogId, string query)
    {
        var api = RestService.For<IStremioApi>(addon.BaseUri);

        return await api.SearchFromCatalog(type, catalogId, query);
    }
}