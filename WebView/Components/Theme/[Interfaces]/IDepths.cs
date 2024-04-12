using System;
using System.Collections.Generic;
using System.Text;

namespace WebView
{
    public interface IDepths
    {
        string Depth0 { get; }
        string Depth4 { get; }
        string Depth8 { get; }
        string Depth16 { get; }
        string Depth64 { get; }
    }
}
