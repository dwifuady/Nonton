using Microsoft.AspNetCore.Components;
using MudBlazor;
using Nonton.Dtos;
using Nonton.Services;
using System;

namespace Nonton.Components
{
    public partial class StreamSourceDialog
    {
        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; } = null!;

        [Parameter]
        public string? Id { get; set; }

        [Inject] public IStreamService StreamService { get; set; } = null!;
        [Inject] public NavigationManager NavigationManager { get; set; } = null!;

        public LoadingContainerState LoadingStateStreamSource { get; set; }
        public IEnumerable<StreamResponse>? StreamResponses { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (string.IsNullOrWhiteSpace(Id))
            {
                LoadingStateStreamSource = LoadingContainerState.Error;
                return;
            }

            LoadingStateStreamSource = LoadingContainerState.Loading;

            try
            {
                StreamResponses = await StreamService.GetStream(Id);

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

        private void PlayContent(string url)
        {
            MudDialog?.Close(DialogResult.Ok(url));
        }

        private void Close()
        {
            NavigationManager.NavigateTo($"detail/{Id}");
        }
    }
}
