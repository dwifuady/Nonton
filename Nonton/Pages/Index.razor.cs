using Nonton.Dtos;
using Nonton.Services;
using Microsoft.AspNetCore.Components;
using Nonton.Dtos.Manifest;

namespace Nonton.Pages
{
    public partial class Index
    {
        [Inject] public ICatalogService CatalogService { get; set; } = null!;
        [Inject] public IAddonService AddonService { get; set; } = null!;
        
        public Discover? DiscoverResult { get; set; }
        public IEnumerable<Addon>? Addons { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Addons = await AddonService.LoadAllCatalogAddons();
            await LoadDefaultDiscover();
            StateHasChanged();
        }

        private async Task LoadDefaultDiscover()
        {
            if (Addons is null) return;
            
            var defaultAddon = Addons.FirstOrDefault();
            var defaultCatalog = defaultAddon?.Manifest?.Catalogs?.FirstOrDefault(x => (x.ExtraRequired != null && !x.ExtraRequired.Any()) || x.ExtraRequired is null);


            if (defaultAddon != null && defaultCatalog != null)
            {
                DiscoverResult = await CatalogService.GetMoviesByCatalog(defaultAddon, defaultCatalog.Id!);
                DiscoverResult.CatalogName = defaultCatalog.Name!;
            }
        }
    }
}
