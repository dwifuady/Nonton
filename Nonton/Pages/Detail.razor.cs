using Microsoft.AspNetCore.Components;
using MudBlazor;
using Nonton.Api;
using Nonton.Components;
using Nonton.Dtos;
using Nonton.Services;
using Refit;

namespace Nonton.Pages
{
    public partial class Detail
    {
        [Parameter]
        public string Id { get; set; } = null!;

        [Inject] public IDialogService DialogService { get; set; } = null!;
        [Inject] public IMetaService MetaService { get; set; } = null!;

        public static IStremioApi StreamApi => RestService.For<IStremioApi>("https://watchhub.strem.io");
        public Meta? ContentMeta { get; set; }
        public StreamResponse? StreamResponse { get; set; }

        private bool _openDrawer = false;

        private readonly DialogOptions _trailerDialogOptions = new() { MaxWidth = MaxWidth.Medium, FullWidth = true, NoHeader = false, FullScreen = false, CloseOnEscapeKey = true, CloseButton = true };
        
        public LoadingContainerState LoadingState { get; set; }

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
                LoadingState = LoadingContainerState.Error;
                return;
            }

            LoadingState = LoadingContainerState.Loading;

            try
            {
                StreamResponse = await StreamApi.GetMovieStream(ContentMeta.ImdbId);
                if (StreamResponse?.Streams is not null && StreamResponse.Streams.Any())
                {
                    LoadingState = LoadingContainerState.Loaded;
                }
                else
                {
                    LoadingState = LoadingContainerState.Empty;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                LoadingState = LoadingContainerState.Error;
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
    }
}
