using Nonton.Features.Catalogs.Models;

namespace Nonton.Features.Catalogs;
public interface ICatalogService
{
    Task<IEnumerable<Catalog>> GetAllCatalogsAsync();
    Task<IEnumerable<Catalog>> GetDefaultCatalogAsync();
    Task<IEnumerable<Catalog>> GetSearchableCatalogAsync();
}
