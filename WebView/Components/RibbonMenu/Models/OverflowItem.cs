using System;
using System.Collections.Generic;
using System.Text;

namespace WebView.Models
{
    public class OverflowItem : IOverflowSetItem
    {
        public string Key { get; set; }

        public string Name { get; set; }

        public OverflowItem(int id)
        {
            Key = id.ToString();
            Name = id.ToString();
        }
    }
}
