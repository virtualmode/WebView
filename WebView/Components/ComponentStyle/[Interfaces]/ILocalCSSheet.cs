using System.Collections.Generic;

namespace WebView
{
    public interface ILocalCSSheet
    {
        ICollection<IRule> Rules { get; set; }
    }
}
