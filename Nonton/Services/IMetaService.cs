using Nonton.Dtos;

namespace Nonton.Services
{
    public interface IMetaService
    {
        Task<Detail> GetMovieMeta(string id);
    }
}
