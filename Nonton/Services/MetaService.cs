using Nonton.Api;
using Nonton.Dtos;
using Refit;

namespace Nonton.Services
{
    public class MetaService : IMetaService
    {
        private readonly IAddonService _addonService;

        public MetaService(IAddonService addonService)
        {
            _addonService = addonService;
        }
        public async Task<Detail> GetMovieMeta(string id)
        {
            var metaAddons = await _addonService.LoadAllMetaAddons();

            var detail = new Detail();

            if (metaAddons == null) return detail;
            foreach (var metaAddon in metaAddons)
            {
                var api = RestService.For<IStremioApi>(metaAddon.BaseUri);

                detail = await api.GetMovieMeta(id);

                if (detail is { Meta: { } }) break;
            }

            return detail;
        }
    }
}