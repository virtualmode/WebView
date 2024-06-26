using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

namespace WebView
{
    public partial class LayerHost : BaseComponent, IDisposable
    {

        [Parameter] public RenderFragment? ChildContent { get; set; }

        [Parameter] public RenderFragment? HostedContent { get; set; }

        [Parameter] public string? Id { get; set; }

        [Parameter] public bool InsertFirst { get; set; } = false;

        [Parameter] public bool IsFixed { get; set; } = true;

        [Inject] private LayerHostService? LayerHostService { get; set; }

        protected LayerPortalGenerator? portalGeneratorReference;


        public Task? AddOrUpdateHostedContentAsync(string layerId, RenderFragment? renderFragment)
        {
            return portalGeneratorReference?.AddOrUpdateHostedContentAsync(layerId, renderFragment);
        }

        public Task? RemoveHostedContentAsync(string layerId)
        {
            return portalGeneratorReference?.RemoveHostedContentAsync(layerId);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                LayerHostService?.RegisterHost(this);
                //JSRuntime.InvokeAsync<string>("registerLayerHost")
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        public void Dispose()
        {
            LayerHostService?.RemoveHost(this);
        }
    }
}
