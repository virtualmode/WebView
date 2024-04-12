using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebView
{
    public partial class OverflowSet<TItem> : BaseComponent
    {
        [Parameter] public bool Vertical { get; set; }
        [Parameter] public IEnumerable<TItem>? Items { get; set; }
        [Parameter] public IEnumerable<TItem>? OverflowItems { get; set; }

        [Parameter] public RenderFragment<TItem>? ItemTemplate { get; set; }

        [Parameter] public RenderFragment<IEnumerable<TItem>>? OverflowTemplate { get; set; }

        [Parameter] public bool DoNotContainWithinFocusZone { get; set; }

        //[Parameter] public RenderFragment<RenderFragment> OverflowMenuButtonTemplate { get; set; }

        [Parameter] public Func<TItem, string>? GetKey { get; set; }

        //protected System.Collections.Generic.List<TItem> calculatedItems;
        //protected System.Collections.Generic.List<TItem> calculatedOverflowItems;

        protected FocusZone? focusZoneComponent;
    }
}
