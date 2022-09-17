namespace Nonton.Features.Addons.Dtos;
using System.Text.Json.Serialization;

public class Detail
{
    [JsonPropertyName("meta")] public Meta? Meta { get; set; }
}
