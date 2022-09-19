using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Nonton.Features.Catalogs.Models;

namespace Nonton.Features.Catalogs.Pages;

public partial class Search : IDisposable
{
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;
    [Inject] public ICatalogService CatalogService { get; set; } = null!;

    public IEnumerable<Catalog>? Catalogs { get; set; }
    public string? Keywords { get; set; }
    public SearchModel SearchModel { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        Catalogs = await CatalogService.GetSearchableCatalogAsync();

        NavigationManager.LocationChanged += NavigationManager_LocationChanged!;
        ParseQueryString();
    }
    private void NavigationManager_LocationChanged(object sender, LocationChangedEventArgs e)
    {
        ParseQueryString();
    }

    private void ParseQueryString()
    {
        var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
        var queryStrings = QueryHelpers.ParseQuery(uri.Query);
        if (queryStrings.TryGetValue("q", out var keyword))
        {
            Keywords = keyword;
        }

        StateHasChanged();
    }

    private void DoSearch()
    {
        NavigationManager.NavigateTo($"search?q={SearchModel.SearchString}");
    }

    void IDisposable.Dispose() => NavigationManager.LocationChanged -= NavigationManager_LocationChanged!;
}

public class SearchModel
{
    public string? SearchString { get; set; }

}