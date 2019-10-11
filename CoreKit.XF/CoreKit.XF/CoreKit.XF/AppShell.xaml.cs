using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace CoreKit.XF
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            //if (propertyName == nameof(CurrentState))
            //{
            //    string path = CurrentState.Location.OriginalString;
            //    this.FlyoutBehavior = path.EndsWith("dashboard", System.StringComparison.Ordinal) || path.EndsWith("home", System.StringComparison.Ordinal) ? FlyoutBehavior.Flyout : FlyoutBehavior.Disabled;
            //    Shell.SetFlyoutBehavior(this.CurrentItem, this.FlyoutBehavior);
            //}
        }
    }
}