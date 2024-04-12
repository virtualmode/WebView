using Microsoft.AspNetCore.Components.Web;

namespace WebView
{
    public class ItemClickedArgs
    {
        public string? Key { get; set; }
        public MouseEventArgs? MouseEventArgs { get; set; }
    }
}
