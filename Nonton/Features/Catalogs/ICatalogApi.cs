using Nonton.Features.Addons.Dtos;

namespace Nonton.Features.Catalogs;
public interface ICatalogApi
{
    Task<DiscoverDto> GetCatalogItem(string baseUri, string type, string catalogId, string genre = "");
    Task<DiscoverDto> Search(string baseUri, string type, string catalogId, string query);
}

