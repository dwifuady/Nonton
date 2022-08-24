using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Nonton.Dtos.Manifest;
using Nonton.Commons;
using Nonton.Data;
using SqliteWasmHelper;

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
                TransportUrl = addon.TransportUrl,
                Manifest = JsonSerializer.Deserialize<Manifest>(addon.Manifest)
            });
        }

        return addons;
    }

    public async Task<IEnumerable<Addon>?> LoadAllCatalogAddons()
    {
        var allAddons = await LoadAllAddons();
        return await Task.FromResult(allAddons!.Where(a => a.Manifest?.Resources != null && a.Manifest.Resources.Contains(AddonConstants.ResourcesCatalog)));
    }

    public async Task<IEnumerable<Addon>?> LoadAllMetaAddons()
    {
        var allAddons = await LoadAllAddons();
        return await Task.FromResult(allAddons!.Where(a => a.Manifest?.Resources != null && a.Manifest.Resources.Contains(AddonConstants.ResourcesMeta)));
    }

    public async Task<IEnumerable<Addon>?> LoadAllStreamAddons()
    {
        var allAddons = await LoadAllAddons();
        return await Task.FromResult(allAddons!.Where(a => a.Manifest?.Resources != null && a.Manifest.Resources.Contains(AddonConstants.ResourcesStream)));
    }

    public async Task<Addon?> LoadDefaultCatalogAddons()
    {
        return await Task.FromResult(DefaultAddons.AllDefaultAddons()!.FirstOrDefault(a => a.Manifest?.Resources != null && a.Manifest.Resources.Contains(AddonConstants.ResourcesCatalog)));
    }

    public async Task<IEnumerable<Addon>?> LoadDefaultAddons()
    {
        return await Task.FromResult(DefaultAddons.AllDefaultAddons());
    }
}