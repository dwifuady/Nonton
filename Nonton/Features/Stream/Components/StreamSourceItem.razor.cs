using Microsoft.AspNetCore.Components;
using Nonton.Features.Addons.Dtos;
using Nonton.Features.Addons.Dtos.Manifest;
using Nonton.Shared;

namespace Nonton.Features.Stream.Components
{
    public partial class StreamSourceItem
    {
        [Parameter] public AddonDto Addon { get; set; } = null!;
        [Parameter] public string Id { get; set; } = null!;
        [Parameter] public EventCallback<string> OnItemSelected { get; set; }

        [Inject] public IStreamService StreamService { get; set; } = null!;
        public StreamResponseDto? StreamResponse { get; set; }

        public LoadingContainerState LoadingStateStreamSource { get; set; }
        private static string _demoUrl = "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4";
        private static string _demoUrl2 = "https://lamberta.github.io/html5-animation/examples/ch04/assets/movieclip.mp4";
        private bool _isDebugMode = false;

        protected override void OnInitialized()
        {
#if DEBUG
            _isDebugMode = true;
#endif
            base.OnInitialized();
        }

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
