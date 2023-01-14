using Nonton.Features.Catalogs.Models;

namespace Nonton.Features.Catalogs;
public interface ICatalogService
{
    Task<IEnumerable<Catalog>> GetAllCatalogsAsync();
    Task<IReadOnlyList<Catalog>> GetDefaultCatalogAsync();
    Task<IEnumerable<Catalog>> GetSearchableCatalogAsync();
    Task<List<Catalog>> GetCatalogsByType(CatalogTypeEnum catalogType);
    Task<List<Catalog>> GetMovieCatalogsAsync();
    Task<List<Catalog>> GetSeriesCatalogsAsync();
    Task<List<Catalog>> GetAnimeCatalogsAsync();
    Task<List<Catalog>> GetChannelCatalogsAsync();
    Task<List<Catalog>> GetTvCatalogsAsync();
}
