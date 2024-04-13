using Microsoft.AspNetCore.Components;

namespace WebView;

public partial class RibbonMenu<TItem> : BaseComponent
{
    private bool _showBackstage;

#pragma warning disable BL0007 // Component parameters should be auto properties.
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public RenderFragment? Backstage { get; set; }
    [Parameter] public string? BackstageHeader { get; set; }
    [Parameter] public EventCallback<bool> ShowBackstageChanged { get; set; }
    [Parameter] public IEnumerable<TItem>? ItemsSource { get; set; }
    [Parameter] public RenderFragment<TItem>? ItemTemplate { get; set; }
    [Parameter]
    public bool ShowBackstage
    {
        get
        {
            return _showBackstage;
        }
        set
        {
            if (_showBackstage != value)
            {
                _showBackstage = value;
                ShowBackstageChanged.InvokeAsync(_showBackstage);
            }
        }
    }
#pragma warning restore BL0007 // Component parameters should be auto properties.
}
