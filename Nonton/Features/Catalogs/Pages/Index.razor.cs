using Microsoft.AspNetCore.Components;
using Nonton.Features.Catalogs.Models;

namespace Nonton.Features.Catalogs.Pages;
public partial class Index
{
    [Inject] public ICatalogService CatalogService { get; set; } = null!;

    public IEnumerable<Catalog>? Catalogs { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var allCatalogs = await CatalogService.GetAllCatalogsAsync();

        Catalogs = allCatalogs.Where(c => !c.Genres.IsGenreRequired);

        StateHasChanged();
    }
}