namespace Nonton.Features.Catalogs.Models;

public interface ICatalog
{
    string AddonName { get; }
    string AddonBaseUri { get; }
    string CatalogId { get; }
    string CatalogName { get; }
    
    /// <summary>
    /// Genres, IsRequired
    /// </summary>
    (List<string>? Genres, bool IsGenreRequired) Genres { get; }
    
    /// <summary>
    /// IsSearchable, IsRequired
    /// </summary>
    (bool IsSearchable, bool IsSearchRequired) Searchable { get; }

    /// <summary>
    /// IsSkippable, IsRequired
    /// </summary>
    (bool IsSkippable, bool IsSkipRequired) Skippable { get; }
}