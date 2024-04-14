using Microsoft.AspNetCore.Components;

namespace Samples;

public partial class NavMenu
{
    [Parameter]
    public EventCallback<WebView.Routing.NavLink> OnLinkClicked { get; set; }

    bool collapseNavMenu = true;
    string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;
    void ToggleNavMenu()
    {
        collapseNavMenu = !collapseNavMenu;
    }
}
