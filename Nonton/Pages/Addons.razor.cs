using Microsoft.AspNetCore.Components;
using MudBlazor;
using Nonton.Components;
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
        
        protected override async Task OnInitializedAsync()
        {
            await LoadAddons();
        }

        private async Task LoadAddons()
        {
            AllAddons = await AddonService.LoadAllAddons();
            StateHasChanged();
        }

        private async Task OpenAddNewAddonDialog()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true, CloseButton = true, MaxWidth = MaxWidth.Large};
            var dialog = DialogService.Show<AddNewAddon>("New addon", options);

            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                var url = result.Data.ToString();
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

            bool? result = await DialogService.ShowMessageBox(
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
