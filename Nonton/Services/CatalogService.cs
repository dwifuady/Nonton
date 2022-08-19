using Nonton.Api;
using Nonton.Dtos;
using Nonton.Dtos.Manifest;
using Refit;

namespace Nonton.Services;
public class CatalogService : ICatalogService
{
    private readonly IAddonService _addonService;
    public CatalogService(IAddonService addonService)
    {
        _addonService = addonService;
    }

    private async Task<Addon?> LoadAddon()
    {
        var addons = await _addonService.LoadAddons();
        return addons?.FirstOrDefault();
    }

    public async Task<Discover?> GetMovieCatalogDefault()
    {
        var addon = await LoadAddon();
        if (addon == null) return null;

        var defaultId = addon?.Manifest?.Catalogs?.FirstOrDefault(x => (x.ExtraRequired != null && !x.ExtraRequired.Any()) || x.ExtraRequired is null)?.Id;

        if (string.IsNullOrWhiteSpace(defaultId)) return null;

        var stremioApi = RestService.For<IStremioApi>(addon!.BaseUri);
        return await stremioApi.GetCatalog("movie", defaultId);
    }

    public async Task<Discover?> GetCatalog(string type, string id)
    {
        var addon = await LoadAddon();
        if (addon == null) return null;

        var stremioApi = RestService.For<IStremioApi>(addon.BaseUri);
        return await stremioApi.GetCatalog(type, id);
    }

    public Task<Discover> GetCatalogByGenre(string type, string id, string genre)
    {
        throw new NotImplementedException();
    }
}