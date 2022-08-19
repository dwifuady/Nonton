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
    }
}
