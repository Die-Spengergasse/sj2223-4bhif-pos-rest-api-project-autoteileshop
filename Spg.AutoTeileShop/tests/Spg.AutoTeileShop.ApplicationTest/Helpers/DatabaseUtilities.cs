using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
//using Spg.AutoTeileShop.

namespace Spg.AutoTeileShop.ApplicationTest.Helpers
{
    public static class DatabaseUtilities
    {
        public static DbContextOptions GetDbOptions()
        {
            // Das garantiert eine DB-Connection die von EF Core nicht automatisch geschlossen wird
            SqliteConnection connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();



            return new DbContextOptionsBuilder()
                .UseSqlite(connection)
                .Options;
        }



        public static void InitializeDatabase(AutoTeileShopContext db)
        {
            db.Database.EnsureCreated();



            //db.Shops.AddRange(GetSeedingShops());
            //db.SaveChanges();



            //db.Catagories.AddRange(GetSeedingCategories(db.Shops.Single(s => s.Id == 1)));
            //db.Categories.AddRange(GetSeedingCategories(db.Shops.Single(s => s.Id == 2)));
            //db.SaveChanges();



            //db.Customers.AddRange(GetSeedingCustomers());
            //db.SaveChanges();



            //// Seed Products
            //db.Products.AddRange(GetSeedingProducts(db.Categories.Single(s => s.Id == 1)));
            //db.SaveChanges();



            // Seed ...
            // db.SaveChanges();
        }



        //private static List<Shop> GetSeedingShops()
        //{
        //    return new List<Shop>()
        //    {
        //        new Shop("GMBH", "Test Shop 1", "Test Location 1", "IDontKnow 1", "Bs 1", new Address("Spengergasse", "20", "1050", "Wien"), new Guid("0c03ceb5-e2a2-4faf-b273-63839505f573")),
        //        new Shop("GMBH", "Test Shop 2", "Test Location 2", "IDontKnow 2", "Bs 2", new Address("Spengergasse", "21", "1051", "Wien"), new Guid("a0a6b711-fd27-4193-8595-325a62d82c5c")),
        //    };
        //}



        private static List<Catagory> GetSeedingCategories()
        {
            var cat1 = new Catagory(null, "MotorKopf", "Test Description", CategoryTypes.MotorTeile);

            return new List<Catagory>()
            {   cat1,
                new Catagory(cat1, "Zylinder", "Test Description2", CategoryTypes.Motor),
            };
        }



        private static List<User> GetSeedingUser()
        {
            return new List<User>()
            {

                new User
                ( new Guid("6ecfca13-f862-4c74-ac0e-30a2a62dd128"),
                "Max", "Mustermann", "Musterstr. 1", "0123456789",
                "max.mustermann@example.com", "password123", Roles.Admin, true),

                new User
                ( new Guid("gu5lda13-kfh5-8574-kc0e-40a2a62dd963"),
                "Anna", "Schmidt", "Hauptstr. 42", "0987654321",
                "anna.schmidt@example.com", "password456", Roles.User, false)
            };
        }

        private static List<Car> GetSeedingCars_without_Product()
        {
            return new List<Car>()
            {
                new Car("BMW", "M3", DateTime.Now.AddYears(-1)),
                new Car("Benz", "C63 AMG", DateTime.Now.AddYears(-2))
            };
        }

        private static List<Product> GetSeedingProducts(Catagory category, AutoTeileShopContext db)
        {
            return new List<Product>()
            {
                new Product(
                    Guid.NewGuid(),
                    "Test Product",
                    19.99m,
                    category,
                    "This is a test product.",
                    "test-image.jpg",
                    QualityType.SehrGut,
                    100,
                    10,
                    DateTime.Now.AddDays(-14),
                    db.Cars.ToList()),
                
            };


        }


    }
}
