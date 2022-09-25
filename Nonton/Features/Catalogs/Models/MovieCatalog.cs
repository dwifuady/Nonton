using Nonton.Commons;

namespace Nonton.Features.Catalogs.Models;
public class MovieCatalog : Catalog
{
    public override CatalogTypeEnum CatalogType => CatalogTypeEnum.Movie;
    public override string CatalogTitle => AddonConstants.TypeMovieTitle;
    public override string CatalogShortName => AddonConstants.TypeMovieShortName;

    public MovieCatalog(string addonName, string addonBaseUri, string catalogId, string catalogName) : base(addonName, addonBaseUri, catalogId, catalogName)
    {
    }

    public MovieCatalog(string addonName, string addonBaseUri, string catalogId, string catalogName, (List<string>, bool) genres, (bool, bool) searchable, (bool, bool) skippable) : base(addonName, addonBaseUri, catalogId, catalogName, genres, searchable, skippable)
    {
    }
}
