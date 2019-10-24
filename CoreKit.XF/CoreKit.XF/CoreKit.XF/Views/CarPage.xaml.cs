using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoreKit.XF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarPage : ContentPage
    {
        private CancellationTokenSource throttleCancellationTokenSource = new CancellationTokenSource();

        public CarPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            Resources["CustomLabelStyle"] = Resources["Custom1LabelStyle"];

            base.OnAppearing();
        }

        private void ValueOnTextChanged(object sender, TextChangedEventArgs e)
        {
            // reset delay on processing
            Interlocked.Exchange(ref this.throttleCancellationTokenSource, new CancellationTokenSource()).Cancel();

            // Wait 500ms before updating data
            Task.Delay(TimeSpan.FromMilliseconds(500), this.throttleCancellationTokenSource.Token)
                .ContinueWith((obj) => { this.HandleValue(); },
                    CancellationToken.None,
                    TaskContinuationOptions.OnlyOnRanToCompletion,
                    TaskScheduler.FromCurrentSynchronizationContext());
        }

        public void HandleValue()
        {
            //TODO
        }

    }
}