using System.Net.Http.Json;
using Nonton.Dtos.Manifest;

namespace Nonton.Services;
public class AddonService : IAddonService
{
    private readonly HttpClient _httpClient;
    public AddonService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Addon>?> LoadAddons()
    {
        return await LoadAddonsDummy();
    }

    public async Task<IEnumerable<Addon>?> LoadAddonsDummy()
    {
        var manifestCinemeta = await _httpClient.GetFromJsonAsync<Manifest>("sample-data/Cinemeta.json");

        if (manifestCinemeta is null) return null;

        var addonCinemeta = new Addon
        {
            Manifest = manifestCinemeta,
            TransportUrl = "https://v3-cinemeta.strem.io/manifest.json"
        };
        
        return new List<Addon>
        {
            addonCinemeta
        };
    }
}