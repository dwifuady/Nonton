using Nonton.Features.Addons.Dtos;
using Nonton.Features.Addons.Dtos.Manifest;

namespace Nonton.Features.Catalogs;
public interface ICatalogApi
{
    Task<Discover> GetDiscoverItem(string baseUri, string type, string catalogId, string genre = "");
    Task<Discover> GetDiscoverItem(AddonDto addon, string type, string catalogId, string genre = "");
    Task<Discover> Search(string baseUri, string type, string catalogId, string query);
    Task<Discover> Search(AddonDto addon, string type, string catalogId, string query);
}

