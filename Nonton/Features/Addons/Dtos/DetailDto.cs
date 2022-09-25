namespace Nonton.Features.Addons.Dtos;
using System.Text.Json.Serialization;

public class DetailDto
{
    [JsonPropertyName("meta")] public MetaDto? Meta { get; set; }
}
