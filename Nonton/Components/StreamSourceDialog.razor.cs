using Microsoft.AspNetCore.Components;
using MudBlazor;
using Nonton.Dtos.Manifest;
using Nonton.Services;

namespace Nonton.Components
{
    public partial class StreamSourceDialog
    {
        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; } = null!;

        [Parameter] public string? Type { get; set; }
        [Parameter] public string? Id { get; set; }

        [Inject] public NavigationManager NavigationManager { get; set; } = null!;
        [Inject] public IAddonService AddonService { get; set; } = null!;

        public IEnumerable<Addon>? Addons { get; set; }
        
        protected override async Task OnParametersSetAsync()
        {
            Addons ??= await AddonService.LoadAllStreamAddons();
        }

        //protected override async Task OnInitializedAsync()
        //{
        //    Addons = await AddonService.LoadAllStreamAddons();
        //}

        private void PlayContent(string url)
        {
            MudDialog?.Close(DialogResult.Ok(url));
        }

        private void Close()
        {
            NavigationManager.NavigateTo($"detail/{Type}/{Id}");
        }
    }
}
