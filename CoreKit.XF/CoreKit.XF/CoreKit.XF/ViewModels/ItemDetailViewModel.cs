using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using CoreKit.XF.Infrastructure;
using CoreKit.XF.Models;
using CoreKit.XF.Services;
using Xamarin.Forms;
using System.Threading;

namespace CoreKit.XF.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {

        public IDataStore<Item> DataStore => new MockDataStore();
        //public IDataStore<Item> DataStore => new ItemDataStore();

        private CancellationTokenSource _Cancellation;

        private bool _isCanceled;

        public bool IsCanceled
        {
            get => _isCanceled;
            set => SetProperty(ref _isCanceled, value);
        }

        private Item _CurrentItem;
        public Item CurrentItem
        {
            get { return _CurrentItem; }
            set { SetProperty(ref _CurrentItem, value); }
        }

        public ObservableCollection<Category> Categories { get; set; }

        //private Category _SelectedCategory;
        //public Category SelectedCategory
        //{
        //    get { return _SelectedCategory; }
        //    set { SetProperty(ref _SelectedCategory, value); }
        //}


        public Category SelectedCategory
        {
            get { return CurrentItem.Category; }
            set
            {
                CurrentItem.Category = value;
                OnPropertyChanged();
            }
        }

        public Command LoadDataCommand { get; set; }

        public ItemDetailViewModel(Item item = null)
        {
            Categories = new ObservableCollection<Category>();

            Title = item?.Name;
            CurrentItem = item;

            LoadDataCommand = new Command(async () => await ExecuteLoadDataCommand());
        }

        private async void CancelAsync()
        {
            if (_Cancellation?.IsCancellationRequested ?? true)
                return;
            _Cancellation.Cancel();
        }

        private async void DoAsync()
        {
            _Cancellation = new CancellationTokenSource();
            IsCanceled = true;

            //handle here
            await Task.Delay(1000, _Cancellation.Token);
            IsCanceled = false;
        }

        async Task ExecuteLoadDataCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Categories.Clear();
                var categories = await DataStore.GetCategoriesAsync();
                foreach (var item in categories)
                {
                    Categories.Add(item);
                }

                //CurrentItem.Category = Categories.Where(a => a.Id == CurrentItem.CategoryId).FirstOrDefault();
                SelectedCategory = Categories.Where(a => a.Id == CurrentItem.CategoryId).FirstOrDefault();

                //this.OnPropertyChanged("CurrentItem");
                //TrySetProperty(ref _CurrentItem, CurrentItem, nameof(CurrentItem));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
