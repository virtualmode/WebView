using WebView;

namespace Samples.Models;

class DropDownViewModel : RibbonItem
{
    public IEnumerable<IDropdownOption>? DropdownOptions { get; set; }
    public IDropdownOption? Selected { get; set; }

    public string Width { get; set; } = "200px";
}
