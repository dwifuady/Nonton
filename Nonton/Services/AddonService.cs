using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Nonton.Api;
using Nonton.Dtos.Manifest;
using Nonton.Commons;
using Nonton.Data;
using SqliteWasmHelper;
using Refit;

namespace Nonton.Services;
public class AddonService : IAddonService
{
    private readonly ISqliteWasmDbContextFactory<NontonDbContext> _dbFactory;
    public AddonService(ISqliteWasmDbContextFactory<NontonDbContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }
    public async Task<IEnumerable<Addon>?> LoadAllAddons()
    {
        var defaultAddons = await LoadDefaultAddons();

        await using var context = await _dbFactory.CreateDbContextAsync();
        var installedAddons = await context.NontonExtensions.ToListAsync();

        var addons = defaultAddons!.ToList();
        foreach (var addon in installedAddons)
        {
            addons.Add(new Addon
            {
                Manifest = JsonSerializer.Deserialize<Manifest>(addon.Manifest),
                TransportUrl = addon.TransportUrl
            });
        }

        return addons;
    }

    public async Task<IEnumerable<Addon>?> LoadAllCatalogAddons()
    {
        var allAddons = await LoadAllAddons();
        var addons = new List<Addon>();
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

    public async Task<IEnumerable<Addon>?> LoadAllMetaAddons()
    {
        var allAddons = await LoadAllAddons();
        var addons = new List<Addon>();
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

    public async Task<IEnumerable<Addon>?> LoadAllStreamAddons()
    {
        var allAddons = await LoadAllAddons();
        var addons = new List<Addon>();
        if (allAddons == null) return await Task.FromResult(addons);

        foreach (var addon in allAddons)
        {
            if (addon.Manifest?.ResourcesString is not null && addon.Manifest.ResourcesString.Contains(AddonConstants.ResourcesStream) && (addon.Manifest.Types!.Contains(AddonConstants.TypeMovie) || addon.Manifest.Types!.Contains(AddonConstants.TypeSeries)))
            {
                addons.Add(addon);
            }

            if (addon.Manifest?.Resources is not null && addon.Manifest.Resources.Select(r => r.Name).Contains(AddonConstants.ResourcesStream) && (addon.Manifest.Types!.Contains(AddonConstants.TypeMovie) || addon.Manifest.Types!.Contains(AddonConstants.TypeSeries)))
            {
                addons.Add(addon);
            }
        }

        return await Task.FromResult(addons);
    }

    public async Task<Addon?> LoadDefaultCatalogAddons()
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

        return await Task.FromResult(new Addon());
    }

    public async Task SaveAddon(string url, string manifestString)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        var manifest = JsonSerializer.Deserialize<Manifest>(manifestString);
        
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

    public async Task<IEnumerable<Addon>?> GetAddonCollection()
    {
        var api = RestService.For<IStremioApi>(AddonConstants.StremioApiUrl);
        return await api.GetAddonCollection()!;
    }

    public async Task<IEnumerable<Addon>?> LoadDefaultAddons()
    {
        return await Task.FromResult(DefaultAddons.AllDefaultAddons());
    }
}