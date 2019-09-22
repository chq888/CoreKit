using CoreKit.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace CoreKit.Data
{
    public class CoreDataContext : DbContext
    {

        public DbSet<Address> Addresses { get; set; } // Address
        public DbSet<Cart> Carts { get; set; } // Cart
        public DbSet<CartItem> CartItems { get; set; } // CartItem
        public DbSet<CartRevisit> CartRevisits { get; set; } // CartRevisit
        public DbSet<Catalog> Catalogs { get; set; } // Catalog
        public DbSet<Category> Categories { get; set; } // Category
        public DbSet<CategoryCatalog> CategoryCatalogs { get; set; } // CategoryCatalog
        public DbSet<Member> Members { get; set; } // Member
        public DbSet<MemberAddress> MemberAddresses { get; set; } // MemberAddress
        public DbSet<Order> Orders { get; set; } // Order
        public DbSet<OrderLine> OrderLines { get; set; } // OrderLine
        public DbSet<Product> Products { get; set; } // Product

        public CoreDataContext(DbContextOptions<CoreDataContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Specifying the Data Provider and Connection String
            //optionsBuilder.UseSqlServer("server=.\\sqlexpress;database=;trusted_connection=true");

            base.OnConfiguring(optionsBuilder);
        }
    }
}
