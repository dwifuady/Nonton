using System.Globalization;
using Nonton.Features.Catalogs;

namespace Nonton.Commons
{
    public static class AddonConstants
    {
        public const string ResourcesCatalog = "catalog";
        public const string ResourcesMeta = "meta";
        public const string ResourcesAddonCatalog = "addon_catalog";
        public const string ResourcesStream = "stream";
        public const string ResourcesSubtitle = "subtitles";

        public const string AddonTypeDefault = "";
        public const string AddonTypeMovieShortName = "movie";
        public const string AddonTypeSeriesShortName = "series";
        public const string AddonTypeChannelShortName = "channel";
        public const string AddonTypeAnimeShortName = "anime";
        public const string AddonTypeTvShortName = "tv";
        public const string AddonTypeGameShortName = "games";

        public const string AddonTypeMovieTitle = "Movie";
        public const string AddonTypeSeriesTitle = "TV Series";
        public const string AddonTypeChannelTitle = "Channel";
        public const string AddonTypeAnimeTitle = "Anime";
        public const string AddonTypeTvTitle = "Tv";
        public const string AddonTypeGamesTitle = "Games";

        public const string ExtraGenre = "genre";
        public const string ExtraSkip = "skip";
        public const string ExtraSearch = "search";

        public const string StremioApiUrl = "https://api.strem.io";

        public const string TrailerTypeTrailer = "Trailer";
        public const string TrailerTypeTeaser = "Teaser";
        public const string TrailerTypeBts = "Behind the Scenes";
        public const string TrailerTypeFeaturette = "Featurette";

        public static Dictionary<string, CatalogTypeEnum> TypeDict = new()
        {
            { AddonTypeSeriesShortName, CatalogTypeEnum.Movie },
            { AddonTypeMovieShortName, CatalogTypeEnum.Series },
            { AddonTypeChannelShortName, CatalogTypeEnum.Channel },
            { AddonTypeAnimeShortName, CatalogTypeEnum.Anime },
            { AddonTypeTvShortName, CatalogTypeEnum.Tv },
            { AddonTypeGameShortName, CatalogTypeEnum.Games }
        };

        public static string ToTitleCase(this string s) =>
            CultureInfo.InvariantCulture.TextInfo.ToTitleCase(s.ToLower());

        public static string DefaultStremioStreamingServerUrl = "http://127.0.0.1:11470/";
        public static string DefaultStremioSubtitlePathUrl = "subtitles.vtt?from=";
    }
}
