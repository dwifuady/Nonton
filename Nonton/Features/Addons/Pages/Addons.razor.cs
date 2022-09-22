using System.Text.Json;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Nonton.Components;
using Nonton.Features.Addons.Dtos.Manifest;

namespace Nonton.Features.Addons.Pages
{
    public partial class Addons
    {
        [Inject] public IAddonService AddonService { get; set; } = null!;
        [Inject] public IDialogService DialogService { get; set; } = null!;
        [Inject] public HttpClient HttpClient { get; set; } = null!;

        public IEnumerable<AddonDto>? AllInstalledAddons { get; set; }
        public IEnumerable<AddonDto>? AddonsCollection { get; set; }
        public IEnumerable<AddonDto>? FilteredAddonsCollection
        {
            get
            {
                if (SearchString is not null && SearchString.Contains("manifest.json"))
                {
                    InstallFromUrl(SearchString);
                    SearchString = string.Empty;
                }
                return string.IsNullOrWhiteSpace(SearchString)
                    ? AddonsCollection
                    : AddonsCollection?.Where(a => a.Manifest!.Name!.ToLower().Contains(SearchString.ToLower()) || a.Manifest is
                    {
                        Description: { }
                    } && a.Manifest.Description.ToLower().Contains(SearchString));
            }
        }

        public string? SearchString { get; set; }
        
        private bool _isInstalledSelected = true;
        private bool _isCommunitySelected;
        private bool _showConfirmBox = false;

        private string _confirmBoxTitle = "";
        private string _confirmBoxDescription = "";
        private string _confirmBoxAddonUrl = "";
        private string _confirmBoxAddonId = "";
        private AddonConfirmBoxTypeEnum _confirmBoxType;
        public enum AddonConfirmBoxTypeEnum
        {
            Install,
            Uninstall
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadAddons();
        }

        private async Task LoadAddons()
        {
            AllInstalledAddons = await AddonService.LoadAllAddons();
            StateHasChanged();
        }

        private async Task BrowseAddons()
        {
            _isCommunitySelected = true;
            _isInstalledSelected = false;
            AddonsCollection ??= await AddonService.GetAddonCollection();
        }

        private void LoadInstalledAddons()
        {
            _isCommunitySelected = false;
            _isInstalledSelected = true;
        }

        private void InstallFromUrl(string url)
        {
            _confirmBoxTitle = "Install";
            _confirmBoxDescription = $"Install addon from {url}?";
            _showConfirmBox = true;
            _confirmBoxAddonUrl = url;
            _confirmBoxType = AddonConfirmBoxTypeEnum.Install;
            //var options = new DialogOptions { CloseOnEscapeKey = true, CloseButton = true, MaxWidth = MaxWidth.Large };
            //var dialog = DialogService.Show<AddNewAddon>("New addon", options);

            //var result = await dialog.Result;

            //if (!result.Cancelled)
            //{
            //    var url = result.Data.ToString();
            //    if (!string.IsNullOrWhiteSpace(url))
            //    {
            //        var manifestString = await DownloadAddonManifest(url);
            //        await SaveAddon(url, manifestString);

            //    }
            //}
        }

        private async Task Install(string url)
        {
            if (!string.IsNullOrWhiteSpace(url))
            {
                var manifestString = await DownloadAddonManifest(url);
                await SaveAddon(url, manifestString);
                _showConfirmBox = false;
            }

            await LoadAddons();
            //todo add warning/error message
        }

        private void Install(AddonDto addon)
        {
            _confirmBoxTitle = "Install";
            _confirmBoxDescription = $"Install addon {addon!.Manifest!.Name}?";
            _showConfirmBox = true;
            _confirmBoxAddonUrl = addon!.TransportUrl;
            _confirmBoxType = AddonConfirmBoxTypeEnum.Install;
        }

        private async Task Uninstall(string addonId)
        {
            await AddonService.DeleteAddon(addonId);
            _showConfirmBox = false;
            await LoadAddons();
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

        private void DeleteAddon(string id)
        {
            _confirmBoxTitle = "Uninstall";
            _confirmBoxDescription = $"Uninstall addon {id}?";
            _showConfirmBox = true;
            _confirmBoxAddonId = id;
            _confirmBoxType = AddonConfirmBoxTypeEnum.Uninstall;
        }
    }
}
