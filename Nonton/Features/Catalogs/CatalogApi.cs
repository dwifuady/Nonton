using Nonton.Features.Addons.Api;
using Nonton.Features.Addons.Dtos;
using Nonton.Features.Addons.Dtos.Manifest;
using Refit;

namespace Nonton.Features.Catalogs;
public class CatalogApi : ICatalogApi
{
    public async Task<Discover> GetDiscoverItem(string baseUri, string type, string catalogId, string genre = "")
    {
        var api = RestService.For<IStremioApi>(baseUri);

        if (string.IsNullOrWhiteSpace(genre))
            return await api.GetCatalogByCatalogId(type, catalogId);

        return await api.GetCatalogByGenre(type, catalogId, genre);
    }

    public async Task<Discover> GetDiscoverItem(AddonDto addon, string type, string catalogId, string genre = "")
    {
        return await GetDiscoverItem(type, catalogId, genre);
    }

    public async Task<Discover> Search(AddonDto addon, string type, string catalogId, string query)
    {
        return await Search(addon.BaseUri, type, catalogId, query);
    }

    public async Task<Discover> Search(string baseUri, string type, string catalogId, string query)
    {
        var api = RestService.For<IStremioApi>(baseUri);

        return await api.SearchFromCatalog(type, catalogId, query);
    }
}