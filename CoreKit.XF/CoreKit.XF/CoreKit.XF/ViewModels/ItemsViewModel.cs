using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using CoreKit.XF.Models;
using CoreKit.XF.Views;
using CoreKit.XF.Services;

namespace CoreKit.XF.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {

        public IDataStore<Item> DataStore => new MockDataStore();
        //public IDataStore<Item> DataStore => new ItemDataStore();

        public ObservableCollection<Item> Items { get; set; }
        public ObservableCollection<Category> Categories { get; set; }

        public Item SelectedItem { get; set; }
        public Category SelectedCategory { get; set; }

        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            Categories = new ObservableCollection<Category>();

            //InitializeData();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        public void InitializeData()
        {
            ExecuteLoadItemsCommand();
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }

                Categories.Clear();
                var categories = await DataStore.GetCategoriesAsync();
                foreach (var item in categories)
                {
                    Categories.Add(item);
                }
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