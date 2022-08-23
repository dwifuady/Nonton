using Nonton.Dtos;
using Nonton.Services;
using Microsoft.AspNetCore.Components;
using Nonton.Commons;
using Nonton.Dtos.Manifest;

namespace Nonton.Pages
{
    public partial class Index
    {
        [Inject] public ICatalogService CatalogService { get; set; } = null!;
        [Inject] public IAddonService AddonService { get; set; } = null!;

        public List<Discover> Discovers { get; set; } = new();

        public IEnumerable<Addon>? Addons { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Addons = await AddonService.LoadAllCatalogAddons();
            await LoadDiscovers();
            StateHasChanged();
        }

        private async Task LoadDiscovers()
        {
            if (Addons is null)
            {
                return;
            }
            var defaultAddon = Addons!.FirstOrDefault();
            var catalogs = defaultAddon?.Manifest?.Catalogs?.Where(x => x.Type == AddonConstants.TypeMovie && ((x.ExtraRequired != null && !x.ExtraRequired.Any()) || x.ExtraRequired is null));
            
            if (defaultAddon != null && catalogs != null)
            {
                foreach (var catalog in catalogs)
                {
                    var discover = await CatalogService.GetMoviesByCatalog(defaultAddon, catalog.Id!);
                    discover.CatalogName = catalog.Name!;
                    Discovers.Add(discover);
                }
            }
        }
    }
}