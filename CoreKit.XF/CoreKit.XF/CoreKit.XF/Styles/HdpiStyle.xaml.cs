using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CoreKit.XF.Styles
{
    public partial class HdpiStyle : ResourceDictionary
    {
        public static HdpiStyle SharedInstance => new HdpiStyle();
        public HdpiStyle()
        {
            InitializeComponent();
        }
    }
}
