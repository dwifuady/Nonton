using Microsoft.AspNetCore.Components;

namespace Nonton.Shared
{
    public enum LoadingContainerState { Loading, Loaded, Error, Empty }
    public partial class LoadingContainer
    {
        [Parameter] public LoadingContainerState State { get; set; }

        [Parameter] public RenderFragment? Loading { get; set; }
        [Parameter] public RenderFragment? Loaded { get; set; }
        [Parameter] public RenderFragment? Empty { get; set; }
        [Parameter] public RenderFragment? Error { get; set; }
    }
}
