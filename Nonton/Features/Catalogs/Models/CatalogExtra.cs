using Nonton.Features.Addons.Enums;

namespace Nonton.Features.Catalogs.Models;

public class CatalogExtra
{
    public CatalogExtra(ExtraEnum name, List<string>? options = null, bool isRequired = false, int? optionsLimit = null)
    {
        Name = name;
        Options = options;
        IsRequired = isRequired;
        OptionsLimit = optionsLimit;
    }

    public ExtraEnum Name { get; }

    public List<string>? Options { get; }

    public bool IsRequired { get; }

    public int? OptionsLimit { get; }
}
