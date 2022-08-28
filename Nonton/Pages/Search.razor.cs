using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Nonton.Dtos.Manifest;
using Nonton.Services;

namespace Nonton.Pages
{
    public partial class Search : IDisposable
    {
        [Inject] public NavigationManager NavigationManager { get; set; } = null!;
        [Inject] public IAddonService AddonService { get; set; } = null!;
        public IEnumerable<Addon>? Addons { get; set; }

        public string? Keywords { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Addons = await AddonService.LoadAllCatalogAddons();
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
        
        void IDisposable.Dispose() => NavigationManager.LocationChanged -= NavigationManager_LocationChanged!;
    }
}
