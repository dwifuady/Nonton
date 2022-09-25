using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using Nonton;
using SqliteWasmHelper;
using MudBlazor;
using Nonton.Features.Addons;
using Nonton.Features.Catalogs;
using Nonton.Features.Meta;
using Nonton.Features.Database;
using Nonton.Features.Stream;
using Toolbelt.Blazor.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 10000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});
builder.Services.AddScoped<IAddonService, AddonService>();
builder.Services.AddScoped<IStreamService, StreamService>();
builder.Services.AddScoped<ICatalogApi, CatalogApi>();
builder.Services.AddScoped<ICatalogService, CatalogService>();
builder.Services.AddScoped<IMetaApi, MetaApi>();
builder.Services.AddScoped<IMetaService, MetaService>();

builder.Services.AddSqliteWasmDbContextFactory<NontonDbContext>(
    opts => opts.UseSqlite("Data Source=nonton.sqlite3"));

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddPWAUpdater();

await builder.Build().RunAsync();
