using System.Text.Json;

namespace Nonton.Commons;

public static class AddonResourcesParser
{
    public static bool TryParseJson<T>(string? json, out T? result)
    {
        if (json == null)
        {
            result = default;
            return false;
        }
        try
        {
            result = JsonSerializer.Deserialize<T>(json);
            return true;
        }
        catch
        {
            result = default;
            return false;
        }
    }
}
