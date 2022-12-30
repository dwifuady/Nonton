using Nonton.Features.Addons.Dtos;
using Nonton.Features.Addons.Dtos.Manifest;
using Refit;

namespace Nonton.Features.Addons.Api
{
    public interface IStremioApi
    {
        [Get("/meta/{type}/{id}.json")]
        Task<DetailDto> GetMeta(string type, string id);

        [Get("/stream/{type}/{id}.json")]
        Task<StreamResponseDto> GetStream(string type, string id);

        [Get("/catalog/{type}/{id}.json")]
        Task<DiscoverDto> GetCatalogByCatalogId(string type, string id);

        [Get("/catalog/{type}/{id}/genre={genre}.json")]
        Task<DiscoverDto> GetCatalogByGenre(string type, string id, string genre);

        [Get("/catalog/{type}/{id}/search={query}.json")]
        Task<DiscoverDto> SearchFromCatalog(string type, string id, string query);

        [Get("/addonscollection.json")]
        Task<List<AddonDto>>? GetAddonCollection();

        [Get("/subtitles/{type}/{id}.json")]
        Task<SubtitleDto>? GetSubtitles(string type, string id);
    }
}
