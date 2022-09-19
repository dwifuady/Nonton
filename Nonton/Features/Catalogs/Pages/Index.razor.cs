using Microsoft.AspNetCore.Components;
using Nonton.Features.Catalogs.Models;
using Nonton.Features.PageState;

namespace Nonton.Features.Catalogs.Pages;
public partial class Index
{
    [Inject] public ICatalogService CatalogService { get; set; } = null!;
    [Inject] public PageHistoryState PageHistoryState { get; set; } = null!;

    public IEnumerable<Catalog>? Catalogs { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Catalogs = await CatalogService.GetDefaultCatalogAsync();
        PageHistoryState.AddPageToHistory("/");
        StateHasChanged();
    }
}