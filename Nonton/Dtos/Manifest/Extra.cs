namespace Nonton.Dtos.Manifest;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Extra
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("options")]
    public List<object>? Options { get; set; }

    [JsonPropertyName("isRequired")]
    public bool? IsRequired { get; set; }

    [JsonPropertyName("optionsLimit")]
    public int? OptionsLimit { get; set; }
}