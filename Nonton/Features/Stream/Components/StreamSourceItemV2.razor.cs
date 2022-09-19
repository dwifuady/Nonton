using Microsoft.AspNetCore.Components;
using Nonton.Features.Addons.Dtos;
using Nonton.Features.Addons.Dtos.Manifest;

namespace Nonton.Features.Stream.Components
{
    public partial class StreamSourceItemV2
    {
        [Parameter] public AddonDto Addon { get; set; } = null!;
        [Parameter] public string Id { get; set; } = null!;

        [Inject] public IStreamService StreamService { get; set; } = null!;
        public StreamResponse? StreamResponse { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            StreamResponse = await StreamService.GetStream(Addon, Id);
        }
    }
}
