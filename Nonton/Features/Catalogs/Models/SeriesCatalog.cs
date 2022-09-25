using Nonton.Commons;

namespace Nonton.Features.Catalogs.Models;
public class SeriesCatalog : Catalog
{
    public override CatalogTypeEnum CatalogType => CatalogTypeEnum.Series;
    public override string CatalogTitle => AddonConstants.TypeSeriesTitle;
    public override string CatalogShortName => AddonConstants.TypeSeriesShortName;

    public SeriesCatalog(string addonName, string addonBaseUri, string catalogId, string catalogName) : base(addonName, addonBaseUri, catalogId, catalogName)
    {
    }

    public SeriesCatalog(string addonName, string addonBaseUri, string catalogId, string catalogName, (List<string>, bool) genres, (bool, bool) searchable, (bool, bool) skippable) : base(addonName, addonBaseUri, catalogId, catalogName, genres, searchable, skippable)
    {
    }
}