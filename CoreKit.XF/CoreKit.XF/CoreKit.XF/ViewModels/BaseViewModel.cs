using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using CoreKit.XF.Models;
using CoreKit.XF.Services;
using System.Threading.Tasks;
using CoreKit.XF.Helpers;

namespace CoreKit.XF.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        //public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

        //readonly WeakEventManager _propertyChangedEventManager = new WeakEventManager();

        //event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        //{
        //    add => _propertyChangedEventManager.AddEventHandler(value);
        //    remove => _propertyChangedEventManager.RemoveEventHandler(value);
        //}

        //protected void SetProperty<T>(ref T backingStore, in T value, in System.Action? onChanged = null, [CallerMemberName] in string propertyname = "")
        //{
        //    if (EqualityComparer<T>.Default.Equals(backingStore, value))
        //        return;

        //    backingStore = value;

        //    onChanged?.Invoke();

        //    OnPropertyChanged(propertyname);
        //}

        //void OnPropertyChanged([CallerMemberName] in string propertyName = "") =>
        //    _propertyChangedEventManager.HandleEvent(this, new PropertyChangedEventArgs(propertyName), nameof(INotifyPropertyChanged.PropertyChanged));


        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool TrySetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            return TrySetProperty<T>(ref backingStore, value, propertyName, onChanged);
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;
            
            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public virtual Task InitializeAsync() => Task.CompletedTask;

        public virtual Task UninitializeAsync() => Task.CompletedTask;

        protected async Task<bool> TryExecuteWithLoadingIndicatorsAsync(
          Task operation,
          Func<Exception, Task<bool>> onError = null) =>
          await TaskHelper.Create()
              .WhenStarting(() => IsBusy = true)
              .WhenFinished(() => IsBusy = false)
              .TryWithErrorHandlingAsync(operation, onError);

        protected async Task<T> TryExecuteWithLoadingIndicatorsAsync<T>(
            Task<T> operation,
            Func<Exception, Task<bool>> onError = null) =>
            await TaskHelper.Create()
                .WhenStarting(() => IsBusy = true)
                .WhenFinished(() => IsBusy = false)
                .TryWithErrorHandlingAsync(operation, onError);

    }
}
