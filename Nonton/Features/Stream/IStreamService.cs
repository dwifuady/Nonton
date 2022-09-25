using Nonton.Features.Addons.Dtos;
using Nonton.Features.Addons.Dtos.Manifest;

namespace Nonton.Features.Stream
{
    public interface IStreamService
    {
        Task<IEnumerable<StreamResponseDto>> GetStream(string id);
        Task<StreamResponseDto?> GetStream(AddonDto addon, string id);
    }
}
