using Microsoft.AspNetCore.Components;
using Nonton.Features.Catalogs.Models;

namespace Nonton.Features.Catalogs.Pages;
public partial class Index
{
    [Inject] public ICatalogService CatalogService { get; set; } = null!;

    public IEnumerable<Catalog>? Catalogs { get; set; }
    public Catalog? CarouselCatalog { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var catalogs = await CatalogService.GetDefaultCatalogAsync();
        CarouselCatalog = catalogs.FirstOrDefault();
        Catalogs = catalogs.Where(c => c != CarouselCatalog);
        StateHasChanged();
    }
}