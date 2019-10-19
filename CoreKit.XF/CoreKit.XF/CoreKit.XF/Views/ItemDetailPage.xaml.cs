using System;
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
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        //public ItemDetailPage()
        //{
        //    InitializeComponent();

        //    var item = new Item
        //    {
        //        Name = "Item 1",
        //        Description = "This is an item description.",
        //        CategoryId = 1
        //    };

        //    viewModel = new ItemDetailViewModel(item);
        //    BindingContext = viewModel;
        //}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Categories.Count == 0)
                viewModel.LoadDataCommand.Execute(null);
        }

    }
}