using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Nonton.Commons;
using Nonton.Features.Addons.Api;
using Nonton.Features.Addons.Dtos.Manifest;
using SqliteWasmHelper;
using Refit;
using Nonton.Features.Database;

namespace Nonton.Features.Addons;
public class AddonService : IAddonService
{
    private readonly ISqliteWasmDbContextFactory<NontonDbContext> _dbFactory;
    public AddonService(ISqliteWasmDbContextFactory<NontonDbContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }
    public async Task<IEnumerable<AddonDto>?> LoadAllAddons()
    {
        var defaultAddons = await LoadDefaultAddons();

        await using var context = await _dbFactory.CreateDbContextAsync();
        var installedAddons = await context.NontonExtensions.ToListAsync();
        var disabledExtension = await context.DisabledExtensions.ToListAsync();
        var addons = defaultAddons!.ToList();
        foreach (var addon in installedAddons)
        {
            var addonManifest = JsonSerializer.Deserialize<ManifestDto>(addon.Manifest);

            if (addonManifest is not null)
            {
                addons.Add(new AddonDto
                {
                    Manifest = addonManifest,
                    TransportUrl = addon.TransportUrl
                });
            }
        }

        foreach (var addon in addons)
        {
            addon.IsEnabled = disabledExtension.All(de => de.TransportUrl != addon.TransportUrl);
        }

        return addons;
    }

    public async Task<IEnumerable<AddonDto>?> LoadAllActiveAddons()
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        var allAddons = await LoadAllAddons();
        var disabledExtension = await context.DisabledExtensions.ToListAsync();
        return allAddons?.Where(a => disabledExtension.All(de => de.TransportUrl != a.TransportUrl));
    }

    public async Task<IEnumerable<AddonDto>?> LoadAllCatalogAddons()
    {
        var allAddons = await LoadAllActiveAddons();
        var addons = new List<AddonDto>();
        if (allAddons == null) return await Task.FromResult(addons);

        foreach (var addon in allAddons)
        {
            if (addon.Manifest?.ResourcesString is not null && addon.Manifest.ResourcesString.Contains(AddonConstants.ResourcesCatalog))
            {
                addons.Add(addon);
            }

            if (addon.Manifest?.Resources is not null && addon.Manifest.Resources.Select(r => r.Name).Contains(AddonConstants.ResourcesCatalog))
            {
                addons.Add(addon);
            }
        }

        return await Task.FromResult(addons);
    }

    public async Task<IEnumerable<AddonDto>?> LoadAllMetaAddons()
    {
        var allAddons = await LoadAllActiveAddons();
        var addons = new List<AddonDto>();
        if (allAddons == null) return await Task.FromResult(addons);

        foreach (var addon in allAddons)
        {
            if (addon.Manifest?.ResourcesString is not null && addon.Manifest.ResourcesString.Contains(AddonConstants.ResourcesMeta))
            {
                addons.Add(addon);
            }

            if (addon.Manifest?.Resources is not null && addon.Manifest.Resources.Select(r => r.Name).Contains(AddonConstants.ResourcesMeta))
            {
                addons.Add(addon);
            }
        }

        return await Task.FromResult(addons);
    }

    public async Task<IEnumerable<AddonDto>?> LoadAllStreamAddons()
    {
        var allAddons = await LoadAllActiveAddons();
        var addons = new List<AddonDto>();
        if (allAddons == null) return await Task.FromResult(addons);

        foreach (var addon in allAddons)
        {
            if (addon.Manifest?.ResourcesString is not null && addon.Manifest.ResourcesString.Contains(AddonConstants.ResourcesStream) && (addon.Manifest.Types!.Contains(AddonConstants.TypeMovieShortName) || addon.Manifest.Types!.Contains(AddonConstants.TypeSeriesShortName)))
            {
                addons.Add(addon);
            }

            if (addon.Manifest?.Resources is not null && addon.Manifest.Resources.Select(r => r.Name).Contains(AddonConstants.ResourcesStream) && (addon.Manifest.Types!.Contains(AddonConstants.TypeMovieShortName) || addon.Manifest.Types!.Contains(AddonConstants.TypeSeriesShortName)))
            {
                addons.Add(addon);
            }
        }

        return await Task.FromResult(addons);
    }

    public async Task<AddonDto?> LoadDefaultCatalogAddons()
    {
        var allAddons = DefaultAddons.AllDefaultAddons();

        if (allAddons == null) return null;

        foreach (var addon in allAddons)
        {
            if (addon.Manifest?.ResourcesString is not null && addon.Manifest.ResourcesString.Contains(AddonConstants.ResourcesCatalog))
            {
                return addon;
            }

            if (addon.Manifest?.Resources is not null && addon.Manifest.Resources.Select(r => r.Name).Contains(AddonConstants.ResourcesCatalog))
            {
                return addon;
            }
        }

        return await Task.FromResult(new AddonDto());
    }

    public async Task SaveAddon(string url, string manifestString)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        var manifest = JsonSerializer.Deserialize<ManifestDto>(manifestString);

        if (manifest == null || string.IsNullOrWhiteSpace(manifest.Id)) return;

        var extension = new NontonExtension
        {
            Manifest = manifestString,
            TransportUrl = url,
            Enabled = true,
            Id = manifest.Id
        };

        context.NontonExtensions.Add(extension);

        await context.SaveChangesAsync();

    }

    public async Task DeleteAddon(string id)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        var addon = await context.NontonExtensions.SingleOrDefaultAsync(x => x.Id == id);
        if (addon == null) return;
        context.NontonExtensions.Remove(addon);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<AddonDto>?> GetAddonCollection()
    {
        var api = RestService.For<IStremioApi>(AddonConstants.StremioApiUrl);
        return await api.GetAddonCollection()!;
    }

    public async Task<IEnumerable<AddonDto>?> LoadDefaultAddons()
    {
        return await Task.FromResult(DefaultAddons.AllDefaultAddons());
    }

    public async Task ToggleAddonStatus(AddonDto addon, bool isEnabled)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        if (isEnabled)
        {
            var disabledExtension = context.DisabledExtensions.FirstOrDefault(a =>
                a.AddonId == addon.Manifest.Id && a.TransportUrl == addon.TransportUrl);

            if (disabledExtension is not null)
            {
                context.DisabledExtensions.Remove(disabledExtension);
            }
        }
        else
        {
            var disabledExtension = new DisabledExtension
            {
                Id = Guid.NewGuid().ToString(),
                AddonId = addon.Manifest.Id!,
                TransportUrl = addon.TransportUrl
            };

            context.DisabledExtensions.Add(disabledExtension);
        }
        await context.SaveChangesAsync();

    }
}