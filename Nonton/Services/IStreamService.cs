using Nonton.Dtos;
using Nonton.Dtos.Manifest;

namespace Nonton.Services
{
    public interface IStreamService
    {
        Task<IEnumerable<StreamResponse>> GetStream(string id);
        Task<StreamResponse?> GetStream(Addon addon, string id);
    }
}
