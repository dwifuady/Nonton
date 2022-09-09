using Nonton.Commons;
using Nonton.Features.Addons;
using Nonton.Features.Addons.Dtos.Manifest;
using Nonton.Features.Catalogs.Models;

namespace Nonton.Features.Catalogs
{
    public class CatalogService : ICatalogService
    {
        private readonly IAddonService _addonService;
        public CatalogService(IAddonService addonService)
        {
            _addonService = addonService;
        }
        public async Task<IEnumerable<Catalog>> GetAllCatalogsAsync()
        {
            var catalogs = new List<Catalog>();
            var catalogAddons = await _addonService.LoadAllCatalogAddons();
            if (catalogAddons == null) return catalogs;

            foreach (var catalogAddon in catalogAddons)
            {
                var allCatalogs = catalogAddon.Manifest.Catalogs;
                if (allCatalogs is null) continue;

                foreach (var catalogDto in allCatalogs)
                {
                    var genres = GetGenres(catalogDto);
                    var isSearchable = IsSearchable(catalogDto);
                    var isSkippable = IsSkippable(catalogDto);
                    switch (catalogDto.Type)
                    {
                        case AddonConstants.TypeMovie:
                            {
                                var catalog = new MovieCatalog(catalogAddon.Manifest.Name!, catalogAddon.BaseUri,
                                    catalogDto.Id!, catalogDto.Name!, genres, isSearchable, isSkippable);
                                catalogs.Add(catalog);
                                break;
                            }
                        case AddonConstants.TypeSeries:
                            {
                                var catalog = new SeriesCatalog(catalogAddon.Manifest.Name!, catalogAddon.BaseUri,
                                    catalogDto.Id!, catalogDto.Name!, genres, isSearchable, isSkippable);
                                catalogs.Add(catalog);
                                break;
                            }
                    }
                }
            }

            return catalogs;
        }

        public Task<IEnumerable<Catalog>> GetDefaultCatalogAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Catalog>> GetSearchableCatalogAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get List of Genre and IsRequired property
        /// </summary>
        /// <param name="catalogDto"></param>
        /// <returns></returns>
        private static (List<string> genres, bool isGenreRequired) GetGenres(CatalogDto catalogDto)
        {
            var genres = new List<string>();
            var isRequired = false;
            
            if (catalogDto.Genres is not null && catalogDto.Genres.Any())
            {
                genres.AddRange(catalogDto.Genres.Select(g => g.ToString()!));
                isRequired = catalogDto.ExtraRequired != null &&
                             catalogDto.ExtraRequired.Contains(AddonConstants.ExtraGenre);
            }
            else if (catalogDto.Extra != null && catalogDto.Extra.Any(e => e.Name == AddonConstants.ExtraGenre))
            {
                var extraGenre = catalogDto.Extra.FirstOrDefault(e => e.Name == AddonConstants.ExtraGenre);
                var genreDto = extraGenre?.Options;
                if (genreDto == null) return (genres, isRequired);

                genres.AddRange(genreDto.Select(g => (string)g));
                isRequired = extraGenre is { IsRequired: { } } && extraGenre.IsRequired.Value;
            }

            return (genres, isRequired);
        }

        private static (bool isSearchable, bool isSearchableRequired) IsSearchable(CatalogDto catalogDto)
        {
            var isSearchable = false;
            var isRequired = false;

            if (catalogDto.ExtraSupported != null && catalogDto.ExtraSupported.Contains(AddonConstants.ExtraSearch))
            {
                isSearchable = true;
                isRequired = catalogDto.ExtraRequired != null &&
                             catalogDto.ExtraRequired.Contains(AddonConstants.ExtraSearch);
            }
            else if (catalogDto.Extra != null && catalogDto.Extra.Any(e => e.Name == AddonConstants.ExtraSearch))
            {
                isSearchable = true;
                var extraSearch = catalogDto.Extra.FirstOrDefault(e => e.Name == AddonConstants.ExtraSearch);
                isRequired = extraSearch is { IsRequired: { } } && extraSearch.IsRequired.Value;
            }

            return (isSearchable, isRequired);
        }

        private static (bool isSkippable, bool isSkippableRequired) IsSkippable(CatalogDto catalogDto)
        {
            var isSkippable = false;
            var isRequired = false;

            if (catalogDto.ExtraSupported != null && catalogDto.ExtraSupported.Contains(AddonConstants.ExtraSkip))
            {
                isSkippable = true;
                isRequired = catalogDto.ExtraRequired != null &&
                             catalogDto.ExtraRequired.Contains(AddonConstants.ExtraSkip);
            }
            else if (catalogDto.Extra != null && catalogDto.Extra.Any(e => e.Name == AddonConstants.ExtraSkip))
            {
                isSkippable = true;
                var extraSearch = catalogDto.Extra.FirstOrDefault(e => e.Name == AddonConstants.ExtraSkip);
                isRequired = extraSearch is { IsRequired: { } } && extraSearch.IsRequired.Value;
            }

            return (isSkippable, isRequired);
        }
    }
}
