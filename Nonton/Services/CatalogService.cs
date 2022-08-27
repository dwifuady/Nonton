using Nonton.Api;
using Nonton.Commons;
using Nonton.Dtos;
using Nonton.Dtos.Manifest;
using Refit;

namespace Nonton.Services;
public class CatalogService : ICatalogService
{
    public async Task<IEnumerable<Catalog>?> GetAddonsDefaultCatalogs(Addon addon)
    {
        if (addon.Manifest?.Catalogs != null && addon.Manifest.Catalogs.Any())
        {
            return await Task.FromResult(
                    addon.Manifest.Catalogs
                        .Where(x =>
                            x.Type is AddonConstants.TypeMovie or AddonConstants.TypeSeries &&
                            ((x.ExtraRequired != null && !x.ExtraRequired.Any()) || x.ExtraRequired is null))
            );
        }

        return null;
    }

    public async Task<Discover> GetDiscoverItem(Addon addon, string type, string catalogId, string genre = "")
    {
        var api = RestService.For<IStremioApi>(addon.BaseUri);

        if (string.IsNullOrWhiteSpace(genre))
            return await api.GetCatalogByCatalogId(type, catalogId);

        return await api.GetCatalogByGenre(type, catalogId, genre);
    }
}