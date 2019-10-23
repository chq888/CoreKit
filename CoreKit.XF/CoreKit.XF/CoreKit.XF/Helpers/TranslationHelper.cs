using CoreKit.XF.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoreKit.XF.Helpers
{
    // ContentProperty to tell xaml processor that value to be translated will be placed in a property named Text
    [ContentProperty("Text")]
    public class Translation : IMarkupExtension
    {
        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return string.Empty;

            // find a match for a locale which is passed in as AppResource.Culture
            // ResourceManager find a match for the text of a locale; if fail, will use default language
            return CoreKit.XF.Resources.AppResource.ResourceManager.GetString(Text, CoreKit.XF.Resources.AppResource.Culture);
        }
    }


    public static class TranslationHelper
    {

        /// <summary>
        /// just test language and international settings without having to change the language and country settings on a device or emulator.
        /// </summary>
        /// <param name="locale"></param>
        public static void ChangeCulture(string locale)
        {
            // CurrentCulture sets the locale, which affects how numbers and dates are formatted.
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(locale);

            //CurrentUICulture sets the language for the app.
            Thread.CurrentThread.CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentCulture;
        }

    }

}
