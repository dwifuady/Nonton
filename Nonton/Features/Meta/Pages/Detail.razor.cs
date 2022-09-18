using Microsoft.AspNetCore.Components;
using MudBlazor;
using Nonton.Components;
using Nonton.Features.Stream;

namespace Nonton.Features.Meta.Pages
{
    public partial class Detail
    {
        [Parameter] public string Id { get; set; } = null!;
        [Parameter] public string Type { get; set; } = null!;

        [Inject] public IDialogService DialogService { get; set; } = null!;
        [Inject] public IMetaService MetaService { get; set; } = null!;
        [Inject] public IStreamService StreamService { get; set; } = null!;
        [Inject] public NavigationManager NavigationManager { get; set; } = null!;

        public Addons.Dtos.Meta? ContentMeta { get; set; }

        public List<int?> Seasons
        {
            get
            {
                if (ContentMeta?.Videos is null) return new List<int?>();
                var seasons = ContentMeta.Videos
                    .Where(x => x.Season.HasValue && x.Season.Value > 0)
                    .GroupBy(x => x.Season)
                    .Select(x => x.Key)
                    .OrderBy(x => x).ToList();
                if (ContentMeta.Videos.Any(x => x.Season == 0))
                {
                    seasons.Add(0);
                }

                return seasons;
            }
        }

        public string SelectedSeason { get; set; } = null!;

        public bool IsTrailerPlaying { get; set; }

        private readonly DialogOptions _trailerDialogOptions = new() { MaxWidth = MaxWidth.Medium, FullWidth = true, NoHeader = false, FullScreen = false, CloseOnEscapeKey = true, CloseButton = true };

        protected override async Task OnParametersSetAsync()
        {
            var id = Id;
            if (Id.Contains(":"))
            {
                id = Id.Split(':')[0];
                SelectedSeason = Id.Split(':')[1];
            }

            var detail = await MetaService.GetMeta(Type, id);
            ContentMeta = detail.Meta;

            StateHasChanged();
        }

        private void Watch(string id, string season = "", string episode = "")
        {
            if (string.IsNullOrWhiteSpace(season) && string.IsNullOrWhiteSpace(episode))
                NavigationManager.NavigateTo($"watch/{Type}/{id}");
            else
                NavigationManager.NavigateTo($"watch/{Type}/{id}:{season}:{episode}");
        }

        private void PlayTrailer()
        {
            IsTrailerPlaying = true;
        }
    }
}
