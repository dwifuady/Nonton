using Nonton.Features.Addons.Dtos.Manifest;

namespace Nonton.Features.Addons;
public interface IAddonService
{
    Task<IEnumerable<AddonDto>?> LoadAllAddons();
    Task<IEnumerable<AddonDto>?> LoadAllCatalogAddons();
    Task<IEnumerable<AddonDto>?> LoadAllMetaAddons();
    Task<IEnumerable<AddonDto>?> LoadAllStreamAddons();
    Task<AddonDto?> LoadDefaultCatalogAddons();
    Task SaveAddon(string url, string manifestString);
    Task DeleteAddon(string id);
    Task<IEnumerable<AddonDto>?> GetAddonCollection();
}
