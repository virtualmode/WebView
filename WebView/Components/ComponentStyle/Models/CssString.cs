using System;
using System.Collections.Generic;
using System.Text;

namespace WebView
{
    public class CssString : IRuleProperties
    {
        [CsProperty(IsCssStringProperty = true)]
        public string? Css { get; set; }
    }
}
