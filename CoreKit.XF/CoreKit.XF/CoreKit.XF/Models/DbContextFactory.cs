using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreKit.XF.Models
{

    public class DbContextFactory
    {

        private static readonly object syncRoot = new object();
        private static DbContextFactory _instance;


        //public static ItemDbContext DbContext { get; private set; }
        //public ItemDbContext DbContext => new ItemDbContext();
        private string _databaseName = "corekit.db";
        private string _databasePath = "";

        public string DatabasePath
        {
            get
            {
                    //return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), _databaseName);
                    //return $"{FileSystem.AppDataDirectory}{Path.DirectorySeparatorChar}{_databaseName}";
                    return $"{_databasePath}{_databaseName}";
            }
        }


        public static DbContextFactory Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }

                lock (syncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new DbContextFactory();
                    }
                }

                return _instance;
            }
        }

        public void SetDatabasePath(string path)
        {
            _databasePath = path;
        }

        public ItemDbContext CreateItemDbContext()
        {
            return new ItemDbContext();
        }

        //public void InitializeDatabase()
        //{
        //    SQLitePCL.Batteries_V2.Init();
        //    using (var context = CreateItemDbContext())
        //    {
        //        context.Database.Migrate();
        //    }
        //}

        public void InitializeDatabase()
        {
            try
            {
                SQLitePCL.Batteries_V2.Init();
                using (var db = DbContextFactory.Instance.CreateItemDbContext())
                {
                    db.Database.EnsureCreated();
                    //db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
            }
        }

    }
  
}
