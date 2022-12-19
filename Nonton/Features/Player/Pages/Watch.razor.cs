using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Nonton.Commons;
using Nonton.Features.Player.Dtos;

namespace Nonton.Features.Player.Pages;

public partial class Watch
{
    [Parameter]
    public string? EncodedPlayableItem { get; set; }

    [Inject] public IJSRuntime JsRuntime { get; set; } = null!;

    public PlayableItem? PlayableItem { get; set; }

    public string? Url { get; set; }

    private IJSObjectReference? _module;

    protected override async Task OnParametersSetAsync()
    {
        if (!string.IsNullOrWhiteSpace(EncodedPlayableItem))
        {
            _module = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/watch.js");
            PlayableItem = JsonSerializer.Deserialize<PlayableItem>(EncodedPlayableItem.DecodeBase64());
            
            if (PlayableItem is not null && PlayableItem.IsYoutubeTrailer)
            {
                await InitPlyr();
            }
            await SetFullHeightPlayer();
        }
    }

    private async Task SetFullHeightPlayer()
    {
        if (_module is not null)
        {
            await _module.InvokeVoidAsync("setFullHeightPlayer");
        }
    }

    private async Task InitPlyr()
    {
        if (_module is not null)
        {
            await _module.InvokeVoidAsync("loadPlyrAssets");
            await _module.InvokeVoidAsync("initPlyr");
        }
    }
}