using System.Globalization;
using Nonton.Features.Meta;

namespace Nonton.Commons
{
    public static class AddonConstants
    {
        public const string ResourcesCatalog = "catalog";
        public const string ResourcesMeta = "meta";
        public const string ResourcesAddonCatalog = "addon_catalog";
        public const string ResourcesStream = "stream";

        public const string TypeDefault = "";
        public const string TypeMovieShortName = "movie";
        public const string TypeSeriesShortName = "series";
        public const string TypeChannelShortName = "channel";
        public const string TypeAnimeShortName = "anime";
        public const string TypeTvShortName = "tv";

        public const string TypeMovieTitle = "Movie";
        public const string TypeSeriesTitle = "Serie";
        public const string TypeChannelTitle = "Channel";
        public const string TypeAnimeTitle = "Anime";
        public const string TypeTvTitle = "Tv";

        public const string ExtraGenre = "genre";
        public const string ExtraSkip = "skip";
        public const string ExtraSearch = "search";

        public const string StremioApiUrl = "https://api.strem.io";

        public const string TrailerTypeTrailer = "Trailer";
        public const string TrailerTypeTeaser = "Teaser";
        public const string TrailerTypeBts = "Behind the Scenes";
        public const string TrailerTypeFeaturette = "Featurette";

        public static Dictionary<string, MetaTypeEnum> MetaTypeDict = new Dictionary<string, MetaTypeEnum>()
        {
            { TypeSeriesShortName, MetaTypeEnum.Series },
            { TypeMovieShortName, MetaTypeEnum.Movie }
        };

        public static string ToTitleCase(this string s) =>
            CultureInfo.InvariantCulture.TextInfo.ToTitleCase(s.ToLower());
    }
}
