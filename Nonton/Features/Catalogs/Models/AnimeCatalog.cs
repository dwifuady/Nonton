using Nonton.Commons;

namespace Nonton.Features.Catalogs.Models;
public class AnimeCatalog : Catalog
{
    public override CatalogTypeEnum CatalogType => CatalogTypeEnum.Anime;
    public override string CatalogTitle => AddonConstants.AddonTypeAnimeTitle;
    public override string CatalogShortName => AddonConstants.AddonTypeAnimeShortName;

    public AnimeCatalog(string addonName, string addonBaseUri, string catalogId, string catalogName) : base(addonName, addonBaseUri, catalogId, catalogName)
    {
    }

    public AnimeCatalog(string addonName, string addonBaseUri, string catalogId, string catalogName, (List<string>, bool) genres, (bool, bool) searchable, (bool, bool) skippable) : base(addonName, addonBaseUri, catalogId, catalogName, genres, searchable, skippable)
    {
    }
}
