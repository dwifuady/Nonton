using System.Text.Json.Serialization;

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
}