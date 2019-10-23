using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CoreKit.XF.Models;
using CoreKit.XF.ViewModels;

namespace CoreKit.XF.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewItemPage : ContentPage
    {

        ItemDetailViewModel viewModel;

        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            Item = new Item
            {
                Name = "Item name",
                Description = "This is an item description.",
                CategoryId = 1
            };

            //BindingContext = this;
            BindingContext = viewModel = new ItemDetailViewModel();
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", viewModel.CurrentItem);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}