namespace Nonton.Features.Addons.Enums;
public enum AddonContentTypeEnum
{
    Movie,
    Series
}

public static class AddonContentTypeEnumExtension
{
    public static string GetValue(this AddonContentTypeEnum addonType)
    {
        return addonType.ToString().ToLower();
    }
}