using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Extensions;
using Nonton.Features.Addons.Enums;
using Nonton.Features.Catalogs.Models;

namespace Nonton.Features.Catalogs.Components
{
    public partial class Discover
    {
        [Parameter] public AddonContentTypeEnum ContentType { get; set; }

        [Inject] public ICatalogService CatalogService { get; set; } = null!;
        [Inject] public ICatalogApi CatalogApi { get; set; } = null!;
        [Inject] public NavigationManager NavigationManager { get; set; } = null!;
        public List<Catalog>? Catalogs { get; set; }
        public MudChip? SelectedCatalogChip { get; set; }
        public MudChip? SelectedGenreChip { get; set; }
        public Catalog? SelectedCatalog { get; set; }
        public (List<string> Genres, bool IsRequired)? Genres { get; set; }
        public string? SelectedGenres { get; set; }
        public IEnumerable<Addons.Dtos.Meta>? Metas { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            Catalogs = ContentType switch
            {
                AddonContentTypeEnum.Movie => await CatalogService.GetMovieCatalogsAsync(),
                AddonContentTypeEnum.Series => await CatalogService.GetSeriesCatalogsAsync(),
                _ => throw new ArgumentOutOfRangeException()
            };

            CatalogChanged();
        }
        
        private void CatalogChanged()
        {
            Genres = null;
            var catalog = SelectedCatalogChip?.Value.As<Catalog>();

            if (catalog?.Genres.Genres == null)
            {
                catalog = Catalogs?.FirstOrDefault();
            }
            
            if (catalog?.Genres == null || catalog?.Genres.Genres == null) return;
            
            SelectedGenres = catalog.Genres.IsGenreRequired ? catalog.Genres.Genres.FirstOrDefault() : "";
            Genres = catalog.Genres!;
            SelectedCatalog = catalog;
            Task.FromResult(LoadMeta());
            StateHasChanged();
        }

        private async Task LoadMeta()
        {
            if (SelectedCatalog is not null)
            {
                var discoverItem = await CatalogApi.GetDiscoverItem(SelectedCatalog.AddonBaseUri, SelectedCatalog.CatalogType, SelectedCatalog.CatalogId, SelectedGenres ?? "");
                Metas = discoverItem.Metas;
                StateHasChanged();
            }
        }

        private async Task GenreChanged()
        {
            await LoadMeta();
        }

        public async Task ViewDetail(string id)
        {
            await Task.Run(() => NavigationManager.NavigateTo($"detail/{ContentType.GetValue()}/{id}"));
        }
    }
}