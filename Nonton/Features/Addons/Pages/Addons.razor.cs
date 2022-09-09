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
        [Inject] public ISnackbar Snackbar { get; set; } = null!;

        public IEnumerable<AddonDto>? AllAddons { get; set; }
        public IEnumerable<AddonDto>? AddonsCollection { get; set; }
        public IEnumerable<AddonDto>? FilteredAddonsCollection
        {
            get
            {
                return string.IsNullOrWhiteSpace(SearchString)
                    ? AddonsCollection
                    : AddonsCollection?.Where(a => a.Manifest!.Name!.ToLower().Contains(SearchString.ToLower()) || a.Manifest is
                    {
                        Description: { }
                    } && a.Manifest.Description.ToLower().Contains(SearchString));
            }
        }
        public string? SearchString { get; set; }
        public MudTextField<string>? SearchBox { get; set; }

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

            if (SearchBox != null) await SearchBox.FocusAsync();
        }

        private async Task InstallFromUrl()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true, CloseButton = true, MaxWidth = MaxWidth.Large };
            var dialog = DialogService.Show<AddNewAddon>("New addon", options);

            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                var url = result.Data.ToString();
                if (!string.IsNullOrWhiteSpace(url))
                {
                    var manifestString = await DownloadAddonManifest(url);
                    await SaveAddon(url, manifestString);
                    Snackbar.Add("Addon installed", Severity.Success);
                }
                else
                {
                    Snackbar.Add("Invalid addon manifest", Severity.Warning);
                }
            }
        }

        private async Task Install(string url)
        {
            if (!string.IsNullOrWhiteSpace(url))
            {
                var manifestString = await DownloadAddonManifest(url);
                await SaveAddon(url, manifestString);
                Snackbar.Add("Addon installed", Severity.Success);
            }
            else
            {
                Snackbar.Add("Invalid addon manifest", Severity.Warning);
            }
        }

        private async Task Install(AddonDto addon)
        {
            var confirmBox = await DialogService.ShowMessageBox(
                "Install",
                $"Install addon {addon!.Manifest!.Name}?",
                yesText: "Install",
                cancelText: "Cancel");

            if (confirmBox.HasValue && confirmBox.Value)
            {
                await Install(addon.TransportUrl!);
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

            var confirmBox = await DialogService.ShowMessageBox(
                "Warning",
                $"Delete addon {addon!.Manifest!.Name}?",
                yesText: "Delete",
                cancelText: "Cancel");

            if (confirmBox.HasValue && confirmBox.Value)
            {
                await AddonService.DeleteAddon(id);
            }

            Snackbar.Add("Addon Removed");
            await LoadAddons();
        }
    }
}
