using Blazorise.Video;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Nonton.Commons;
using Nonton.Features.Player.Dtos;

namespace Nonton.Features.Player.Pages;

public partial class Watch : IDisposable
{
    [Parameter]
    public string? Type { get; set; }
    [Parameter]
    public string? Id { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;
    [Inject] public IJSRuntime JsRuntime { get; set; } = null!;
    [Inject] public PlayableItemStateContainer StateContainer { get; set; } = null!;
    public PlayableItem? PlayableItem { get; set; }
    public Dictionary<string, object> VideoAttributes { get; set; } = new();
    public StreamingLibrary StreamingLibrary { get; set; } = StreamingLibrary.Dash;

    private IJSObjectReference? _module;

    protected override void OnInitialized()
    {
        StateContainer.OnChange += StateHasChanged;
    }

    protected override async Task OnParametersSetAsync()
    {
        _module = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/watch.js");
        PlayableItem = StateContainer.PlayableItem;
        await InitPlyr();
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
        if (PlayableItem is null)
        {
            NavigationManager.NavigateTo($"detail/{Type}/{Id}");
        }

        if (PlayableItem is not null && PlayableItem.IsYoutubeTrailer)
        {
            await InitPlyrForYoutube();
        }

        SetStreamingLibrary();
        SetPlyrAttributes();
        await SetFullHeightPlayer();
    }

    private async Task InitPlyrForYoutube()
    {
        if (_module is not null)
        {
            await _module.InvokeVoidAsync("loadPlyrAssets");
            await _module.InvokeVoidAsync("initPlyr");
        }
    }

    private void SetStreamingLibrary()
    {
        if (PlayableItem is null) return;
        if (PlayableItem.Url.EndsWith("m3u8"))
        {
            StreamingLibrary = StreamingLibrary.Hls;
        }
    }

    private void SetPlyrAttributes()
    {
        //setting crossorigin to anonymous so it can load subtitle from other source
        VideoAttributes.Add("crossorigin", "anonymous");
    }
    
    public void Dispose()
    {
        StateContainer.OnChange -= StateHasChanged;
    }
}