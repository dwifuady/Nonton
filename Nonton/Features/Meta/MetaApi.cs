using Nonton.Features.Addons;
using Nonton.Features.Addons.Api;
using Nonton.Features.Addons.Dtos;
using Refit;

namespace Nonton.Features.Meta;
public class MetaApi : IMetaApi
{
    private readonly IAddonService _addonService;
    public MetaApi(IAddonService addonService)
    {
        _addonService = addonService;
    }

    public async Task<DetailDto> GetMetaDto(string type, string id)
    {
        var metaAddons = await _addonService.LoadAllMetaAddons();

        var detail = new DetailDto();

        if (metaAddons == null) return detail;
        foreach (var metaAddon in metaAddons)
        {
            var api = RestService.For<IStremioApi>(metaAddon.BaseUri);

            detail = await api.GetMeta(type, id);

            if (detail is { Meta: { } }) break;
        }

        return detail;
    }
}
