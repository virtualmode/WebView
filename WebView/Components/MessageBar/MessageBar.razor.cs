using Microsoft.AspNetCore.Components;

namespace WebView;

public partial class MessageBar : BaseComponent
{
    private MessageBar? _componentRef;

#pragma warning disable BL0007 // Component parameters should be auto properties.
    [Parameter]
    public bool IsMultiline { get; set; } = true;

    [Parameter]
    public MessageBarType MessageBarType { get; set; } = MessageBarType.Info;

    [Parameter]
    public bool Truncated { get; set; }

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public RenderFragment? Actions { get; set; }

    [Parameter]
    public string? DismissButtonAriaLabel { get; set; }

    [Parameter]
    public string? OverflowButtonAriaLabel { get; set; }

    [Parameter]
    public EventCallback OnDismiss { get; set; }

    [Parameter]
    public MessageBar? ComponentRef
    {
        get => _componentRef;
        set
        {
            if (value == _componentRef)
                return;
            _componentRef = value;
            if (value != null)
                MessageBarType = value.MessageBarType;
            ComponentRefChanged.InvokeAsync(value);
        }
    }

    [Parameter]
    public EventCallback<MessageBar> ComponentRefChanged { get; set; }
#pragma warning restore BL0007 // Component parameters should be auto properties.

    protected bool HasDismiss { get => (OnDismiss.HasDelegate); }

    protected bool HasExpand { get => (Truncated && Actions == null); }

    protected bool ExpandSingelLine { get; set; }

    protected void Truncate()
    {
        ExpandSingelLine = !ExpandSingelLine;
    }

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
            ComponentRef = this as MessageBar;
        return base.OnAfterRenderAsync(firstRender);
    }

    protected string GetTypeCss()
    {
        return MessageBarType switch
        {
            MessageBarType.Warning => " ms-MessageBar--warning ",
            MessageBarType.Error => " ms-MessageBar--error ",
            MessageBarType.Blocked => " ms-MessageBar--blocked ",
            MessageBarType.SevereWarning => " ms-MessageBar--severeWarning ",
            MessageBarType.Success => " ms-MessageBar--success ",
            _ => "",
        };
    }
}
