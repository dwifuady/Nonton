﻿using Microsoft.AspNetCore.Components;
using MudBlazor;
using Nonton.Api;
using Nonton.Components;
using Nonton.Dtos;
using Refit;

namespace Nonton.Pages
{
    public partial class Detail
    {
        [Parameter]
        public string Id { get; set; } = null!;

        [Inject] public IDialogService DialogService { get; set; } = null!;

        public static IStremioApi StremioApi => RestService.For<IStremioApi>("https://v3-cinemeta.strem.io");
        public static IStremioApi StreamApi => RestService.For<IStremioApi>("https://watchhub.strem.io");
        public Meta? ContentMeta { get; set; }
        public StreamResponse? StreamResponse { get; set; }

        private const string ImdbIcon = "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 448 512\"><!--! Font Awesome Free 6.1.1 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license/free (Icons: CC BY 4.0, Fonts: SIL OFL 1.1, Code: MIT License) Copyright 2022 Fonticons, Inc. --><path d=\"M400 32H48C21.5 32 0 53.5 0 80v352c0 26.5 21.5 48 48 48h352c26.5 0 48-21.5 48-48V80c0-26.5-21.5-48-48-48zM21.3 229.2H21c.1-.1.2-.3.3-.4zM97 319.8H64V192h33zm113.2 0h-28.7v-86.4l-11.6 86.4h-20.6l-12.2-84.5v84.5h-29V192h42.8c3.3 19.8 6 39.9 8.7 59.9l7.6-59.9h43zm11.4 0V192h24.6c17.6 0 44.7-1.6 49 20.9 1.7 7.6 1.4 16.3 1.4 24.4 0 88.5 11.1 82.6-75 82.5zm160.9-29.2c0 15.7-2.4 30.9-22.2 30.9-9 0-15.2-3-20.9-9.8l-1.9 8.1h-29.8V192h31.7v41.7c6-6.5 12-9.2 20.9-9.2 21.4 0 22.2 12.8 22.2 30.1zM265 229.9c0-9.7 1.6-16-10.3-16v83.7c12.2.3 10.3-8.7 10.3-18.4zm85.5 26.1c0-5.4 1.1-12.7-6.2-12.7-6 0-4.9 8.9-4.9 12.7 0 .6-1.1 39.6 1.1 44.7.8 1.6 2.2 2.4 3.8 2.4 7.8 0 6.2-9 6.2-14.4z\"/></svg>";

        private bool _openDrawer = false;

        private readonly DialogOptions _trailerDialogOptions = new() { MaxWidth = MaxWidth.Medium, FullWidth = true, NoHeader = false, FullScreen = false, CloseOnEscapeKey = true, CloseButton = true };
        
        public LoadingContainerState LoadingState { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var detail = await StremioApi.GetMovieDetail(Id);
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
                StreamResponse = await StreamApi.GetStream(ContentMeta.ImdbId);
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
