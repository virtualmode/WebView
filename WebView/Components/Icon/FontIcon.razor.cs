using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace WebView
{
    [Obsolete(message: "Use 'Icon' instead")]
    public partial class FontIcon : BaseComponent
    {
        private string? icon;

        [Parameter]
        public string? IconName { get; set; }
        [Parameter] public string? IconSrc { get; set; }

        protected override Task OnParametersSetAsync()
        {
            if (IconName != null)
            {
                MappedFontIcons.Icons.TryGetValue(IconName, out icon);
            }
            else
            {
                icon = IconSrc;
            }

            return base.OnParametersSetAsync();
        }
    }
}
