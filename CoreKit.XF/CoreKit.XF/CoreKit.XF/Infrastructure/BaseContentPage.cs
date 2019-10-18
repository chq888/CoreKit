using CoreKit.XF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CoreKit.XF.Infrastructure
{

    public abstract class BaseContentPage : ContentPage
    {

        protected bool isInitialized;
        protected bool isUninitialized;

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

        }

    }


    public abstract class BaseContentPage<T> : BaseContentPage
            where T : BaseViewModel
    {

        protected virtual T ViewModel => BindingContext as T;

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!isInitialized)
            {
                ViewModel.InitializeAsync();
                isInitialized = true;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (!isUninitialized)
            {
                ViewModel.UninitializeAsync();
                isUninitialized = true;
            }
        }
    }
}
