using System.Text.Json.Serialization;

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
}
