using System.Text.Json.Serialization;
using Nonton.Commons;

namespace Nonton.Features.Addons.Dtos.Manifest
{
    public class AddonDto
    {
        [JsonPropertyName("manifest")] public ManifestDto Manifest { get; set; } = null!;

        [JsonPropertyName("transportUrl")] public string TransportUrl { get; set; } = null!;

        [JsonPropertyName("transportName")]
        public string? TransportName { get; set; }

        [JsonIgnore] 
        public string BaseUri => string.IsNullOrWhiteSpace(TransportUrl)
            ? string.Empty
            : TransportUrl.Replace("/manifest.json", "");

        [JsonIgnore] public bool IsDefaultAddon { get; set; }
    }

    public static class AddonExtension
    {
        public static IEnumerable<CatalogDto>? GetDefaultCatalogs(this AddonDto addon)
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

        public static IEnumerable<CatalogDto>? GetSearchableCatalogs(this AddonDto addon)
        {
            if (addon.Manifest?.Catalogs != null && addon.Manifest.Catalogs.Any())
            {
                return addon.Manifest.Catalogs
                    .Where(x =>
                        x.Type is AddonConstants.TypeMovie or AddonConstants.TypeSeries &&
                        ((x.ExtraSupported != null && x.ExtraSupported.Any() && x.ExtraSupported.Contains(AddonConstants.ExtraSearch)) ));
            }

            return null;
        }
    }
}