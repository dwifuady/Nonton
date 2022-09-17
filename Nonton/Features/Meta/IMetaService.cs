using Nonton.Features.Addons.Dtos;

namespace Nonton.Features.Meta
{
    public interface IMetaService
    {
        Task<Detail> GetMeta(string type, string id);
    }
}
