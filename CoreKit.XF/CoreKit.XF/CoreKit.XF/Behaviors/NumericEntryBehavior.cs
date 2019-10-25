using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CoreKit.XF.Behaviors
{
    public class NumericEntryBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);

            bindable.TextChanged += TextChanged_Handler;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= TextChanged_Handler;

            base.OnDetachingFrom(bindable);
        }

        void TextChanged_Handler(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                return;
            }

            double currentValue;
            if (!double.TryParse(e.NewTextValue, out currentValue))
            {
                ((Entry)sender).Text = e.OldTextValue;
            }
        }
    }

}
