using Nonton.Features.Addons.Api;
using Nonton.Features.Addons.Dtos;
using Refit;

namespace Nonton.Features.Catalogs;
public class CatalogApi : ICatalogApi
{
    public async Task<DiscoverDto> GetCatalogItem(string baseUri, string type, string catalogId, string genre = "")
    {
        var api = RestService.For<IStremioApi>(baseUri);

        if (string.IsNullOrWhiteSpace(genre))
            return await api.GetCatalogByCatalogId(type, catalogId);

        return await api.GetCatalogByGenre(type, catalogId, genre);
    }

    public async Task<DiscoverDto> Search(string baseUri, string type, string catalogId, string query)
    {
        var api = RestService.For<IStremioApi>(baseUri);

        return await api.SearchFromCatalog(type, catalogId, query);
    }
}