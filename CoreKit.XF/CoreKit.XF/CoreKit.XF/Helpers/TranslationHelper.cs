using CoreKit.XF.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        private static readonly System.Resources.ResourceManager _resourceManager;

        public static CultureInfo CurrentLanguage { get; set; }


        static TranslationHelper()
        {
            _resourceManager = CoreKit.XF.Resources.AppResource.ResourceManager;
            CurrentLanguage = new CultureInfo("en-US");
        }

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

        public static string GetText(string namespaceKey, string typeKey, string name)
        {
            string resolvedKey = name;

            if (!string.IsNullOrEmpty(typeKey))
            {
                resolvedKey = $"{typeKey}.{resolvedKey}";
            }

            if (!string.IsNullOrEmpty(namespaceKey))
            {
                resolvedKey = $"{namespaceKey}.{resolvedKey}";
            }

            return GetText(resolvedKey);
        }

        public static string GetText(string namespaceKey, string typeKey, string name, params object[] formatArgs)
        {
            string baseText = GetText(namespaceKey, typeKey, name);

            if (string.IsNullOrEmpty(baseText))
            {
                return baseText;
            }

            return string.Format(baseText, formatArgs);
        }

        public static string GetText(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return name;
            }

            return _resourceManager.GetString(name, CurrentLanguage);
        }

    }


}
