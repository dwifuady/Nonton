﻿using System;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Nonton.Commons;
using Nonton.Features.Addons;
using Nonton.Features.Addons.Dtos.Manifest;
using Nonton.Features.Catalogs.Models;
using Nonton.Features.Meta.Models;
using Nonton.Features.Player.Dtos;
using Nonton.Features.Stream;
using Nonton.Shared;

namespace Nonton.Features.Meta.Pages;

public partial class Detail
{
    [Parameter] public string Id { get; set; } = null!;
    [Parameter] public string Type { get; set; } = null!;

    [Inject] public IMetaService MetaService { get; set; } = null!;
    [Inject] public IStreamService StreamService { get; set; } = null!;
    [Inject] public IAddonService AddonService { get; set; } = null!;
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;

    public bool ShowSourceSelect { get; set; }
    public IEnumerable<AddonDto>? Addons { get; set; }
    public IMeta? Meta { get; set; }


    public string SelectedSeason { get; set; } = null!;

    public bool IsTrailerPlaying { get; set; }
    public bool IsContentPlaying { get; set; }
    public string? ContentUrl { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        var id = Id;
        if (Id.Contains(':'))
        {
            id = Id.Split(':')[0];
            SelectedSeason = Id.Split(':')[1];
        }
        else if (Type == AddonConstants.TypeSeriesShortName)
        {
            SelectedSeason = "1";
        }

        Meta = await MetaService.GetMeta(Type, id);

        await LoadSource();

        StateHasChanged();
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
            NavigationManager.NavigateTo($"watch/{JsonSerializer.Serialize(playableItem).ToBase64()}");
        }
    }

    private void CloseTrailer()
    {
        IsTrailerPlaying = false;
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

    private void PlayContent(string url)
    {
        var playableItem = new PlayableItem
        {
            Url = url,
            IsYoutubeTrailer = false,
            Title = Meta?.Name,
            ImdbId = Meta?.ImdbId
        };
        NavigationManager.NavigateTo($"watch/{JsonSerializer.Serialize(playableItem).ToBase64()}");
    }
}
