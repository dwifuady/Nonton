using Nonton.Dtos.Manifest;

namespace Nonton.Services;
public interface IAddonService
{
    Task<IEnumerable<Addon>?> LoadAddons();
}
