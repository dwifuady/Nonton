﻿namespace Nonton.Dtos;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Discover
{
    [JsonPropertyName("metas")]
    public List<Meta>? Metas { get; set; }

    [JsonPropertyName("hasMore")]
    public bool HasMore { get; set; }

    [JsonPropertyName("cacheMaxAge")]
    public int CacheMaxAge { get; set; }

    [JsonPropertyName("staleRevalidate")]
    public int StaleRevalidate { get; set; }

    [JsonPropertyName("staleError")]
    public int StaleError { get; set; }

    [JsonIgnore] public string CatalogName { get; set; } = null!;
}