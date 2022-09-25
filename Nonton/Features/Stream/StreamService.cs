using Nonton.Commons;
using Nonton.Features.Addons;
using Nonton.Features.Addons.Api;
using Nonton.Features.Addons.Dtos;
using Nonton.Features.Addons.Dtos.Manifest;
using Refit;

namespace Nonton.Features.Stream
{
    public class StreamService : IStreamService
    {

        private readonly IAddonService _addonService;

        public StreamService(IAddonService addonService)
        {
            _addonService = addonService;
        }

        public async Task<IEnumerable<StreamResponseDto>> GetStream(string id)
        {
            var streamAddons = await _addonService.LoadAllStreamAddons();

            var streamResponses = new List<StreamResponseDto>();

            if (streamAddons == null) return streamResponses;

            var streamType = AddonConstants.TypeMovieShortName;
            if (id.Contains(":"))
            {
                streamType = AddonConstants.TypeSeriesShortName;
            }

            foreach (var streamAddon in streamAddons)
            {
                var api = RestService.For<IStremioApi>(streamAddon.BaseUri);
                var streamResponse = await api.GetStream(streamType, id);
                if (streamResponse.Streams != null && streamResponse.Streams.Any())
                {
                    streamResponses.Add(streamResponse);
                }
            }

            return streamResponses;
        }

        public async Task<StreamResponseDto?> GetStream(AddonDto addon, string id)
        {
            var streamType = AddonConstants.TypeMovieShortName;
            if (id.Contains(":"))
            {
                streamType = AddonConstants.TypeSeriesShortName;
            }

            var api = RestService.For<IStremioApi>(addon.BaseUri);
            return await api.GetStream(streamType, id);
        }
    }
}
