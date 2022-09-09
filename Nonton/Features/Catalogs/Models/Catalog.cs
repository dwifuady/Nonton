using Nonton.Commons;

namespace Nonton.Features.Catalogs.Models;
public class Catalog : ICatalog
{
    public virtual string CatalogType => AddonConstants.TypeDefault;
    public Catalog(string addonName, string addonBaseUri, string catalogId, string catalogName)
    {
        AddonName = addonName;
        AddonBaseUri = addonBaseUri;
        CatalogId = catalogId;
        CatalogName = catalogName;
    }

    public Catalog(string addonName, string addonBaseUri, string catalogId, string catalogName, (List<string>, bool) genres, (bool, bool) searchable, (bool, bool) skippable)
    {
        AddonName = addonName;
        AddonBaseUri = addonBaseUri;
        CatalogId = catalogId;
        CatalogName = catalogName;
        Genres = genres;
        Searchable = searchable;
        Skippable = skippable;
    }

    public string AddonName { get; }
    public string AddonBaseUri { get; }
    public string CatalogId { get; }
    public string CatalogName { get; }
    public (List<string>? Genres, bool IsGenreRequired) Genres { get; }
    public (bool IsSearchable, bool IsSearchRequired) Searchable { get; }
    public (bool IsSkippable, bool IsSkipRequired) Skippable { get; }
}