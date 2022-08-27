using System.Text.Json.Serialization;
using Nonton.Commons;

namespace Nonton.Dtos.Manifest
{
    public class Addon
    {
        [JsonPropertyName("manifest")]
        public Manifest? Manifest { get; set; }

        [JsonPropertyName("transportUrl")] 
        public string? TransportUrl { get; set; }

        [JsonIgnore] 
        public string BaseUri => string.IsNullOrWhiteSpace(TransportUrl) ? string.Empty : TransportUrl.Replace("/manifest.json", "");

        [JsonIgnore] public bool IsDefaultAddon { get; set; }
    }

    public static class AddonExtension
    {
        public static IEnumerable<Catalog>? GetDefaultCatalogs(this Addon addon)
        {
            if (addon.Manifest?.Catalogs != null && addon.Manifest.Catalogs.Any())
            {
                return addon.Manifest.Catalogs
                    .Where(x =>
                        x.Type is AddonConstants.TypeMovie or AddonConstants.TypeSeries &&
                        ((x.ExtraRequired != null && !x.ExtraRequired.Any()) || x.ExtraRequired is null));
            }

            return null;
        }
    }
}