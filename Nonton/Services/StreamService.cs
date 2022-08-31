using Microsoft.AspNetCore.Components;
using MudBlazor;
using Nonton.Api;
using Nonton.Commons;
using Nonton.Dtos;
using Nonton.Dtos.Manifest;
using Refit;

namespace Nonton.Services
{
    public class StreamService : IStreamService
    {
        [Inject] public ISnackbar Snackbar { get; set; } = null!;

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

            var streamType = AddonConstants.TypeMovie;
            if (id.Contains(":"))
            {
                streamType = AddonConstants.TypeSeries;
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

        public async Task<StreamResponse?> GetStream(Addon addon, string id)
        {
            var streamType = AddonConstants.TypeMovie;
            if (id.Contains(":"))
            {
                streamType = AddonConstants.TypeSeries;
            }

            var api = RestService.For<IStremioApi>(addon.BaseUri);
            return await api.GetStream(streamType, id);
        }
    }
}
