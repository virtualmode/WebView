using WebView.Style;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebView.Routing
{
    public partial class Nav: BaseComponent
    {
        [Parameter] public RenderFragment? ChildContent { get; set; }
        //[Parameter] public string AriaLabel { get; set; }
        [Parameter] public string? ExpandButtonAriaLabel { get; set; }

        [Parameter] public bool IsOnTop { get; set; }

        protected override Task OnInitializedAsync()
        {
            //System.Diagnostics.Debug.WriteLine("Initializing NavBase");
            return base.OnInitializedAsync();
        }

        protected override Task OnParametersSetAsync()
        {
            return base.OnParametersSetAsync();
        }

        public void ManuallyRefresh()
        {
            StateHasChanged();
        }
    }
}
