using Nonton.Api;
using Nonton.Commons;
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
        var addons = await _addonService.LoadAllAddons();
        return addons?.FirstOrDefault();
    }

    public async Task<Discover?> GetMovies()
    {
        var addon = await _addonService.LoadDefaultCatalogAddons();
        if (addon == null) return null;

        var defaultId = addon?.Manifest?.Catalogs?.FirstOrDefault(x => (x.ExtraRequired != null && !x.ExtraRequired.Any()) || x.ExtraRequired is null)?.Id;

        if (string.IsNullOrWhiteSpace(defaultId)) return null;

        var stremioApi = RestService.For<IStremioApi>(addon!.BaseUri);
        return await stremioApi.GetCatalogByCatalogId(AddonConstants.TypeMovie, defaultId);
    }

    public async Task<Discover> GetMoviesByCatalog(Addon addon, string catalogId)
    {
        var api = RestService.For<IStremioApi>(addon.BaseUri);
        return await api.GetCatalogByCatalogId(AddonConstants.TypeMovie, catalogId);
    }

    public Task<Discover> GetMoviesByCatalog(string catalogId)
    {
        throw new NotImplementedException();
    }

    public Task<Discover> GetMoviesByCatalogGenre(string catalogId, string genre)
    {
        throw new NotImplementedException();
    }
}