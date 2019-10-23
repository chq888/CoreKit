using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoreKit.XF.Resources
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ColorDictionary : ResourceDictionary
    {
        public ColorDictionary()
        {
            InitializeComponent();
        }
    }
}