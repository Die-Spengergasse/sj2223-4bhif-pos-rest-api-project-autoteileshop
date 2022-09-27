using Microsoft.EntityFrameworkCore;
using Spg.Spengershop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.Spengershop.Infrastructure
{
    public class SpengerShopContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Catagory> Catagories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }



        public SpengerShopContext(DbContextOptions<SpengerShopContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if(!options.IsConfigured)
                options.UseSqlite("Data Source=Spengershop.db");
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Customer>().ToTable("Customer");
        //    modelBuilder.Entity<ShoppingCartItem>().ToTable("ShoppingCartItem");
        //    modelBuilder.Entity<Catagory>().ToTable("Catagory");
        //    modelBuilder.Entity<Product>().ToTable("Product");
        //    modelBuilder.Entity<ShoppingCart>().ToTable("ShoppingCart");

        //}
    }
}
