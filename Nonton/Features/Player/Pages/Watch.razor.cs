using System.IO;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Blazorise.Video;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Nonton.Commons;
using Nonton.Features.Addons;
using Nonton.Features.Addons.Api;
using Nonton.Features.Addons.Dtos;
using Nonton.Features.Addons.Dtos.Manifest;
using Nonton.Features.Player.Dtos;
using Refit;

namespace Nonton.Features.Player.Pages;

public partial class Watch : IDisposable
{
    [Parameter]
    public string Type { get; set; } = null!;

    [Parameter]
    public string Id { get; set; } = null!;
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;
    [Inject] public IJSRuntime JsRuntime { get; set; } = null!;
    [Inject] public PlayableItemStateContainer StateContainer { get; set; } = null!;
    [Inject] public IAddonService AddonService { get; set; } = null!;
    public IEnumerable<AddonDto>? SubtitleAddons { get; set; }

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
        SubtitleAddons = await AddonService.LoadAllSubtitleAddons();

        await InitPlyr();
        await DownloadSubtitles();
    }

    private async Task FullScreenEntered()
    {
        await _module!.InvokeVoidAsync("setScreenOrientation", "landscape");
    }

    private async Task FullScreenExited()
    {
        await _module!.InvokeVoidAsync("setScreenOrientation", "portrait");
    }

    private async Task SetFullHeightPlayer()
    {
        if (_module is not null)
        {
            Console.WriteLine("Set full height player");
            await _module.InvokeVoidAsync("setFullHeightPlayer", "1");
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
        if (PlayableItem is not null && PlayableItem.Url.EndsWith("m3u8"))
        {
            StreamingLibrary = StreamingLibrary.Hls;
        }
    }

    private void SetPlyrAttributes()
    {
        // temp solution, adding crossorigin attr will make the realdebrid's video unplayable
        if (PlayableItem is not null && PlayableItem!.Url.EndsWith("m3u8"))
        {
            //setting crossorigin to anonymous so it can load subtitle from other source
            VideoAttributes.Add("crossorigin", "anonymous");
        }
    }

    private async Task DownloadSubtitles()
    {
        if (SubtitleAddons is null)
        {
            Console.WriteLine("No subtitle addons");
            return;
        }

        foreach (var subtitleAddon in SubtitleAddons)
        {
            var api = RestService.For<IStremioApi>(subtitleAddon.BaseUri);
            
            var subtitleDto = await api.GetSubtitles(Type, Id);
            if (subtitleDto.Subtitles is not null)
            {
                foreach (var subtitle in subtitleDto.Subtitles)
                {
                    if (string.IsNullOrWhiteSpace(subtitle.Url))
                    {
                        continue;
                    }

                    var url = subtitle.Url.Replace(
                        AddonConstants.DefaultStremioStreamingServerUrl + AddonConstants.DefaultStremioSubtitlePathUrl,
                        "");

                    using var client = new HttpClient();
                    await using var content =
                        await client.GetStreamAsync(
                            url);
                    var zipArchive = new ZipArchive(content, ZipArchiveMode.Read);
                    var test = zipArchive.Entries[0].Open();
                    StreamReader srtReader = new StreamReader(test);
                    
                    await GenerateSubtitleUrl(srtReader.ReadToEnd(), subtitle.Lang);
                }
            }
        }

        
    }

    private async Task GenerateSubtitleUrl(string data, string langId)
    {
        var url = await _module.InvokeAsync<string>("generateSubtitleUrl", data);
        if (!string.IsNullOrWhiteSpace(url))
        {
            if (PlayableItem is not null && PlayableItem!.Subtitles is null)
            {
                PlayableItem!.Subtitles = new List<Subtitle>();
            }

            var lang = "English";
            if (langId.Contains("ind") || langId.Contains("indonesia"))
            {
                lang = "Indonesia";
            }

            PlayableItem?.Subtitles?.Add(new Subtitle { Id = langId, Lang = lang, Url = url});
        }
        StateHasChanged();
    }

    public void Dispose()
    {
        StateContainer.OnChange -= StateHasChanged;
    }
}