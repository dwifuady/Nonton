using Nonton.Commons;
using Nonton.Features.Addons;
using Nonton.Features.Addons.Dtos;
using Nonton.Features.Meta.Models;

namespace Nonton.Features.Meta
{
    public class MetaService : IMetaService
    {
        private readonly IMetaApi _metaApi;
        private readonly IAddonService _addonService;

        public MetaService(IAddonService addonService, IMetaApi metaApi)
        {
            _addonService = addonService;
            _metaApi = metaApi;
        }
        
        public async Task<IMeta> GetMeta(string metaType, string id)
        {
            var metaDto = await _metaApi.GetMetaDto(metaType, id);
            return metaDto.ToMeta();
        }
        
    }

    public static class MetaExtension
    {
        public static IMeta ToMeta(this DetailDto detail)
        {
            if (detail.Meta is null)
            {
                throw new Exception("Can't find meta for this title");
            }

            var metaDto = detail.Meta;

            return metaDto.Type switch
            {
                AddonConstants.AddonTypeSeriesShortName =>
                    new SeriesMeta(
                        metaDto.Id, 
                        metaDto.Name!, 
                        metaDto.ImdbId,
                        metaDto.Description,
                        metaDto.Year,
                        metaDto.Cast, 
                        metaDto.Director, 
                        metaDto.Genre, 
                        metaDto.ImdbRating, 
                        metaDto.Released, 
                        metaDto.Slug,
                        metaDto.Type, 
                        metaDto.Writer, 
                        metaDto.Poster, 
                        metaDto.Background, 
                        metaDto.Logo, 
                        metaDto.Runtime, 
                        metaDto.Videos.MapToGroupedSeasons(), 
                        metaDto.Videos.MapToEpisodes()),
                AddonConstants.AddonTypeMovieShortName =>
                    new MovieMeta(
                        metaDto.Id,
                        metaDto.Name!,
                        metaDto.ImdbId,
                        metaDto.Description,
                        metaDto.Year,
                        metaDto.Cast,
                        metaDto.Director,
                        metaDto.Genre,
                        metaDto.ImdbRating,
                        metaDto.Released,
                        metaDto.Slug,
                        metaDto.Type,
                        metaDto.Writer,
                        metaDto.Poster,
                        metaDto.Background,
                        metaDto.Logo,
                        metaDto.Runtime,
                        metaDto.Trailers.MapToTrailers()
                        ),
                _ => new DefaultMeta(
                    metaDto.Id, 
                    metaDto.Name!, 
                    metaDto.ImdbId, 
                    metaDto.Description, 
                    metaDto.Year,
                    metaDto.Cast, 
                    metaDto.Director, 
                    metaDto.Genre, 
                    metaDto.ImdbRating, 
                    metaDto.Released, 
                    metaDto.Slug,
                    metaDto.Type, 
                    metaDto.Writer, 
                    metaDto.Poster, 
                    metaDto.Background, 
                    metaDto.Logo, 
                    metaDto.Runtime)
            };
        }

        public static List<(string Type, string Source)> MapToTrailers(this List<TrailerDto>? trailerDtos)
        {
            if (trailerDtos == null) return new List<(string Type, string Source)>();

            var trailers = new List<(string Type, string Source)>();
            
            foreach (var trailerDto in trailerDtos)
            {
                if (!string.IsNullOrWhiteSpace(trailerDto.Type) && !string.IsNullOrWhiteSpace(trailerDto.Source))
                {
                    trailers.Add(new ValueTuple<string, string>(trailerDto.Type, trailerDto.Source));
                }
            }

            return trailers;
        }

        public static List<(int SeasonNumber, string SeasonDescription)> MapToGroupedSeasons(this List<VideoDto>? videoDtos)
        {
            if ((videoDtos != null && !videoDtos.Any()) || videoDtos is null)
                return new List<(int SeasonNumber, string SeasonDescription)>();

            var seasons = new List<(int SeasonNumber, string SeasonDescription)>();
            var seasonNumbers = videoDtos.Where(s => s.Season.HasValue).Select(x => x.Season!.Value).GroupBy(x => x);
            foreach (var seasonNumber in seasonNumbers)
            {
                seasons.Add(seasonNumber.Key == 0
                    ? new ValueTuple<int, string>(seasonNumber.Key, "Special")
                    : new ValueTuple<int, string>(seasonNumber.Key, $"Season {seasonNumber.Key}"));
            }

            return seasons;
        }

        public static List<Episode>? MapToEpisodes(this List<VideoDto>? videoDtos)
        {
            if ((videoDtos != null && !videoDtos.Any()) || videoDtos is null) return null;
            
            var episodes = new List<Episode>();
            foreach (var videoDto in videoDtos)
            {
                if (!videoDto.Season.HasValue || !videoDto.Number.HasValue) continue;

                episodes.Add(
                    new Episode(
                        videoDto.Id!,
                        videoDto.Season.Value,
                        videoDto.Number.Value,
                        videoDto.Name, 
                        videoDto.Released,
                        "",
                        "",
                        videoDto.Overview,
                        videoDto.Thumbnail
                    ));
            }

            return episodes;
        }
    }
}