using System.Text.Json;
using Nonton.Features.Addons.Dtos.Manifest;

namespace Nonton.Features.Addons;
public static class DefaultAddons
{
    public static IEnumerable<(string TransportUrl, string AddonManifest)> DefaultAddonsDictionary =>
        new List<(string TransportUrl, string AddonManifest)>
        {
                new("https://v3-cinemeta.strem.io/manifest.json", DefaultAddonsManifest.Cinemeta),
                new("https://watchhub.strem.io/manifest.json", DefaultAddonsManifest.WatchHub)
        };

    public static IEnumerable<AddonDto>? AllDefaultAddons()
    {
        var defaultAddonsDict = DefaultAddonsDictionary;

        var defaultAddons = new List<AddonDto>();
        foreach (var (transportUrl, addonManifest) in defaultAddonsDict)
        {
            var manifest = JsonSerializer.Deserialize<ManifestDto>(addonManifest);

            if (manifest is null) return null;

            defaultAddons.Add(new AddonDto
            {
                TransportUrl = transportUrl,
                Manifest = manifest,
                IsDefaultAddon = true
            });
        }

        return defaultAddons;
    }
}
