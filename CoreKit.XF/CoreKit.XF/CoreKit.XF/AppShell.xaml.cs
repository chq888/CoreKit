using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace CoreKit.XF
{
    public partial class AppShell : Xamarin.Forms.Shell
    {

        //List<HomeMenuItem> menuItems;

        //public enum MenuItemType
        //{
        //    Browse,
        //    About
        //}
        //public class HomeMenuItem
        //{
        //    public MenuItemType Id { get; set; }

        //    public string Title { get; set; }
        //}

        //Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        //public async Task NavigateFromMenu(int id)
        //{
        //    if (!MenuPages.ContainsKey(id))
        //    {
        //        switch (id)
        //        {
        //            case (int)MenuItemType.Browse:
        //                MenuPages.Add(id, new NavigationPage(new ItemsPage()));
        //                break;
        //            case (int)MenuItemType.About:
        //                MenuPages.Add(id, new NavigationPage(new AboutPage()));
        //                break;
        //        }
        //    }

        //    var newPage = MenuPages[id];

        //    if (newPage != null && Detail != newPage)
        //    {
        //        Detail = newPage;

        //        if (Device.RuntimePlatform == Device.Android)
        //            await Task.Delay(100);

        //        IsPresented = false;
        //    }
        //}

        public AppShell()
        {
            InitializeComponent();

            //menuItems = new List<HomeMenuItem>
            //{
            //    new HomeMenuItem {Id = MenuItemType.Browse, Title="Browse" },
            //    new HomeMenuItem {Id = MenuItemType.About, Title="About" }
            //};
            //MenuPages.Add((int)MenuItemType.Browse, (NavigationPage)Detail);


            //            <StackLayout VerticalOptions="FillAndExpand">
            //    <ListView x:Name="ListViewMenu"
            //                HasUnevenRows="True">
            //        <ListView.ItemTemplate>
            //            <DataTemplate>
            //                <ViewCell>
            //                    <Grid Padding="10">
            //                        <Label Text="{Binding Title}" FontSize="20"/>
            //                    </Grid>
            //                </ViewCell>
            //            </DataTemplate>
            //        </ListView.ItemTemplate>
            //    </ListView>
            //</StackLayout>
            //        ListViewMenu.ItemsSource = menuItems;

            //        ListViewMenu.SelectedItem = menuItems[0];
            //        ListViewMenu.ItemSelected += async (sender, e) =>
            //        {
            //            if (e.SelectedItem == null)
            //                return;

            //            var id = (int)((HomeMenuItem)e.SelectedItem).Id;
            //            await RootPage.NavigateFromMenu(id);
            //};
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