using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace WebView
{
    public partial class Check : BaseComponent
    {
        [Parameter]
        public bool Checked { get; set; }

        [Parameter]
        public bool UseFastIcons { get; set; }
    }
}
