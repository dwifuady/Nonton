using Nonton.Dtos;
using Refit;

namespace Nonton.Api
{
    public interface IStremioApi
    {
        [Get("/meta/{type}/{id}.json")]
        Task<Detail> GetMeta(string type, string id);

        [Get("/stream/movie/{id}.json")]
        Task<StreamResponse> GetMovieStream(string id);

        [Get("/catalog/{type}/{id}.json")]
        Task<Discover> GetCatalogByCatalogId(string type, string id);

        [Get("/catalog/{type}/{id}/genre={genre}.json")]
        Task<Discover> GetCatalogByGenre(string type, string id, string genre);
    }
}
