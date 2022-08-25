using Microsoft.AspNetCore.Components;
using MudBlazor;
using Nonton.Components;
using Nonton.Dtos;
using Nonton.Services;

namespace Nonton.Pages
{
    public partial class Detail
    {
        [Parameter]
        public string Id { get; set; } = null!;

        [Inject] public IDialogService DialogService { get; set; } = null!;
        [Inject] public IMetaService MetaService { get; set; } = null!;
        [Inject] public IStreamService StreamService { get; set; } = null!;

        public Meta? ContentMeta { get; set; }
        public IEnumerable<StreamResponse>? StreamResponses { get; set; }
        public bool IsPlaying { get; set; }
        public string? ContentUrl { get; set; }

        private bool _openDrawer = false;

        private readonly DialogOptions _trailerDialogOptions = new() { MaxWidth = MaxWidth.Medium, FullWidth = true, NoHeader = false, FullScreen = false, CloseOnEscapeKey = true, CloseButton = true };
        
        public LoadingContainerState LoadingStateStreamSource { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var detail = await MetaService.GetMovieMeta(Id);
            ContentMeta = detail.Meta;

            StateHasChanged();
        }

        private async Task SelectSource()
        {
            _openDrawer = true;
            if (string.IsNullOrWhiteSpace(ContentMeta?.ImdbId))
            {
                LoadingStateStreamSource = LoadingContainerState.Error;
                return;
            }

            LoadingStateStreamSource = LoadingContainerState.Loading;

            try
            {
                StreamResponses = await StreamService.GetStream(ContentMeta.ImdbId);
                
                if (StreamResponses is not null && StreamResponses.Any())
                {
                    LoadingStateStreamSource = LoadingContainerState.Loaded;
                }
                else
                {
                    LoadingStateStreamSource = LoadingContainerState.Empty;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                LoadingStateStreamSource = LoadingContainerState.Error;
            }
        }

        private void PlayTrailer()
        {
            var dialogParameters = new DialogParameters
        {
            { "YoutubeId", ContentMeta?.TrailerStreams?.FirstOrDefault()?.YtId }
        };

            DialogService.Show<YoutubePopup>($"{ContentMeta?.Name} ({ContentMeta?.Year}) | Trailer", dialogParameters, _trailerDialogOptions);
        }

        private void PlayContent(string url)
        {
            IsPlaying = true;
            _openDrawer = false;
            ContentUrl = url;
            StateHasChanged();
        }
    }
}
