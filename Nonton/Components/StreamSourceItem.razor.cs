using Microsoft.AspNetCore.Components;
using MudBlazor;
using Nonton.Features.Addons.Dtos;
using Nonton.Features.Addons.Dtos.Manifest;
using Nonton.Features.Stream;

namespace Nonton.Components
{
    public partial class StreamSourceItem
    {
        [Parameter] public AddonDto Addon { get; set; } = null!;
        [Parameter] public string Id { get; set; } = null!;
        [Parameter] public EventCallback<string> OnItemSelected { get; set; }

        [Inject] public IStreamService StreamService { get; set; } = null!;
        [Inject] public ISnackbar Snackbar { get; set; } = null!;

        public StreamResponse? StreamResponse { get; set; }

        public LoadingContainerState LoadingStateStreamSource { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            LoadingStateStreamSource = LoadingContainerState.Loading;
            try
            {
                StreamResponse = await StreamService.GetStream(Addon, Id);

                LoadingStateStreamSource = LoadingContainerState.Loaded;
            }
            catch (Exception)
            {
                LoadingStateStreamSource = LoadingContainerState.Empty;
            }
        }

        private void PlayContent(string url)
        {
            OnItemSelected.InvokeAsync(url);
        }
    }
}
