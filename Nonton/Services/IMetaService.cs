using Nonton.Dtos;

namespace Nonton.Services
{
    public interface IMetaService
    {
        Task<Detail> GetMeta(string type, string id);
    }
}
