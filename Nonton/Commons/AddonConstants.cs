using System.Globalization;

namespace Nonton.Commons
{
    public static class AddonConstants
    {
        public const string ResourcesCatalog = "catalog";
        public const string ResourcesMeta = "meta";
        public const string ResourcesAddonCatalog = "addon_catalog";
        public const string ResourcesStream = "stream";

        public const string TypeMovie = "movie";
        public const string TypeSeries = "series";

        public static string ToTitleCase(this string s) =>
            CultureInfo.InvariantCulture.TextInfo.ToTitleCase(s.ToLower());
    }
}
