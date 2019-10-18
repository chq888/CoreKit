using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;

namespace CoreKit.XF.Models
{

    public class ItemDbContext : DbContext
    {

        public DbSet<Item> Items { get; set; }

        //protected string DatabasePath
        //{
        //    get
        //    {
        //        try
        //        {
        //            //return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), _databaseName);
        //            //return $"{FileSystem.AppDataDirectory}{Path.DirectorySeparatorChar}{_databaseName}";
        //            return $"{FileSystem.CacheDirectory}{Path.DirectorySeparatorChar}{_databaseName}";
        //        }
        //        catch
        //        {
        //            return _databaseName;
        //        }
        //    }
        //}

        public ItemDbContext()
        {
            // TODO check version here
            //this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var dbPath = DependencyService.Get<IDatabaseService>().GetDatabasePath();
            //optionsBuilder.UseSqlite($"Filename={dbPath}");
            optionsBuilder.UseSqlite($"Data Source={DbContextFactory.Instance.DatabasePath}");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //SeedInitialData(modelBuilder);
        }

        private void SeedInitialData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().HasData(
                    new Item()
                    {
                        Text = "test 1",
                        Description = "test desc"
                    });
        }

    }

}
