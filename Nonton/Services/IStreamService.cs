using Nonton.Dtos;

namespace Nonton.Services
{
    public interface IStreamService
    {
        Task<IEnumerable<StreamResponse>> GetStream(string id);
    }
}
