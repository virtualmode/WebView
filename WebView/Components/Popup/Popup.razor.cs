using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebView
{
    /**
     * This adds accessibility to Dialog and Panel controls
     */
    public partial class Popup : BaseComponent, IAsyncDisposable
    {
        [Parameter] public RenderFragment? ChildContent { get; set; }

        [Parameter] public string? Role { get; set; }
        [Parameter] public bool ShouldRestoreFocus { get; set; } = true;  //THIS DOES NOTHING AT THE MOMENT.

        [Parameter] public EventCallback<EventArgs> OnDismiss { get; set; }


        // Come back to this later if needed!
        // Line needed on razor page:
        // style=@("overflowY: {(needsVerticalScrollBar ? "scroll" : "hidden")}; outline: none")
        //private bool needsVerticalScrollBar = false;

        private string? _handleToLastFocusedElement;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (baseModule == null)
                baseModule = await JSRuntime!.InvokeAsync<IJSObjectReference>("import", BasePath);
            if (firstRender)
            {
                _handleToLastFocusedElement = $"id_{Guid.NewGuid().ToString().Replace("-", "")}";
                await baseModule!.InvokeVoidAsync("storeLastFocusedElement", _handleToLastFocusedElement);
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task KeyDownHandler(KeyboardEventArgs args)
        {
            switch (args.Key)
            {
                case "Escape":
                    await OnDismiss.InvokeAsync(args);
                    break;
            }

            //return Task.CompletedTask;
        }

        public override async ValueTask DisposeAsync()
        {
            try
            {
                if (_handleToLastFocusedElement != null && baseModule != null)
                {
                    await baseModule.InvokeVoidAsync("restoreLastFocus", _handleToLastFocusedElement, ShouldRestoreFocus);
                    await baseModule.DisposeAsync();
                }

                await base.DisposeAsync();
            }
            catch (TaskCanceledException)
            {
            }
        }
    }
}
