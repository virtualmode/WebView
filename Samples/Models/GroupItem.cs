using WebView.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Samples.Models
{
    class GroupItem: IGroup
    {
        public ICollection<IRibbonItem> ItemsSource { get; set; } = new ObservableCollection<IRibbonItem>();
    }
}
