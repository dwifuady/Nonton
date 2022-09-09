using System.Text.Json.Serialization;

namespace Nonton.Features.Addons.Dtos.Manifest
{
    public class ResourceDto
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("types")]
        public List<string>? Types { get; set; }

        [JsonPropertyName("idPrefixes")]
        public List<string>? IdPrefixes { get; set; }
    }
}
