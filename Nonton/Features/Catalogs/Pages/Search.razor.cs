using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Nonton.Features.Catalogs.Models;

namespace Nonton.Features.Catalogs.Pages;

public partial class Search : IDisposable
{
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;
    [Inject] public ICatalogService CatalogService { get; set; } = null!;

    public IEnumerable<Catalog>? SearchableCatalogs { get; set; }
    public string? Keywords { get; set; }
    public SearchModel SearchModel { get; set; } = new();

    public IEnumerable<Catalog>? Catalogs { get; set; }
    public CatalogTypeEnum SelectedType { get; set; } = CatalogTypeEnum.Movie;
    public string? SelectedCatalogId { get; set; }
    public Catalog? SelectedCatalog { get; set; }
    public string? SelectedGenre { get; set; }

    protected override async Task OnInitializedAsync()
    {
        SearchableCatalogs = await CatalogService.GetSearchableCatalogAsync();

        NavigationManager.LocationChanged += NavigationManager_LocationChanged!;
        ParseQueryString();

        await LoadCatalogs();
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
            SearchModel.SearchString = keyword;
        }

        StateHasChanged();
    }

    private void DoSearch()
    {
        NavigationManager.NavigateTo($"search?q={SearchModel.SearchString}");
    }

    void IDisposable.Dispose() => NavigationManager.LocationChanged -= NavigationManager_LocationChanged!;

    private async Task LoadCatalogs()
    {
        Catalogs = await CatalogService.GetCatalogsByType(SelectedType);
        SelectedCatalog = Catalogs.FirstOrDefault();
        SelectedCatalogId = SelectedCatalog?.CatalogId;
        SetDefaultGenre();
        StateHasChanged();
    }

    private async Task SelectedTypeChanged(ChangeEventArgs args)
    {
        if (!string.IsNullOrWhiteSpace(args.Value?.ToString()))
        {
            Enum.TryParse(args.Value.ToString(), false, out CatalogTypeEnum catalogType);
            SelectedType = catalogType;
            await LoadCatalogs();
        }
    }

    private void SelectedCatalogChanged(ChangeEventArgs args)
    {
        if (string.IsNullOrWhiteSpace(args.Value?.ToString())) return;

        SelectedCatalogId = args.Value.ToString();
        if (Catalogs == null) return;

        SelectedCatalog = Catalogs.FirstOrDefault(c => c.CatalogId == SelectedCatalogId);
        SetDefaultGenre();
    }

    private void SetDefaultGenre()
    {
        if (SelectedCatalog is { Genres.IsGenreRequired: false })
        {
            SelectedGenre = "";
        }
        else
        {
            if (SelectedCatalog?.Genres.Genres != null) 
                SelectedGenre = SelectedCatalog.Genres.Genres.FirstOrDefault();
        }
    }
}

public class SearchModel
{
    public string? SearchString { get; set; }

}