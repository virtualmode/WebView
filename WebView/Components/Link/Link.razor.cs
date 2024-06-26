using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;

namespace WebView
{
    public partial class Link : BaseComponent
    {
        [Parameter]
        public LinkType? Type { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public string? Href { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        [Parameter]
        public string? Target { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

    }
}
