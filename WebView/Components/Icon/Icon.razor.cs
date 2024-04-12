using Microsoft.AspNetCore.Components;

namespace WebView
{
    public partial class Icon : BaseComponent
    {
        [Parameter] public bool Filled { get; set; }
        [Parameter] public string? IconName { get; set; }
        [Parameter] public int IconSize { get; set; } = 20;
        [Parameter] public string? IconSrc { get; set; }
        [Parameter] public IconType IconType { get; set; }
        [Parameter] public bool UseSystemIcons { get; set; } = true;

        public string IconClassName
        {
            get
            {
                if (IconName == null)
                {
                    return "ms-Icon-placeHolder";
                }
                else
                {
                    if (!UseSystemIcons)
                    {
                        return IconName;
                    }
                    else
                    {
                        return $"icon-ic_fluent_{IconName}_{IconSize}_{(Filled ? "filled" : "regular")}";
                    }
                }
            }
        }

        public string FontStyle
        {
            get
            {
                return $@"font-family: FluentSystemIcons-{(Filled ? "Filled" : "Regular")} !important;
{this.Style}
";
            }
        }

        public string SpecificIconName
        {
            get
            {
                if (IconName == null)
                {
                    return "ms-Icon-placeHolder";
                }
                else
                {
                    if (!UseSystemIcons)
                    {
                        return IconName;
                    }
                    else
                    {
                        return $"ic_fluent_{IconName}_{IconSize}_{(Filled ? "filled" : "regular")}";
                    }
                }
            }
        }
    }
}
