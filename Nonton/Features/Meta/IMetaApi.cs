using Nonton.Features.Addons.Dtos;

namespace Nonton.Features.Meta;

public interface IMetaApi
{
    Task<DetailDto> GetMetaDto(string type, string id);
}
