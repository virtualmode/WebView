using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

namespace WebView.Routing
{
    public partial class NavBar : BaseComponent
    {
        [Parameter] public LayoutDirection Direction { get; set; }
        [Parameter] public string? Header { get; set; }

        [Parameter] public IEnumerable<INavBarItem> Items { get; set; } = new List<NavBarItem>();
        [Parameter] public IEnumerable<INavBarItem> OverflowItems { get; set; } = new List<NavBarItem>();
        //[Parameter] public IEnumerable<INavBarItem> FarItems { get; set; } = new List<NavBarItem>();

        [Parameter] public EventCallback<INavBarItem> OnDataReduced { get; set; }
        [Parameter] public EventCallback<INavBarItem> OnDataGrown { get; set; }

        [Parameter] public bool ShiftOnReduce { get; set; }

        [Parameter] public RenderFragment? FooterTemplate { get; set; }

        protected Func<NavBarData?, NavBarData?>? onGrowData;
        protected Func<NavBarData?, NavBarData?>? onReduceData;

        protected NavBarData? _currentData;

        [Inject] protected NavigationManager? NavigationManager { get; set; }

        protected override Task OnInitializedAsync()
        {
            onReduceData = (data) =>
            {
                if (data!.PrimaryItems?.Count > 0)
                {
                    INavBarItem movedItem = data.PrimaryItems[ShiftOnReduce ? 0 : data.PrimaryItems.Count - 1];
                    movedItem.RenderedInOverflow = true;

                    data.OverflowItems?.Insert(0, movedItem);
                    data.PrimaryItems.Remove(movedItem);

                    data.CacheKey = ComputeCacheKey(data);

                    OnDataReduced.InvokeAsync(movedItem);

                    return data;
                }
                else
                    return null;
            };

            onGrowData = (data) =>
            {
                if (data!.OverflowItems?.Count > data.MinimumOverflowItems)
                {
                    INavBarItem? movedItem = data.OverflowItems[0];
                    movedItem.RenderedInOverflow = false;
                    data.OverflowItems.Remove(movedItem);

                    if (ShiftOnReduce)
                        data.PrimaryItems?.Insert(0, movedItem);
                    else
                        data.PrimaryItems?.Add(movedItem);

                    data.CacheKey = ComputeCacheKey(data);

                    OnDataGrown.InvokeAsync(movedItem);

                    return data;
                }
                else
                    return null;
            };

            ProcessUri(NavigationManager!.Uri);
            NavigationManager.LocationChanged += UriHelper_OnLocationChanged;

            return base.OnInitializedAsync();
        }

        protected override Task OnParametersSetAsync()
        {
            _currentData = new NavBarData()
            {
                PrimaryItems = new List<INavBarItem>(Items ?? new List<INavBarItem>()),
                OverflowItems = new List<INavBarItem>(OverflowItems ?? new List<INavBarItem>()),
                //FarItems = new List<ICommandBarItem>(FarItems != null ? FarItems : new List<ICommandBarItem>()),
                MinimumOverflowItems = OverflowItems != null ? OverflowItems.Count() : 0,
                CacheKey = ""
            };



            return base.OnParametersSetAsync();
        }

        private static string ComputeCacheKey(NavBarData data)
        {
            string? primaryKey = data.PrimaryItems?.Aggregate("", (acc, item) => acc + item.CacheKey);
            //var farKey = data.FarItems.Aggregate("", (acc, item) => acc + item.CacheKey);
            string? overflowKey = data.OverflowItems?.Aggregate("", (acc, item) => acc + item.CacheKey);
            return string.Join(" ", primaryKey, overflowKey);
        }

        private void UriHelper_OnLocationChanged(object? sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
        {
            ProcessUri(e.Location);
        }

        private void ProcessUri(string uri)
        {
            if (uri.StartsWith(NavigationManager!.BaseUri))
                uri = uri[NavigationManager.BaseUri.Length..];

            string? processedUriRelative = null;
            string? processedUriAnchorIncluded = null;
            string? processUriAnchorOnly = null;

            processedUriRelative = uri.Split('?', '#')[0];

            string[]? split = uri.Split('?');
            processedUriAnchorIncluded = split[0];
            if (split.Length > 1)
            {
                string[]? anchorSplit = split[1].Split('#');
                if (anchorSplit.Length > 1)
                    processedUriAnchorIncluded += "#" + anchorSplit[1];
            }
            else
            {
                string[]? anchorSplit = split[0].Split('#');
                if (anchorSplit.Length > 1)
                    processedUriAnchorIncluded += "#" + anchorSplit[1];
            }

            string[]? split2 = uri.Split('#');
            if (split2.Length > 1)
                processUriAnchorOnly = split2[1];
            else
                processUriAnchorOnly = "";

            IEnumerable<INavBarItem>? allItems = Items.Concat(Items.Where(x => x.Items != null).SelectMany(x => GetChild(x.Items!)).Cast<INavBarItem>())
                .Concat(OverflowItems.Concat(OverflowItems.Where(x => x.Items != null).SelectMany(x => GetChild(x.Items!)).Cast<INavBarItem>()));
            foreach (INavBarItem? item in allItems)
            {
                switch (item.NavMatchType)
                {
                    case NavMatchType.RelativeLinkOnly:
                        if (processedUriRelative.Equals(item.Id) && !item.Checked)
                        {
                            item.Checked = true;
                        }
                        else if (!processedUriRelative.Equals(item.Id) && item.Checked)
                        {
                            item.Checked = false;
                        }
                        break;
                    case NavMatchType.AnchorIncluded:
                        if (processedUriAnchorIncluded.Equals(item.Id) && !item.Checked)
                        {
                            item.Checked = true;
                        }
                        else if (!processedUriAnchorIncluded.Equals(item.Id) && item.Checked)
                        {
                            item.Checked = false;
                        }
                        break;
                    case NavMatchType.AnchorOnly:
                        if (processUriAnchorOnly.Equals(item.Id) && !item.Checked)
                        {
                            item.Checked = true;
                        }
                        else if (!processUriAnchorOnly.Equals(item.Id) && item.Checked)
                        {
                            item.Checked = false;
                        }
                        break;
                }

            }
            StateHasChanged();
        }

        protected IEnumerable<IContextualMenuItem> GetChild(IEnumerable<IContextualMenuItem> list)
        {
            return list.Concat(list.Where(x => x.Items != null).SelectMany(x => GetChild(x.Items!)));
        }
    }
}
