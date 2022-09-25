using Nonton.Commons;

namespace Nonton.Features.Catalogs.Models;

public class GamesCatalog : Catalog
{
    public override CatalogTypeEnum CatalogType => CatalogTypeEnum.Games;
    public override string CatalogTitle => AddonConstants.TypeGamesTitle;
    public override string CatalogShortName => AddonConstants.TypeGameShortName;
    public GamesCatalog(string addonName, string addonBaseUri, string catalogId, string catalogName) : base(addonName, addonBaseUri, catalogId, catalogName)
    {
    }

    public GamesCatalog(string addonName, string addonBaseUri, string catalogId, string catalogName, (List<string>, bool) genres, (bool, bool) searchable, (bool, bool) skippable) : base(addonName, addonBaseUri, catalogId, catalogName, genres, searchable, skippable)
    {
    }
}
