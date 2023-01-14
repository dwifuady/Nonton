using System.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Nonton.Commons;
using Nonton.Features.Addons;
using Nonton.Features.Addons.Dtos;
using Nonton.Features.Addons.Dtos.Manifest;
using Nonton.Features.Catalogs.Models;
using Nonton.Features.Meta.Models;
using Nonton.Features.Player.Dtos;
using Nonton.Features.Stream;
using Nonton.Shared;

namespace Nonton.Features.Meta.Pages;

public partial class Detail : IDisposable
{
    [Parameter] public string Id { get; set; } = null!;
    [Parameter] public string Type { get; set; } = null!;

    [Inject] public IMetaService MetaService { get; set; } = null!;
    [Inject] public IStreamService StreamService { get; set; } = null!;
    [Inject] public IAddonService AddonService { get; set; } = null!;
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;
    [Inject] public PlayableItemStateContainer StateContainer { get; set; } = null!;
    [Inject] public IJSRuntime JsRuntime { get; set; } = null!;

    public bool ShowSourceSelect { get; set; }
    public IEnumerable<AddonDto>? Addons { get; set; }
    public IMeta? Meta { get; set; }

    public string SelectedSeason { get; set; } = null!;
    
    private IJSObjectReference? _module;

    protected override void OnInitialized()
    {
        StateContainer.OnChange += StateHasChanged;
    }

    protected override async Task OnParametersSetAsync()
    {
        var id = Id;
        if (Id.Contains(':'))
        {
            id = Id.Split(':')[0];
            SelectedSeason = Id.Split(':')[1];
        }
        else if (Type == AddonConstants.AddonTypeSeriesShortName)
        {
            SelectedSeason = "1";
        }

        Meta = await MetaService.GetMeta(Type, id);

        await LoadSource();

        StateHasChanged();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        _module = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/carousel.js");
        if (_module is not null && Meta is ISeriesMeta { Episodes.Count: > 0 })
        {
            await _module.InvokeVoidAsync("initFlickityEpisodes");
        }
    }

    private void PlayTrailer()
    {
        if (Meta is MovieMeta movieMeta)
        {
            var trailerSource = movieMeta?.Trailers.FirstOrDefault(x => x.Type == AddonConstants.TrailerTypeTrailer).Source;
            var playableItem = new PlayableItem
            {
                Url = $"https://www.youtube.com/embed/{trailerSource}",
                IsYoutubeTrailer = true,
                Title = Meta?.Name,
                ImdbId = Meta?.ImdbId
            };
            StateContainer.PlayableItem = playableItem;
            NavigationManager.NavigateTo($"watch/{Type}/{Id}");
        }
    }
    
    private void ToggleSourceSelect()
    {
        ShowSourceSelect = !ShowSourceSelect;
    }

    private void WatchFirstEpisode()
    {
        Id = $"{Id}:1:1";
        ToggleSourceSelect();
    }

    private void WatchEpisode(string episodeId)
    {
        Id = episodeId;
        ToggleSourceSelect();
    }

    private async Task LoadSource()
    {
        Addons ??= await AddonService.LoadAllStreamAddons();
    }

    private void PlayContent(StreamDto streamDto)
    {
        if (string.IsNullOrWhiteSpace(streamDto.Url))
            return;

        var playableItem = new PlayableItem
        {
            Url = streamDto.Url,
            IsYoutubeTrailer = false,
            Title = Meta?.Name,
            ImdbId = Meta?.ImdbId,
            Poster = Meta?.Background
        };

        if (streamDto.Subtitles is not null)
        {
            List<Subtitle> subtitles = new();
            foreach (var streamDtoSubtitle in streamDto.Subtitles)
            {
                //some addons encode url twice
                var decodedUrl = HttpUtility.UrlDecode(HttpUtility.UrlDecode(streamDtoSubtitle.Url));
                //remove default stremio streaming server url
                var subtitleUrl = decodedUrl?.Replace(AddonConstants.DefaultStremioStreamingServerUrl + AddonConstants.DefaultStremioSubtitlePathUrl, "");

                if (string.IsNullOrWhiteSpace(subtitleUrl)) continue;
                var sub = new Subtitle
                {
                    Id = streamDtoSubtitle.Id,
                    Lang = streamDtoSubtitle.Lang,
                    Url = subtitleUrl
                };
                subtitles.Add(sub);
            }

            playableItem.Subtitles = subtitles;
        }

        StateContainer.PlayableItem = playableItem;

        NavigationManager.NavigateTo($"watch/{Type}/{Id}");
    }

    public void Dispose()
    {
        StateContainer.OnChange -= StateHasChanged;
    }
}
