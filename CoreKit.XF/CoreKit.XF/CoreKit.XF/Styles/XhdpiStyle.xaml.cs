using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CoreKit.XF.Styles
{
    public partial class XhdpiStyle : ResourceDictionary
    {
        public static XhdpiStyle SharedInstance => new XhdpiStyle();
        public XhdpiStyle()
        {
            InitializeComponent();
        }
    }
}
