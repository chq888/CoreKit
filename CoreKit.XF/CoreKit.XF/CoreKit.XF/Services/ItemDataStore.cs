using CoreKit.XF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CoreKit.XF.Services
{

    public class ItemDataStore : IDataStore<Item>
    {
        public async Task<bool> AddItemAsync(Item item)
        {
            int count = 0;
            using (var context = DbContextFactory.Instance.CreateItemDbContext())
            {
                try
                {
                    var currentItem = context.Items.Find(item.Id);

                    if (currentItem == null)
                    {
                        context.Items.Add(item);
                    }
                    else
                    {
                        currentItem.Id = item.Id;
                        currentItem.Text = item.Text;
                        currentItem.Description = item.Description;

                        context.Items.Update(currentItem);
                    }
                }
                catch(Exception ex)
                {
                    context.Add(item);
                }

                count = await context.SaveChangesAsync();
            }

            return await Task.FromResult(count == 1);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            int count = 0;
            using (var context = new ItemDbContext())
            {
                var item = context.Items.Where(p => p.Id == id).FirstOrDefault();
                context.Remove<Item>(item);
                //context.Items.Remove(item);
                count = await context.SaveChangesAsync();
            }
            return await Task.FromResult(count == 1);
        }

        public async Task<Item> GetItemAsync(int id)
        {
            using (var context = new ItemDbContext())
            {
                return await context.FindAsync<Item>(id);
            }
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            using (var context = new ItemDbContext())
            {
                return await context.Items.ToListAsync();
                //var items = await context.Items.ToList();
                //return await Task.FromResult(items);
            }
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            int count = 0;
            using (var context = new ItemDbContext())
            {
                context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                count = await context.SaveChangesAsync();
            }
            return await Task.FromResult(count == 1);
        }
    }

}
