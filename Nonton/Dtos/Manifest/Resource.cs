using System.Text.Json.Serialization;

namespace Nonton.Dtos.Manifest
{
    public class Resource
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("types")]
        public List<string>? Types { get; set; }

        [JsonPropertyName("idPrefixes")]
        public List<string>? IdPrefixes { get; set; }
    }
}
