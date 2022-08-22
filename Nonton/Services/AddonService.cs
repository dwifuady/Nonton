using Nonton.Dtos.Manifest;
using Nonton.Commons;

namespace Nonton.Services;
public class AddonService : IAddonService
{
    public async Task<IEnumerable<Addon>?> LoadAllAddons()
    {
        return await LoadDefaultAddons();
    }

    public async Task<IEnumerable<Addon>?> LoadAllCatalogAddons()
    {
        return await Task.FromResult(DefaultAddons.AllDefaultAddons()!.Where(a => a.Manifest?.Resources != null && a.Manifest.Resources.Contains(AddonConstants.ResourcesCatalog)));
    }

    public async Task<IEnumerable<Addon>?> LoadAllMetaAddons()
    {
        return await Task.FromResult(DefaultAddons.AllDefaultAddons()!.Where(a => a.Manifest?.Resources != null && a.Manifest.Resources.Contains(AddonConstants.ResourcesMeta)));
    }

    public async Task<Addon?> LoadDefaultCatalogAddons()
    {
        return await Task.FromResult(DefaultAddons.AllDefaultAddons()!.FirstOrDefault(a => a.Manifest?.Resources != null && a.Manifest.Resources.Contains(AddonConstants.ResourcesCatalog)));
    }

    public async Task<IEnumerable<Addon>?> LoadDefaultAddons()
    {
        return await Task.FromResult(DefaultAddons.AllDefaultAddons());
    }
}