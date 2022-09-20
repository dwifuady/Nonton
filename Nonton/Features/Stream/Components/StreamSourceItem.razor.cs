using Microsoft.AspNetCore.Components;
using Nonton.Components;
using Nonton.Features.Addons.Dtos;
using Nonton.Features.Addons.Dtos.Manifest;

namespace Nonton.Features.Stream.Components
{
    public partial class StreamSourceItem
    {
        [Parameter] public AddonDto Addon { get; set; } = null!;
        [Parameter] public string Id { get; set; } = null!;
        [Parameter] public EventCallback<string> OnItemSelected { get; set; }

        [Inject] public IStreamService StreamService { get; set; } = null!;
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

        private void SelectSource(string url)
        {
            OnItemSelected.InvokeAsync(url);
        }
    }
}
