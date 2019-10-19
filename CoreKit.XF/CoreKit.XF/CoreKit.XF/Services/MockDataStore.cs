using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreKit.XF.Models;
using CoreKit.XF.Models;

namespace CoreKit.XF.Services
{
    //public class MockDataStore : IDataStore<Item>
    //{
    //    readonly List<Item> items;

    //    public MockDataStore()
    //    {
    //        items = new List<Item>()
    //        {
    //            //Guid.NewGuid().ToString()
    //            //new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
    //            //new Item { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
    //            //new Item { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." },
    //            //new Item { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description." },
    //            //new Item { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." },
    //            //new Item { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." }
    //            new Item { Text = "First item", Description="This is an item description." },
    //            new Item { Text = "Second item", Description="This is an item description." },
    //            new Item { Text = "Third item", Description="This is an item description." },
    //            new Item { Text = "Fourth item", Description="This is an item description." },
    //            new Item { Text = "Fifth item", Description="This is an item description." },
    //            new Item { Text = "Sixth item", Description="This is an item description." }
    //        };
    //    }

    //    public async Task<bool> AddItemAsync(Item item)
    //    {
    //        items.Add(item);

    //        return await Task.FromResult(true);
    //    }

    //    public async Task<bool> UpdateItemAsync(Item item)
    //    {
    //        var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
    //        items.Remove(oldItem);
    //        items.Add(item);

    //        return await Task.FromResult(true);
    //    }

    //    public async Task<bool> DeleteItemAsync(int id)
    //    {
    //        var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
    //        items.Remove(oldItem);

    //        return await Task.FromResult(true);
    //    }

    //    public async Task<Item> GetItemAsync(int id)
    //    {
    //        return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
    //    }

    //    public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
    //    {
    //        return await Task.FromResult(items);
    //    }
    //}


    public class MockDataStore : IDataStore<Item>
    {
        private readonly List<Category> mockCategories;
        private readonly List<Item> mockItems;
        private int nextItemId;

     
        public MockDataStore()
        {
            mockCategories = new List<Category>
            {
                new Category() { Id = 1, Name = "Xamarin.Forms"},
                 new Category() { Id = 2, Name = "Xamarin Android"},
                  new Category() { Id = 3, Name = "Xamarin IOS"}
            };

            mockItems = new List<Item>
            {
                new Item { Id = 1, CategoryId=1, Name="Forms",
                    Description="With Xamarin.Forms", Category=mockCategories[0] },
                new Item { Id = 2, CategoryId=2, Name="Android",
                    Description="With Xamarin Android", Category=mockCategories[1] },
                new Item { Id = 3, CategoryId=3, Name="IOS",
                    Description="With Xamarin IOS", Category=mockCategories[2] }
            };

            nextItemId = mockItems.Count;
        }

        public async Task<bool> AddItemAsync(Item currentItem)
        {
            lock (this)
            {
                currentItem.Id = nextItemId;
                mockItems.Add(currentItem);
                nextItemId++;
            }
            return await Task.FromResult(true);
        }

        //public async Task<int> AddItemAsync(Item currentItem)
        //{
        //    lock (this)
        //    {
        //        currentItem.Id = nextItemId;
        //        mockItems.Add(currentItem);
        //        nextItemId++;
        //    }
        //    return await Task.FromResult(currentItem.Id);
        //}

        public async Task<bool> UpdateItemAsync(Item currentItem)
        {
            var noteIndex = mockItems.FindIndex((Item arg) => arg.Id == currentItem.Id);
            var noteFound = noteIndex != -1;
            if (noteFound)
            {
                mockItems[noteIndex].Name = currentItem.Name;
                mockItems[noteIndex].Description = currentItem.Description;
                mockItems[noteIndex].Category = currentItem.Category;
            }
            return await Task.FromResult(noteFound);
        }

        public async Task<Item> GetItemAsync(int id)
        {
            //Item testItem = null;
            //foreach(var loopItem in mockItems)
            //{
            //    if (loopItem.Id == id)
            //    {
            //        testItem = loopItem;
            //        break;
            //    }
            //}
            //int i = 0;
            var note = mockItems.FirstOrDefault(currentItem => currentItem.Id == id);

            // Make a copy of the note to simulate reading from an external datastore
            var returnItem = CopyItem(note);
            return await Task.FromResult(returnItem);
        }

        public async Task<IList<Item>> GetItemsAsync()
        {
            // Make a copy of the notes to simulate reading from an external datastore
            var returnItems = new List<Item>();
            foreach (var note in mockItems)
                returnItems.Add(CopyItem(note));
            return await Task.FromResult(returnItems);
        }

        public async Task<IList<Category>> GetCategoriesAsync()
        {
            return await Task.FromResult(mockCategories);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = mockItems.Where((Item arg) => arg.Id == id).FirstOrDefault();
            mockItems.Remove(oldItem);

            return await Task.FromResult(true);
        }


        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(mockItems);
        }

        private static Item CopyItem(Item item)
        {
            return new Item { Id = item.Id, Name = item.Name, Description = item.Description, Category = item.Category };
        }

    }

}