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
        public DbSet<User> Users { get; set; }
        public DbSet<Catagory> Catagories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Car> Cars{ get; set; }
        public DbSet<UserMailConfirme> UserMailConfirms { get; set; }


        public AutoTeileShopContext(DbContextOptions options) : base(options)
        {
        }

        protected AutoTeileShopContext() : this(new DbContextOptions<DbContext>())
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
                options.UseSqlite("Data Source= I:\\Dokumente 4TB\\HTL\\4 Klasse\\POS1 Git Repo\\sj2223-4bhif-pos-rest-api-project-autoteileshop\\Spg.AutoTeileShop\\src\\AutoTeileShop.db"); //Home PC
                            //  D:/4 Klasse/Pos1 Repo/sj2223-4bhif-pos-rest-api-project-autoteileshop/Spg.AutoTeileShop/src/AutoTeileShop.db"     //Laptop
            
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        }

        public void Seed()
        {
            Randomizer.Seed = new Random(1017);

            List<User> users = new Faker<User>("de")
                .Rules((f, c) =>
                {
                    c.Guid = f.Random.Guid();
                    c.Vorname = f.Name.FirstName(Bogus.DataSets.Name.Gender.Female);
                    c.Nachname = f.Name.LastName();
                    c.Email = f.Internet.Email(c.Vorname, c.Nachname);
                    c.Telefon = f.Person.Phone;
                    c.Addrese = f.Address.FullAddress();
                    c.PW = f.Internet.Password();                  
                })
            .Generate(50)
            .ToList();
            Users.AddRange(users);
            SaveChanges();

            
            List<Car> cars = new Faker<Car>("de")
            .Rules((f, ca) =>
            {
                ca.Marke = f.Vehicle.Manufacturer();
                ca.Modell = f.Vehicle.Model();
                ca.Baujahr = f.Date.Past(15, DateTime.Now);                
            })
            .Generate(60)
            .ToList();
            Cars.AddRange(cars);
            SaveChanges();


            List<Catagory> catagory = new Faker<Catagory>("de")
            .Rules((f, ca) =>
            {

                ca.Description = f.Name.JobDescriptor();

                Array values = Enum.GetValues(typeof(CategoryTypes));
                Random random = new Random();                
                ca.CategoryType = (CategoryTypes)values.GetValue(random.Next(values.Length));
                ca.Name = ca.CategoryType.ToString();
                
            })
            .Generate(50)
            .ToList();
            Catagories.AddRange(catagory);
            SaveChanges();

            
            List<Product> product = new Faker<Product>("de")
            .Rules((f, p) =>
            {
                
                p.Guid = f.Random.Guid();
                p.Name = f.Commerce.ProductName();
                p.Price = f.Random.Decimal(0, 1000);
                p.catagory = f.PickRandom(catagory);
                p.Description = f.Commerce.ProductDescription();
                p.Image = f.Image.PicsumUrl();

                Array values = Enum.GetValues(typeof(QualityType));
                Random random = new Random();
                p.Quality= (QualityType)values.GetValue(random.Next(values.Length));

                p.Stock = f.Random.Int(11, 1000);
                p.Discount = f.Random.Int(0, 100);
                p.receive = f.Date.Past(10, DateTime.Now);
                p.AddProductFitsForCar(f.PickRandom(cars));
                

            })
            .Generate(500)
            .ToList();
            product.AddRange(product);
            SaveChanges();

            List<ShoppingCartItem> shoppingCartItems = new Faker<ShoppingCartItem>("de")
                   .Rules((f, shI) =>
                   {
                       shI.guid = f.Random.Guid();
                       shI.Pieces = f.Random.Int(1, 9);
                       shI.ProductNav = f.PickRandom(product);
                   })
                   .Generate(100)
                   .ToList();
            ShoppingCartItems.AddRange(shoppingCartItems);
            SaveChanges();


            List<ShoppingCart> shoppingCart = new Faker<ShoppingCart>("de")
           .Rules((f, sh) =>
           {
               sh.guid = f.Random.Guid();
               //sh.UserNav = f.PickRandom(Users);
               for(int i = 0; i < 2; i++)
               {
                   sh.AddShoppingCartItem(f.PickRandom(shoppingCartItems));
               }
           })
           .Generate(50)
           .ToList();
            ShoppingCarts.AddRange(shoppingCart);
            SaveChanges();


            List<UserMailConfirme> userMailConfirmes = new Faker<UserMailConfirme>("de")
           .Rules((f, u) =>
           {
               u.User = f.PickRandom(users);
               u.Code = Guid.NewGuid().ToString().Substring(0, 8);
               //u.UserId = u.User.Id;
           })
           .Generate(50)
           .ToList();
        }

       




    }
}
