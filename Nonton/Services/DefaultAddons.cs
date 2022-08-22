using System.Text.Json;
using Nonton.Dtos.Manifest;
using Nonton.Addons;

namespace Nonton.Services
{
    public static class DefaultAddons
    {
        public static IEnumerable<(string TransportUrl, string AddonManifest)> DefaultAddonsDictionary =>
            new List<(string TransportUrl, string AddonManifest)>
            {
                  new("https://v3-cinemeta.strem.io/manifest.json", DefaultAddonsManifest.Cinemeta)
            };

        public static IEnumerable<Addon>? AllDefaultAddons()
        {
            var defaultAddonsDict = DefaultAddonsDictionary;

            var defaultAddons = new List<Addon>();
            foreach (var (transportUrl, addonManifest) in defaultAddonsDict)
            {
                var manifest = JsonSerializer.Deserialize<Manifest>(addonManifest); 
                        
                if (manifest is null) return null;

                defaultAddons.Add(new Addon
                {
                    TransportUrl = transportUrl,
                    Manifest = manifest
                });
            }

            return defaultAddons;
        }
    }
}
