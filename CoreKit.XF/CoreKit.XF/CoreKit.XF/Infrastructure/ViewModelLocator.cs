using CoreKit.XF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreKit.XF.Infrastructure
{
    //https://github.com/lbugnion/mvvmlight/blob/master/GalaSoft.MvvmLight/GalaSoft.MvvmLight.Extras%20(PCL)/Ioc/SimpleIoc.cs
    public static class ViewModelLocator
    {
        public static ItemsViewModel ItemsVM
        {
            get { return ServiceLocator.Current.Resolve<ItemsViewModel> (); }
        }

        static ViewModelLocator()
        {
            //Microsoft.Practices.ServiceLocation.ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            //SimpleIoc.Default.Register<ItemsViewModel>();

            ServiceLocator.Current.Register<ItemsViewModel>(new ItemsViewModel());
        }
    }
}
