namespace Nonton.Features.Addons.Enums;
public enum AddonResourceEnum
{
    Catalog,
    Meta,
    Stream
}

public static class AddonResourceEnumExtension
{
    public static string GetValue(this AddonResourceEnum addonType)
    {
        return addonType.ToString().ToLower();
    }
}