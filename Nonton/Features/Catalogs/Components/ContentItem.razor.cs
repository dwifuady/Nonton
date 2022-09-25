﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Nonton.Features.Addons.Dtos;
using Nonton.Features.Catalogs.Models;
using Nonton.Shared;

namespace Nonton.Features.Catalogs.Components
{
    public partial class ContentItem
    {
        [Parameter]
        public Catalog Catalog { get; set; } = null!;

        [Parameter] 
        public ContentItemType ContentItemType { get; set; } = ContentItemType.Default;

        [Parameter]
        public string? SearchQuery { get; set; }

        [Parameter]
        public string? Genre { get; set; } = "";
        [Inject] public NavigationManager NavigationManager { get; set; } = null!;
        [Inject] public ICatalogApi CatalogApi { get; set; } = null!;
        [Inject] public IJSRuntime JsRuntime { get; set; } = null!;

        public IEnumerable<MetaDto>? Metas { get; set; }

        public LoadingContainerState LoadingContainerState { get; set; } = LoadingContainerState.Empty;

        protected override async Task OnParametersSetAsync()
        {
            LoadingContainerState = LoadingContainerState.Loading;
            await LoadItems();
            LoadingContainerState = LoadingContainerState.Loaded;
        }

        private async Task LoadItems()
        {
            try
            {
                if (ContentItemType == ContentItemType.Search && !string.IsNullOrWhiteSpace(SearchQuery))
                {
                    var discoverItem = await CatalogApi.Search(Catalog.AddonBaseUri, Catalog.CatalogShortName, Catalog.CatalogId, SearchQuery);
                    Metas = discoverItem.Metas;
                }
                else
                {
                    var discoverItem = await CatalogApi.GetCatalogItem(Catalog.AddonBaseUri, Catalog.CatalogShortName, Catalog.CatalogId, Genre ?? "");
                    Metas = discoverItem.Metas;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        //protected override async Task OnAfterRenderAsync(bool firstRender)
        //{
        //    if (!string.IsNullOrWhiteSpace(Catalog.CatalogName))
        //    {
        //        await JsRuntime.InvokeVoidAsync("forceHorizontalScroll", $"content-container-{Catalog.CatalogType}-{Catalog.CatalogId}");
        //    }
        //}

        public async Task ViewDetail(string id)
        {
            await Task.Run(() => NavigationManager.NavigateTo($"detail/{Catalog.CatalogShortName}/{id}"));
        }
    }

    public enum ContentItemType
    {
        Default,
        Search,
        Discover
    }
}
