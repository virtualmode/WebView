using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.Models
{
    public class OwnedDataItem : DataItem
    {
        public string Owner { get; set; }

        public OwnedDataItem(string parentKey)
        {
            Owner = parentKey;
        }
    }
}
