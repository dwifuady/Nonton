using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using Nonton.Dtos.Manifest;
using Nonton.Services;

namespace Nonton.Pages
{
    public partial class Addons
    {
        [Inject] public IAddonService AddonService { get; set; } = null!;
        [Inject] public IDialogService DialogService { get; set; } = null!;
        [Inject] public HttpClient HttpClient { get; set; } = null!;
        
        public IEnumerable<Addon>? AllAddons { get; set; }
        public IEnumerable<Addon>? AddonsCollection { get; set; }
        public IEnumerable<Addon>? FilteredAddonsCollection 
        {
            get
            {
                return string.IsNullOrWhiteSpace(SearchString)
                    ? AddonsCollection
                    : AddonsCollection?.Where(a => a.Manifest!.Name!.ToLower().Contains(SearchString.ToLower()) || (a.Manifest is
                    {
                        Description: { }
                    } && a.Manifest.Description.ToLower().Contains(SearchString)));
            }
        }
        public string? SearchString { get; set; }

        private bool _openAddonBrowser = false;
        
        protected override async Task OnInitializedAsync()
        {
            await LoadAddons();
        }

        private async Task LoadAddons()
        {
            AllAddons = await AddonService.LoadAllAddons();
            StateHasChanged();
        }

        private async Task BrowseAddons()
        {
            _openAddonBrowser = true;
            AddonsCollection ??= await AddonService.GetAddonCollection();
        }

        private void SearchOrInstall()
        {
            if (!string.IsNullOrWhiteSpace(SearchString) && SearchString.EndsWith("/manifest.json"))
            {
                Install(SearchString);
            }
        }

        private async Task Install(string url)
        {;
            if (!string.IsNullOrWhiteSpace(url))
            {
                var manifestString = await DownloadAddonManifest(url);
                Console.WriteLine("Saving Addon");
                await SaveAddon(url, manifestString);
            }
            else
            {
                Console.WriteLine("Url is empty");
            }
        }

        private async Task<string> DownloadAddonManifest(string url)
        {
            try
            {
                var manifestString = await HttpClient.GetStringAsync(url);
                return manifestString;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "";
            }
        }

        private async Task SaveAddon(string url, string manifestString)
        {
            await AddonService.SaveAddon(url, manifestString);
            await LoadAddons();
        }

        private async Task DeleteAddon(string id)
        {
            var addon = AllAddons!.SingleOrDefault(x => x.Manifest!.Id!.Equals(id));
            
            var result = await DialogService.ShowMessageBox(
                "Warning",
                $"Delete addon {addon!.Manifest!.Name}?",
                yesText: "Delete",
                cancelText: "Cancel");

            if (result.HasValue && result.Value)
            {
                await AddonService.DeleteAddon(id);
            }
            
            await LoadAddons();
        }
    }
}
