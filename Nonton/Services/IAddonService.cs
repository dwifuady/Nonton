using Nonton.Dtos.Manifest;

namespace Nonton.Services;
public interface IAddonService
{
    Task<IEnumerable<Addon>?> LoadAllAddons();
    Task<IEnumerable<Addon>?> LoadAllCatalogAddons();
    Task<IEnumerable<Addon>?> LoadAllMetaAddons();
    Task<IEnumerable<Addon>?> LoadAllStreamAddons();
    Task<Addon?> LoadDefaultCatalogAddons();
    Task SaveAddon(string url, string manifestString);
    Task DeleteAddon(string id);
    Task<IEnumerable<Addon>?> GetAddonCollection();
}
