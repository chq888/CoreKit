using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CoreKit.XF.Droid.Renderers;
using CoreKit.XF.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace CoreKit.XF.Droid.Renderers
{
    public class CustomEntryRenderer : EntryRenderer
    {

        public CustomEntryRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            this.Control?.SetBackgroundColor(Android.Graphics.Color.AliceBlue);
        }
    }
}