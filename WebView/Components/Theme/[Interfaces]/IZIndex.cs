using System;
using System.Collections.Generic;
using System.Text;

namespace WebView
{
    public interface IZIndex
    {
        public int FocusStyle { get; }
        public int Coachmark { get; }
        public int Layer { get; }
        public int KeytipLayer { get; }
        public int Nav { get; }
    }
}
