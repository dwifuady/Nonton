using Nonton.Dtos;
using Nonton.Dtos.Manifest;

namespace Nonton.Services;
public interface ICatalogService
{
    Task<Discover> GetDiscoverItem(Addon addon, string type, string catalogId, string genre ="");
    Task<Discover> Search(Addon addon, string type, string catalogId, string query);
}

