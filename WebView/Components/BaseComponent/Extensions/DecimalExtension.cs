using System;
using System.Collections.Generic;
using System.Text;

namespace WebView
{
    public static class DecimalExtension
    {
        public static string ToCssValue(this decimal value)
        {
            return value.ToString().Replace(',', '.');
        }
    }
}
