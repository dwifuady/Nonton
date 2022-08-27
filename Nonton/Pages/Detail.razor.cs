using Microsoft.AspNetCore.Components;
using MudBlazor;
using Nonton.Components;
using Nonton.Dtos;
using Nonton.Services;

namespace Nonton.Pages
{
    public partial class Detail
    {
        [Parameter] public string Id { get; set; } = null!;
        [Parameter] public string Type { get; set; } = null!;

        [Inject] public IDialogService DialogService { get; set; } = null!;
        [Inject] public IMetaService MetaService { get; set; } = null!;
        [Inject] public IStreamService StreamService { get; set; } = null!;
        [Inject] public NavigationManager NavigationManager { get; set; } = null!;

        public Meta? ContentMeta { get; set; }

        private readonly DialogOptions _trailerDialogOptions = new() { MaxWidth = MaxWidth.Medium, FullWidth = true, NoHeader = false, FullScreen = false, CloseOnEscapeKey = true, CloseButton = true };
        
        protected override async Task OnParametersSetAsync()
        {
            var detail = await MetaService.GetMeta(Type, Id);
            ContentMeta = detail.Meta;

            StateHasChanged();
        }

        private void Watch(string id)
        {
            NavigationManager.NavigateTo($"watch/{id}");
        }

        private void PlayTrailer()
        {
            var dialogParameters = new DialogParameters
        {
            { "YoutubeId", ContentMeta?.TrailerStreams?.FirstOrDefault()?.YtId }
        };

            DialogService.Show<YoutubePopup>($"{ContentMeta?.Name} ({ContentMeta?.Year}) | Trailer", dialogParameters, _trailerDialogOptions);
        }
    }
}
