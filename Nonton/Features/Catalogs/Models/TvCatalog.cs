using Nonton.Commons;

namespace Nonton.Features.Catalogs.Models;
public class TvCatalog : Catalog
{
    public override CatalogTypeEnum CatalogType => CatalogTypeEnum.Tv;
    public override string CatalogTitle => AddonConstants.AddonTypeTvTitle;
    public override string CatalogShortName => AddonConstants.AddonTypeTvShortName;

    public TvCatalog(string addonName, string addonBaseUri, string catalogId, string catalogName) : base(addonName, addonBaseUri, catalogId, catalogName)
    {
    }

    public TvCatalog(string addonName, string addonBaseUri, string catalogId, string catalogName, (List<string>, bool) genres, (bool, bool) searchable, (bool, bool) skippable) : base(addonName, addonBaseUri, catalogId, catalogName, genres, searchable, skippable)
    {
    }
}
