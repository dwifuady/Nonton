﻿namespace Nonton.Features.Addons;
public static class DefaultAddonsManifest
{
    public static string Cinemeta =>
        @"{
    ""id"": ""com.linvo.cinemeta"",
    ""version"": ""3.0.9"",
    ""description"": ""The official addon for movie and series catalogs"",
    ""name"": ""Cinemeta"",
    ""resources"": [""catalog"", ""meta"", ""addon_catalog""],
    ""types"": [""movie"", ""series""],
    ""idPrefixes"": [""tt""],
    ""addonCatalogs"": [{
            ""type"": ""all"",
            ""id"": ""official"",
            ""name"": ""Official""
        }, {
            ""type"": ""movie"",
            ""id"": ""official"",
            ""name"": ""Official""
        }, {
            ""type"": ""series"",
            ""id"": ""official"",
            ""name"": ""Official""
        }, {
            ""type"": ""channel"",
            ""id"": ""official"",
            ""name"": ""Official""
        }, {
            ""type"": ""all"",
            ""id"": ""community"",
            ""name"": ""Community""
        }, {
            ""type"": ""movie"",
            ""id"": ""community"",
            ""name"": ""Community""
        }, {
            ""type"": ""series"",
            ""id"": ""community"",
            ""name"": ""Community""
        }, {
            ""type"": ""channel"",
            ""id"": ""community"",
            ""name"": ""Community""
        }, {
            ""type"": ""tv"",
            ""id"": ""community"",
            ""name"": ""Community""
        }, {
            ""type"": ""Podcasts"",
            ""id"": ""community"",
            ""name"": ""Community""
        }, {
            ""type"": ""other"",
            ""id"": ""community"",
            ""name"": ""Community""
        }
    ],
    ""catalogs"": [{
            ""type"": ""movie"",
            ""id"": ""top"",
            ""genres"": [""Action"", ""Adventure"", ""Animation"", ""Biography"", ""Comedy"", ""Crime"", ""Documentary"", ""Drama"", ""Family"", ""Fantasy"", ""History"", ""Horror"", ""Mystery"", ""Romance"", ""Sci-Fi"", ""Sport"", ""Thriller"", ""War"", ""Western""],
            ""extra"": [{
                    ""name"": ""genre"",
                    ""options"": [""Action"", ""Adventure"", ""Animation"", ""Biography"", ""Comedy"", ""Crime"", ""Documentary"", ""Drama"", ""Family"", ""Fantasy"", ""History"", ""Horror"", ""Mystery"", ""Romance"", ""Sci-Fi"", ""Sport"", ""Thriller"", ""War"", ""Western""]
                }, {
                    ""name"": ""search""
                }, {
                    ""name"": ""skip""
                }
            ],
            ""extraSupported"": [""search"", ""genre"", ""skip""],
            ""name"": ""Popular""
        }, {
            ""type"": ""series"",
            ""id"": ""top"",
            ""genres"": [""Action"", ""Adventure"", ""Animation"", ""Biography"", ""Comedy"", ""Crime"", ""Documentary"", ""Drama"", ""Family"", ""Fantasy"", ""History"", ""Horror"", ""Mystery"", ""Romance"", ""Sci-Fi"", ""Sport"", ""Thriller"", ""War"", ""Western"", ""Reality-TV"", ""Talk-Show"", ""Game-Show""],
            ""extra"": [{
                    ""name"": ""genre"",
                    ""options"": [""Action"", ""Adventure"", ""Animation"", ""Biography"", ""Comedy"", ""Crime"", ""Documentary"", ""Drama"", ""Family"", ""Fantasy"", ""History"", ""Horror"", ""Mystery"", ""Romance"", ""Sci-Fi"", ""Sport"", ""Thriller"", ""War"", ""Western"", ""Reality-TV"", ""Talk-Show"", ""Game-Show""]
                }, {
                    ""name"": ""search""
                }, {
                    ""name"": ""skip""
                }
            ],
            ""extraSupported"": [""search"", ""genre"", ""skip""],
            ""name"": ""Popular""
        }, {
            ""type"": ""movie"",
            ""id"": ""year"",
            ""genres"": [""2022"", ""2021"", ""2020"", ""2019"", ""2018"", ""2017"", ""2016"", ""2015"", ""2014"", ""2013"", ""2012"", ""2011"", ""2010"", ""2009"", ""2008"", ""2007"", ""2006"", ""2005"", ""2004"", ""2003"", ""2002"", ""2001"", ""2000"", ""1999"", ""1998"", ""1997"", ""1996"", ""1995"", ""1994"", ""1993"", ""1992"", ""1991"", ""1990"", ""1989"", ""1988"", ""1987"", ""1986"", ""1985"", ""1984"", ""1983"", ""1982"", ""1981"", ""1980"", ""1979"", ""1978"", ""1977"", ""1976"", ""1975"", ""1974"", ""1973"", ""1972"", ""1971"", ""1970"", ""1969"", ""1968"", ""1967"", ""1966"", ""1965"", ""1964"", ""1963"", ""1962"", ""1961"", ""1960"", ""1959"", ""1958"", ""1957"", ""1956"", ""1955"", ""1954"", ""1953"", ""1952"", ""1951"", ""1950"", ""1949"", ""1948"", ""1947"", ""1946"", ""1945"", ""1944"", ""1943"", ""1942"", ""1941"", ""1940"", ""1939"", ""1938"", ""1937"", ""1936"", ""1935"", ""1934"", ""1933"", ""1932"", ""1931"", ""1930"", ""1929"", ""1928"", ""1927"", ""1926"", ""1925"", ""1924"", ""1923"", ""1922"", ""1921"", ""1920""],
            ""extra"": [{
                    ""name"": ""genre"",
                    ""options"": [""2022"", ""2021"", ""2020"", ""2019"", ""2018"", ""2017"", ""2016"", ""2015"", ""2014"", ""2013"", ""2012"", ""2011"", ""2010"", ""2009"", ""2008"", ""2007"", ""2006"", ""2005"", ""2004"", ""2003"", ""2002"", ""2001"", ""2000"", ""1999"", ""1998"", ""1997"", ""1996"", ""1995"", ""1994"", ""1993"", ""1992"", ""1991"", ""1990"", ""1989"", ""1988"", ""1987"", ""1986"", ""1985"", ""1984"", ""1983"", ""1982"", ""1981"", ""1980"", ""1979"", ""1978"", ""1977"", ""1976"", ""1975"", ""1974"", ""1973"", ""1972"", ""1971"", ""1970"", ""1969"", ""1968"", ""1967"", ""1966"", ""1965"", ""1964"", ""1963"", ""1962"", ""1961"", ""1960"", ""1959"", ""1958"", ""1957"", ""1956"", ""1955"", ""1954"", ""1953"", ""1952"", ""1951"", ""1950"", ""1949"", ""1948"", ""1947"", ""1946"", ""1945"", ""1944"", ""1943"", ""1942"", ""1941"", ""1940"", ""1939"", ""1938"", ""1937"", ""1936"", ""1935"", ""1934"", ""1933"", ""1932"", ""1931"", ""1930"", ""1929"", ""1928"", ""1927"", ""1926"", ""1925"", ""1924"", ""1923"", ""1922"", ""1921"", ""1920""],
                    ""isRequired"": true
                }, {
                    ""name"": ""skip""
                }
            ],
            ""extraSupported"": [""genre"", ""skip""],
            ""extraRequired"": [""genre""],
            ""name"": ""New""
        }, {
            ""type"": ""series"",
            ""id"": ""year"",
            ""genres"": [""2022"", ""2021"", ""2020"", ""2019"", ""2018"", ""2017"", ""2016"", ""2015"", ""2014"", ""2013"", ""2012"", ""2011"", ""2010"", ""2009"", ""2008"", ""2007"", ""2006"", ""2005"", ""2004"", ""2003"", ""2002"", ""2001"", ""2000"", ""1999"", ""1998"", ""1997"", ""1996"", ""1995"", ""1994"", ""1993"", ""1992"", ""1991"", ""1990"", ""1989"", ""1988"", ""1987"", ""1986"", ""1985"", ""1984"", ""1983"", ""1982"", ""1981"", ""1980"", ""1979"", ""1978"", ""1977"", ""1976"", ""1975"", ""1974"", ""1973"", ""1972"", ""1971"", ""1970"", ""1969"", ""1968"", ""1967"", ""1966"", ""1965"", ""1964"", ""1963"", ""1962"", ""1961"", ""1960""],
            ""extra"": [{
                    ""name"": ""genre"",
                    ""options"": [""2022"", ""2021"", ""2020"", ""2019"", ""2018"", ""2017"", ""2016"", ""2015"", ""2014"", ""2013"", ""2012"", ""2011"", ""2010"", ""2009"", ""2008"", ""2007"", ""2006"", ""2005"", ""2004"", ""2003"", ""2002"", ""2001"", ""2000"", ""1999"", ""1998"", ""1997"", ""1996"", ""1995"", ""1994"", ""1993"", ""1992"", ""1991"", ""1990"", ""1989"", ""1988"", ""1987"", ""1986"", ""1985"", ""1984"", ""1983"", ""1982"", ""1981"", ""1980"", ""1979"", ""1978"", ""1977"", ""1976"", ""1975"", ""1974"", ""1973"", ""1972"", ""1971"", ""1970"", ""1969"", ""1968"", ""1967"", ""1966"", ""1965"", ""1964"", ""1963"", ""1962"", ""1961"", ""1960""],
                    ""isRequired"": true
                }, {
                    ""name"": ""skip""
                }
            ],
            ""extraSupported"": [""genre"", ""skip""],
            ""extraRequired"": [""genre""],
            ""name"": ""New""
        }, {
            ""type"": ""movie"",
            ""id"": ""imdbRating"",
            ""genres"": [""Action"", ""Adventure"", ""Animation"", ""Biography"", ""Comedy"", ""Crime"", ""Documentary"", ""Drama"", ""Family"", ""Fantasy"", ""History"", ""Horror"", ""Mystery"", ""Romance"", ""Sci-Fi"", ""Sport"", ""Thriller"", ""War"", ""Western""],
            ""extra"": [{
                    ""name"": ""genre"",
                    ""options"": [""Action"", ""Adventure"", ""Animation"", ""Biography"", ""Comedy"", ""Crime"", ""Documentary"", ""Drama"", ""Family"", ""Fantasy"", ""History"", ""Horror"", ""Mystery"", ""Romance"", ""Sci-Fi"", ""Sport"", ""Thriller"", ""War"", ""Western""]
                }, {
                    ""name"": ""skip""
                }
            ],
            ""extraSupported"": [""genre"", ""skip""],
            ""name"": ""Featured""
        }, {
            ""type"": ""series"",
            ""id"": ""imdbRating"",
            ""genres"": [""Action"", ""Adventure"", ""Animation"", ""Biography"", ""Comedy"", ""Crime"", ""Documentary"", ""Drama"", ""Family"", ""Fantasy"", ""History"", ""Horror"", ""Mystery"", ""Romance"", ""Sci-Fi"", ""Sport"", ""Thriller"", ""War"", ""Western"", ""Reality-TV"", ""Talk-Show"", ""Game-Show""],
            ""extra"": [{
                    ""name"": ""genre"",
                    ""options"": [""Action"", ""Adventure"", ""Animation"", ""Biography"", ""Comedy"", ""Crime"", ""Documentary"", ""Drama"", ""Family"", ""Fantasy"", ""History"", ""Horror"", ""Mystery"", ""Romance"", ""Sci-Fi"", ""Sport"", ""Thriller"", ""War"", ""Western"", ""Reality-TV"", ""Talk-Show"", ""Game-Show""]
                }, {
                    ""name"": ""skip""
                }
            ],
            ""extraSupported"": [""genre"", ""skip""],
            ""name"": ""Featured""
        }, {
            ""type"": ""series"",
            ""id"": ""last-videos"",
            ""extra"": [{
                    ""name"": ""lastVideosIds"",
                    ""isRequired"": true,
                    ""optionsLimit"": 500
                }
            ],
            ""extraSupported"": [""lastVideosIds""],
            ""extraRequired"": [""lastVideosIds""],
            ""name"": ""Last videos""
        }
    ]
}
";

    public static string WatchHub =>
        @"{
    ""id"": ""org.stremio.watchhub"",
    ""logo"": ""https://www.strem.io/images/watchhub-logo.png"",
    ""version"": ""1.15.0"",
    ""name"": ""WatchHub"",
    ""description"": ""Find where to stream your favorite movies and shows amongst Netflix, iTunes, Hulu, Amazon, HBO GO and many others. Supports many countries."",
    ""resources"": [""stream""],
    ""types"": [""movie"", ""series""],
    ""catalogs"": [],
    ""idPrefixes"": [""tt""]}
";
}
