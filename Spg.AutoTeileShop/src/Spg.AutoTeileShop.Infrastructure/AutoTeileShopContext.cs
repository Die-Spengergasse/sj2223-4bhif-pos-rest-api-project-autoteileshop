using Bogus;
using Microsoft.EntityFrameworkCore;
using Spg.AutoTeileShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Infrastructure
{
    public class AutoTeileShopContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Catagory> Catagories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Car> Cars{ get; set; }


        public AutoTeileShopContext(DbContextOptions options) : base(options)
        {
        }

        protected AutoTeileShopContext() : this(new DbContextOptions<DbContext>())
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
                options.UseSqlite("Data Source=AutoTeileShop.db");

            
        }

        //public void Seed()
        //{
        //    Randomizer.Seed = new Random(1017);

        //    List<Customer> customers = new Faker<Customer>("de")
        //        .Rules((f, c) =>
        //        {
        //            //Id = id;
        //            //Guid = guid;
        //            //Vorname = vorname;
        //            //Nachname = nachname;
        //            //Addrese = strasse;
        //            //Telefon = telefon;
        //            //Email = email;
        //            //PW = pW;

        //            c.Guid = f.Random.Guid();
        //            c.Vorname = f.Name.FirstName(Bogus.DataSets.Name.Gender.Female);
        //            c.Nachname = f.Name.LastName();
        //            c.Email = f.Internet.Email();
        //            c.Telefon = f.Person.Phone;
        //            c.Addrese = f.Address.FullAddress();
        //            int i = f.Random.Number(1, 100);
        //            c.Stammkunde = false;
        //            if (i % 5 == 0)
        //                c.Stammkunde = true;

        //        })
        //    .Generate(80)
        //    .ToList();
        //    Customers.AddRange(customers);
        //    SaveChanges();


        //    List<Car> cars = new Faker<Car>("de")
        //    .Rules((f, p) =>
        //    {
        //        p.guid = f.Random.Guid();
        //        p.Besitzer = customers[f.Random.Number(0, 79)];
        //        p.Marke = f.Company.CompanyName();
        //        p.Modell = f.Commerce.ProductName();
        //        p.Kennzeichen = f.Random.Replace("W##-###");
        //        p.Erstzulassung = f.Date.Past(10, DateTime.Now);
        //        p.Kw = f.Random.Decimal(60, 500);

        //    })
        //    .Generate(300)
        //    .ToList();
        //    Cars.AddRange(cars);
        //    SaveChanges();
        //}
    }
}
