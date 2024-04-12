using System;
using System.Collections.Generic;

namespace WebView
{
    public class SelectedDateResult
    {
        public DateTime Date { get; set; }
        public List<DateTime>? SelectedDateRange { get; set; }

        public bool ShouldLetOpenDatePicker { get; set; }
    }
}
