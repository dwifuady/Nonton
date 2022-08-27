using Microsoft.AspNetCore.Components;
using MudBlazor;
using Nonton.Components;
using Nonton.Services;

namespace Nonton.Pages
{
    public partial class Watch
    {
        [Parameter] public string? Id { get; set; }

        [Inject] public IMetaService MetaService { get; set; } = null!;
        [Inject] public IStreamService StreamService { get; set; } = null!;
        [Inject] public IDialogService DialogService { get; set; } = null!;

        public string? ContentUrl { get; set; }

        private readonly DialogOptions _sourceSelectDialogOptions = new() { MaxWidth = MaxWidth.Large, FullWidth = true, NoHeader = true, FullScreen = false, CloseOnEscapeKey = false, CloseButton = false, DisableBackdropClick = true};

        protected override async Task OnParametersSetAsync()
        {
            await SelectSource();
        }

        private async Task SelectSource()
        {
            var dialogParameters = new DialogParameters
            {
                { "Id", Id }
            };

            var dialog = DialogService.Show<StreamSourceDialog>($"Select source", dialogParameters, _sourceSelectDialogOptions);
            
            var result = await dialog.Result;

            if (!result.Cancelled && !string.IsNullOrWhiteSpace(result.Data.ToString()))
            {
                PlayContent(result.Data.ToString()!);
                dialog.Close();
            }
        }

        private void PlayContent(string url)
        {
            ContentUrl = url;
            //ContentUrl = "https://lamberta.github.io/html5-animation/examples/ch04/assets/movieclip.mp4";
            StateHasChanged();
        }
    }
}
