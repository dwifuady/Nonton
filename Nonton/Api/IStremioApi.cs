using Nonton.Dtos;
using Nonton.Dtos.Manifest;
using Refit;

namespace Nonton.Api
{
    public interface IStremioApi
    {
        [Get("/meta/{type}/{id}.json")]
        Task<Detail> GetMeta(string type, string id);

        [Get("/stream/{type}/{id}.json")]
        Task<StreamResponse> GetStream(string type, string id);

        [Get("/catalog/{type}/{id}.json")]
        Task<Discover> GetCatalogByCatalogId(string type, string id);

        [Get("/catalog/{type}/{id}/genre={genre}.json")]
        Task<Discover> GetCatalogByGenre(string type, string id, string genre);

        [Get("/catalog/{type}/{id}/search={query}.json")]
        Task<Discover> SearchFromCatalog(string type, string id, string query);

        [Get("/addonscollection.json")]
        Task<List<Addon>>? GetAddonCollection();
    }
}
