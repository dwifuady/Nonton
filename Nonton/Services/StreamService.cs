using Nonton.Api;
using Nonton.Dtos;
using Refit;

namespace Nonton.Services
{
    public class StreamService : IStreamService
    {
        private readonly IAddonService _addonService;

        public StreamService(IAddonService addonService)
        {
            _addonService = addonService;
        }

        public async Task<IEnumerable<StreamResponse>> GetStream(string id)
        {
            var streamAddons = await _addonService.LoadAllStreamAddons();

            var streamResponses = new List<StreamResponse>();

            if (streamAddons == null) return streamResponses;

            foreach (var streamAddon in streamAddons)
            {
                var api = RestService.For<IStremioApi>(streamAddon.BaseUri);
                var streamResponse = await api.GetMovieStream(id);
                if (streamResponse.Streams != null && streamResponse.Streams.Any())
                {
                    streamResponses.Add(streamResponse);
                }
            }

            return streamResponses;
        }
    }
}
