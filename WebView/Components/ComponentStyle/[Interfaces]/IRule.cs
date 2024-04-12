using System;
using System.Collections.Generic;
using System.Text;

namespace WebView
{
    public interface IRule
    {
        public IRuleProperties? Properties { get; set; }
    }
}
