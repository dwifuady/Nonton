using Nonton.Dtos.Manifest;

namespace Nonton.Services;
public interface IAddonService
{
    Task<IEnumerable<Addon>?> LoadAllAddons();
    Task<IEnumerable<Addon>?> LoadAllCatalogAddons();
    Task<IEnumerable<Addon>?> LoadAllMetaAddons();
    Task<Addon?> LoadDefaultCatalogAddons();
}
