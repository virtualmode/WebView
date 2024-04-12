using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace WebView
{
    public partial class LayerPortal : BaseComponent, IDisposable
    {
        [Parameter] public RenderFragment? ChildContent { get; set; }
        [Parameter] public string? Id { get; set; }
        [Parameter] public ElementReference? VirtualParent { get; set; }
        [Parameter] public bool IsFixed { get; set; } = true;

        protected bool shouldRender = false;

        protected override bool ShouldRender()
        {
            if (shouldRender)
            {
                shouldRender = false;
                return true;
            }
            return false;
        }

        public void Rerender()
        {
            shouldRender = true;
            InvokeAsync(StateHasChanged);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
    
            await base.OnAfterRenderAsync(firstRender);
        }

       

        public void Dispose()
        {
            
        }
    }
}
