using System;
using System.Collections.Generic;
using System.Text;

namespace WebView.Models
{
    public interface IGroup
    {
        ICollection<IRibbonItem> ItemsSource { get; set; }
    }
}
