using Nonton.Features.Addons.Dtos;
using Nonton.Features.Addons.Dtos.Manifest;

namespace Nonton.Features.Stream
{
    public interface IStreamService
    {
        Task<IEnumerable<StreamResponse>> GetStream(string id);
        Task<StreamResponse?> GetStream(AddonDto addon, string id);
    }
}
