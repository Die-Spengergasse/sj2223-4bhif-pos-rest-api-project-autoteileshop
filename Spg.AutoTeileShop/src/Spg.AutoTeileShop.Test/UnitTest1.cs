using Microsoft.EntityFrameworkCore;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;

namespace Spg.AutoTeileShop.Test
{
    public class UnitTest1
    {
        public AutoTeileShopContext db;

        private AutoTeileShopContext createDB()
        {
            DbContextOptions options = new DbContextOptionsBuilder()
                .UseSqlite("Data Source=AutoTeileShop.db")
                .Options;

            db = new AutoTeileShopContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            return db;
        }

        [Fact]
        public void Create_Catagory_Test()
        {
            AutoTeileShopContext db = createDB();

            Catagory catagory = new Catagory()
            {
                CategoryType = CategoryTypes.MotorTeile,
                Description = "test",
                Name = "test",


            };

            db.Catagories.Add(catagory);
            db.SaveChanges();

            Assert.Equal(1, db.Catagories.Count());

        }

        [Fact]
        public void Create_Catagory_with_TopCatagory_Test()
        {
            AutoTeileShopContext db = createDB();
            

            Catagory TopCatagory = new Catagory()
            {
                CategoryType = CategoryTypes.Fahrwerk,
                Description = "TOPtest1",
                Name = "TOPtest1",


            };
            db.Add(TopCatagory);

            Catagory catagory = new Catagory()
            {
                CategoryType = CategoryTypes.Tuning,
                Description = "test1",
                Name = "test1",
                TopCatagory = TopCatagory
            };
            db.Catagories.Add(catagory);

            db.SaveChanges();

            Assert.Equal(2, db.Catagories.Count());
            Assert.Equal(db.Catagories.Find((long)2).TopCatagory, TopCatagory);

        }

        [Fact]
        public void Create_Product_with_Catagory_Test()
        {
            AutoTeileShopContext db = createDB();

            Catagory Catagory = new Catagory()
            {
                CategoryType = CategoryTypes.Fahrwerk,
                Description = "test1",
                Name = "test1",
            };
            db.Catagories.Add(Catagory);

            Product product = new Product()
            {
                catagory = Catagory,
                Description = "Des Test",
                Guid = Guid.NewGuid(),
                Name = "Pro Test",
                Price = 499.99M
            };
            db.Products.Add(product);
            db.SaveChanges();

            Assert.Equal(1, db.Catagories.Count());
            Assert.Equal(1, db.Products.Count());
            Assert.Equal(db.Products.Find(product.Id).catagory, Catagory);
        }

        [Fact]
        public void Create_Customer_Test()
        {
            AutoTeileShopContext db = createDB();

            Customer customer = new Customer()
            {
                Guid = Guid.NewGuid(),
                Vorname = "Max",
                Nachname = "Musterman",
                Email = "Max.Musterman@gmx.at",
                Strasse = "TestStaﬂe ",
                Telefon = "0004514554"
            };
            db.Customers.Add(customer);
            db.SaveChanges();
            Assert.Equal(1, db.Customers.Count());
        }

        [Fact]
        public void Create_ShoppingCartItem()
        {
            AutoTeileShopContext db = createDB();

            ShoppingCartItem shoppingCartItem = new ShoppingCartItem()
            {
                guid = Guid.NewGuid(),
                Quantity = 1,
            };
            db.ShoppingCartItems.Add(shoppingCartItem);
            db.SaveChanges();
            Assert.Equal(1, db.ShoppingCartItems.Count());
        }

        [Fact]
        public void Create_ShoppingCart()
        {
            AutoTeileShopContext db = createDB();

            ShoppingCart shoppingCart = new ShoppingCart()
            {
                guid = Guid.NewGuid(),

            };

            db.ShoppingCarts.Add(shoppingCart);
            db.SaveChanges();
            Assert.Equal(1, db.ShoppingCarts.Count());
        }

        [Fact]
        public void Create_ShoppingCart_with_ShoppingCartItem_with_Product_and_with_Customer___over_ShoppingCartNav()
        {
            AutoTeileShopContext db = createDB();

            Product product = new Product()
            {
                Description = "Des Test",
                Guid = Guid.NewGuid(),
                Name = "Pro Test",
                Price = 499.99M
            };
            db.Products.Add(product);

            Customer customer = new Customer()
            {
                Guid = Guid.NewGuid(),
                Vorname = "Max",
                Nachname = "Musterman",
                Email = "Max.Muster@gmx.at",
                Strasse = "TestStaﬂe ",
                Telefon = "0004514554"
            };
            db.Customers.Add(customer);

            ShoppingCart shoppingCart = new ShoppingCart()
            {
                guid = Guid.NewGuid(),
                CustomerNav = customer

            };
            db.ShoppingCarts.Add(shoppingCart);

            ShoppingCartItem shoppingCartItem = new ShoppingCartItem()
            {
                guid = Guid.NewGuid(),
                Quantity = 1,
                ProductNav = product,
                ShoppingCartNav = shoppingCart,

            };
            db.ShoppingCartItems.Add(shoppingCartItem);

            ShoppingCartItem shoppingCartItem2 = new ShoppingCartItem()
            {
                guid = Guid.NewGuid(),
                Quantity = 6,
                ProductNav = product,
                ShoppingCartNav = shoppingCart,
            };
            db.ShoppingCartItems.Add(shoppingCartItem2);


            db.SaveChanges();

            //Count Test
            Assert.Equal(1, db.Customers.Count());
            Assert.Equal(1, db.Products.Count());
            Assert.Equal(1, db.ShoppingCarts.Count());
            Assert.Equal(2, db.ShoppingCartItems.Count());
            Assert.Equal(1, db.Products.Count());

            //Relationen Test
            Assert.Equal(db.ShoppingCarts.Find(shoppingCart.Id).CustomerNav, customer);
            Assert.Equal(db.ShoppingCartItems.Find(shoppingCartItem.Id).ProductNav, product);
            Assert.Equal(db.ShoppingCartItems.Find(shoppingCartItem.Id).ShoppingCartNav, shoppingCart);
            Assert.Equal(db.ShoppingCartItems.Find(shoppingCartItem2.Id).ProductNav, product);
            Assert.Equal(db.ShoppingCartItems.Find(shoppingCartItem2.Id).ShoppingCartNav, shoppingCart);

        }

