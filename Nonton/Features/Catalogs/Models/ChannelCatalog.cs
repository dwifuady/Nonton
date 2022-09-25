using Nonton.Commons;

namespace Nonton.Features.Catalogs.Models;
public class ChannelCatalog : Catalog
{
    public override CatalogTypeEnum CatalogType => CatalogTypeEnum.Channel;
    public override string CatalogTitle => AddonConstants.TypeChannelTitle;
    public override string CatalogShortName => AddonConstants.TypeChannelShortName;

    public ChannelCatalog(string addonName, string addonBaseUri, string catalogId, string catalogName) : base(addonName, addonBaseUri, catalogId, catalogName)
    {
    }

    public ChannelCatalog(string addonName, string addonBaseUri, string catalogId, string catalogName, (List<string>, bool) genres, (bool, bool) searchable, (bool, bool) skippable) : base(addonName, addonBaseUri, catalogId, catalogName, genres, searchable, skippable)
    {
    }
}