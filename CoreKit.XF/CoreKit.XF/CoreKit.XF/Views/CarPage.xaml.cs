﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoreKit.XF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarPage : ContentPage
    {
        public CarPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            Resources["CustomLabelStyle"] = Resources["Custom1LabelStyle"];

            base.OnAppearing();
        }
    }
}