        [Fact]
        public void Create_ShoppingCart_with_ShoppingCartItem_with_Product_and_with_Customer___over_ShoppingCartItemList()
        {
            AutoTeileShopContext db = createDB();

            Product product = new Product()
            {
                Description = "Des Test",
                Guid = Guid.NewGuid(),
                Name = "Pro Test",
                Price = 499.99M
            };
            db.Products.Add(product);

            Product product2 = new Product()
            {
                Description = "Des Test2",
                Guid = Guid.NewGuid(),
                Name = "Pro Test2",
                Price = 499.99M
            };
            db.Products.Add(product2);

            Customer customer = new Customer()
            {
                Guid = Guid.NewGuid(),
                Vorname = "Max",
                Nachname = "Musterman",
                Email = "Max.Muster@gmx.at",
                Strasse = "TestStaﬂe ",
                Telefon = "0004514554"
            };
            db.Customers.Add(customer);

            ShoppingCartItem shoppingCartItem = new ShoppingCartItem()
            {
                guid = Guid.NewGuid(),
                Quantity = 1,
                ProductNav = product,

            };
            db.ShoppingCartItems.Add(shoppingCartItem);

            ShoppingCartItem shoppingCartItem2 = new ShoppingCartItem()
            {
                guid = Guid.NewGuid(),
                Quantity = 6,
                ProductNav = product2,
            };
            db.ShoppingCartItems.Add(shoppingCartItem2);

            ShoppingCart shoppingCart = new ShoppingCart()
            {
                guid = Guid.NewGuid(),
                CustomerNav = customer
            };
            shoppingCart.AddShoppingCartItem(shoppingCartItem);
            shoppingCart.AddShoppingCartItem(shoppingCartItem2);

            db.ShoppingCarts.Add(shoppingCart);


            db.SaveChanges();

            //Count Test
            Assert.Equal(1, db.Customers.Count());
            Assert.Equal(2, db.Products.Count());
            Assert.Equal(1, db.ShoppingCarts.Count());
            Assert.Equal(2, db.ShoppingCartItems.Count());
            Assert.Equal(2, db.Products.Count());

            //Relationen Test
            Assert.Equal(db.ShoppingCarts.Find(shoppingCart.Id).CustomerNav, customer);
            Assert.Equal(db.ShoppingCartItems.Find(shoppingCartItem.Id).ProductNav, product);
            Assert.Equal(db.ShoppingCartItems.Find(shoppingCartItem2.Id).ProductNav, product2);

            Assert.Equal(db.ShoppingCarts.Find(shoppingCart.Id).ShoppingCartItems.Count(), 2);
            Assert.Equal(db.ShoppingCarts.Find(shoppingCart.Id).ShoppingCartItems.First(), shoppingCartItem);
            Assert.Equal(db.ShoppingCarts.Find(shoppingCart.Id).ShoppingCartItems.Last(), shoppingCartItem2);
        }

        [Fact]
        public void Create_Warehouse()
        {

            AutoTeileShopContext db = createDB();

            Warehouse warehouse = new Warehouse()
            {
                Guid = Guid.NewGuid(),
            };


            db.Warehouses.Add(warehouse);


            db.SaveChanges();
            Assert.Equal(1, db.Warehouses.Count());


        }

        [Fact]
        public void Create_Warehouse_with_Product()
        {

            AutoTeileShopContext db = createDB();

            Product product = new Product()
            {
                Description = "Des Test",
                Guid = Guid.NewGuid(),
                Name = "Pro Test",
                Price = 499.99M
            };
            db.Products.Add(product);

            Product product2 = new Product()
            {
                Description = "Des Test2",
                Guid = Guid.NewGuid(),
                Name = "Pro Test2",
                Price = 499.99M
            };
            db.Products.Add(product2);

            Warehouse warehouse = new Warehouse()
            {
                Guid = Guid.NewGuid(),

            };
            warehouse.AddProduct(product);
            warehouse.AddProduct(product2);

            db.Warehouses.Add(warehouse);
            db.SaveChanges();
            Assert.Equal(1, db.Warehouses.Count());
            Assert.Equal(2, db.Products.Count());
            Assert.Equal(product, db.Warehouses.Find(warehouse.Id).Products.First());
            Assert.Equal(product2, db.Warehouses.Find(warehouse.Id).Products.Last());

        }
    }
}
