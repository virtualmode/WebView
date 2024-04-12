using System;
using System.Collections.Generic;
using System.Text;

using WebView.Themes.Default;

namespace WebView
{
    public class FontStyle : IFontStyle
    {
        public IFontSize FontSize { get; set; } = new DefaultFontSize();
        public IFontWeight FontWeight { get; set; } = new DefaultFontWeight();
        public IIconFontSize IconFontSize { get; set; } = new DefaultIconFontSize();
    }
}
