using Nonton.Services;
using Microsoft.AspNetCore.Components;
using Nonton.Dtos.Manifest;

namespace Nonton.Pages
{
    public partial class Index
    {
        [Inject] public IAddonService AddonService { get; set; } = null!;

        public IEnumerable<Addon>? Addons { get; set; }
        
        protected override async Task OnInitializedAsync()
        {
            Addons = await AddonService.LoadAllCatalogAddons();
            StateHasChanged();
        }
    }
}