using Nonton.Dtos;
using Refit;

namespace Nonton.Api
{
    public interface IStremioApi
    {
        [Get("/catalog/movie/top.json")]
        Task<Discover> GetTopMovies();

        [Get("/meta/movie/{id}.json")]
        Task<Detail> GetMovieDetail(string id);

        [Get("/stream/movie/{id}.json")]
        Task<StreamResponse> GetStream(string id);

        [Get("/catalog/{type}/{id}.json")]
        Task<Discover> GetCatalog(string type, string id);

        [Get("/catalog/{type}/{id}/genre={genre}.json")]
        Task<Discover> GetCatalogByGenre(string type, string id, string genre);
    }
}
