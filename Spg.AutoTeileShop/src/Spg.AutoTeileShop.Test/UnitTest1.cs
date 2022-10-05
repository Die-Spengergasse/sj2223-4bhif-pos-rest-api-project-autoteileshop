using Microsoft.EntityFrameworkCore;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;

namespace Spg.AutoTeileShop.Test
{
    public class UnitTest1
    {
        

        private AutoTeileShopContext createDB()
        {
            DbContextOptions options = new DbContextOptionsBuilder()
                .UseSqlite("Data Source=AutoTeileShop.db")
                .Options;

            AutoTeileShopContext db = new AutoTeileShopContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            return db;
        }

        [Fact]
        public void DomainModel_Create_Catagory_Test()
        {
            //Arrange

            AutoTeileShopContext db = createDB();
            Catagory catagory = new Catagory()
            {
                CategoryType = CategoryTypes.MotorTeile,
                Description = "test",
                Name = "test"
            };

            //Act

            db.Catagories.Add(catagory);
            db.SaveChanges();

            //Assert

            Assert.Equal(1, db.Catagories.Count());

        }

        [Fact]
        public void DomainModel_Create_Catagory_with_TopCatagory_Test()
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
        public void DomainModel_Create_Product_with_Catagory_Test()
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
        public void DomainModel_Create_Customer_Test()
        {
            AutoTeileShopContext db = createDB();

            Customer customer = new Customer()
            {
                Guid = Guid.NewGuid(),
                Vorname = "Max",
                Nachname = "Musterman",
                Email = "Max.Musterman@gmx.at",
                Strasse = "TestSta�e ",
                Telefon = "0004514554"
            };
            db.Customers.Add(customer);
            db.SaveChanges();
            Assert.Equal(1, db.Customers.Count());
        }

        [Fact]
        public void DomainModel_Create_ShoppingCartItem()
        {
            AutoTeileShopContext db = createDB();

            ShoppingCartItem shoppingCartItem = new ShoppingCartItem()
            {
                guid = Guid.NewGuid(),
                Pieces = 1,
            };
            db.ShoppingCartItems.Add(shoppingCartItem);
            db.SaveChanges();
            Assert.Equal(1, db.ShoppingCartItems.Count());
        }

        [Fact]
        public void DomainModel_Create_ShoppingCart()
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
        public void DomainModel_Create_ShoppingCart_with_ShoppingCartItem_with_Product_and_with_Customer___over_ShoppingCartNav()
        {
            //Arrange

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
                Strasse = "TestSta�e ",
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
                Pieces = 1,
                ProductNav = product,
                ShoppingCartNav = shoppingCart,

            };
            db.ShoppingCartItems.Add(shoppingCartItem);

            ShoppingCartItem shoppingCartItem2 = new ShoppingCartItem()
            {
                guid = Guid.NewGuid(),
                Pieces = 6,
                ProductNav = product,
                ShoppingCartNav = shoppingCart,
            };
            db.ShoppingCartItems.Add(shoppingCartItem2);

            //Act
            db.SaveChanges();

            //Assert

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
        public void DomainModel_Create_ShoppingCart_with_ShoppingCartItem_with_Product_and_with_Customer___over_ShoppingCartItemList()
        {
            AutoTeileShopContext db = createDB();

            Catagory Catagory = new Catagory()
            {
                CategoryType = CategoryTypes.Fahrwerk,
                Description = "test1",
                Name = "test1",
            };
            db.Catagories.Add(Catagory);

            Catagory Catagory2 = new Catagory()
            {
                CategoryType = CategoryTypes.Tuning,
                Description = "test2",
                Name = "test2",
            };
            db.Catagories.Add(Catagory2);

            Product product = new Product()
            {
                Description = "Des Test",
                Guid = Guid.NewGuid(),
                Name = "Pro Test",
                Price = 499.99M,
                Stock = 10,
                catagory = Catagory
            };
            db.Products.Add(product);

            Product product2 = new Product()
            {
                Description = "Des Test2",
                Guid = Guid.NewGuid(),
                Name = "Pro Test2",
                Price = 499.99M,
                Stock = 10,
                catagory = Catagory2
            };
            db.Products.Add(product2);

            Customer customer = new Customer()
            {
                Guid = Guid.NewGuid(),
                Vorname = "Max",
                Nachname = "Musterman",
                Email = "Max.Muster@gmx.at",
                Strasse = "TestSta�e ",
                Telefon = "0004514554"
            };
            db.Customers.Add(customer);

            ShoppingCartItem shoppingCartItem = new ShoppingCartItem()
            {
                guid = Guid.NewGuid(),
                Pieces = 1,
                ProductNav = product,

            };
            db.ShoppingCartItems.Add(shoppingCartItem);

            ShoppingCartItem shoppingCartItem2 = new ShoppingCartItem()
            {
                guid = Guid.NewGuid(),
                Pieces = 1,
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

            Console.WriteLine(shoppingCart.ShoppingCartItems.Count());

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

            Assert.Equal(2, (db.ShoppingCarts.Find(shoppingCart.Id).ShoppingCartItems).Count());
            Assert.Equal(db.ShoppingCarts.Find(shoppingCart.Id).ShoppingCartItems.First(), shoppingCartItem);
            Assert.Equal(db.ShoppingCarts.Find(shoppingCart.Id).ShoppingCartItems.Last(), shoppingCartItem2);
        }

        [Fact]
        public void DomainModel_Create_Car_Test()
        {
            AutoTeileShopContext db = createDB();

            Car car = new Car()
            {
                Baujahr = DateTime.Now,
                Marke = "BMW",
                Modell = "M3",
            };
            db.Cars.Add(car);
            db.SaveChanges();

            Assert.Equal(1, db.Cars.Count());
        }

        [Fact]
        public void DomainModel_Create_Car_With_Product_Test()
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

            Car car = new Car()
            {
                Baujahr = DateTime.Now,
                Marke = "BMW",
                Modell = "M3",
            };
            product.AddProductFitsForCar(car);
            db.Cars.Add(car);
            db.SaveChanges();

            Assert.Equal(1, db.Cars.Count());
            Assert.Equal(1, db.Products.Count());
        }

        [Fact]
        public void DomainModel_Add_ShoppingCarItem_to_ShoppingCar_Test()
        {
            AutoTeileShopContext db = createDB();

            Product product = new Product()
            {
                Description = "Des Test",
                Guid = Guid.NewGuid(),
                Name = "Pro Test",
                Price = 499.99M,
                Stock = 5
            };
            db.Products.Add(product);

            ShoppingCartItem shoppingCartItem = new ShoppingCartItem()
            {
                guid = Guid.NewGuid(),
                Pieces = 1,
                ProductNav = product,
            };
            db.ShoppingCartItems.Add(shoppingCartItem);

            ShoppingCart shoppingCart = new ShoppingCart()
            {
                guid = Guid.NewGuid(),
            };
            shoppingCart.AddShoppingCartItem(shoppingCartItem);
            db.ShoppingCarts.Add(shoppingCart);
            db.SaveChanges();

            Assert.Equal(shoppingCart, shoppingCartItem.ShoppingCartNav);
            Assert.Equal(1, db.ShoppingCarts.Count());
            Assert.Equal(1, db.ShoppingCartItems.Count());
            Assert.Equal(1, db.Products.Count());
        }

        [Fact]
        public void DomainModel_Add_ShoppingCarItem_to_ShoppingCar_False_More_Pices_than_Stock_Test()
        {
            AutoTeileShopContext db = createDB();
            Product product = new Product()
            {
                Description = "Des Test",
                Guid = Guid.NewGuid(),
                Name = "Pro Test",
                Price = 499.99M,
                Stock = 1
            };

            db.Products.Add(product);

            ShoppingCartItem shoppingCartItem = new ShoppingCartItem()
            {
                guid = Guid.NewGuid(),
                Pieces = 2,
                ProductNav = product,
            };
            db.ShoppingCartItems.Add(shoppingCartItem);

            ShoppingCart shoppingCart = new ShoppingCart()
            {
                guid = Guid.NewGuid(),
            };
            try
            {
                bool worked = shoppingCart.AddShoppingCartItem(shoppingCartItem);
                db.ShoppingCarts.Add(shoppingCart);
   
            }
            catch (Exception e)
            {
                Assert.Equal("Not enough stock", e.Message);
            }

            db.SaveChanges();

            Assert.Equal(0, db.ShoppingCarts.Count());
            Assert.Equal(1, db.ShoppingCartItems.Count());
            Assert.Equal(1, db.Products.Count());
        }


        [Fact]
        public void DomainModel_Add_ShoppingCarItem_to_ShoppingCar_2_Items_of_the_same_Product_Test()
        {
            AutoTeileShopContext db = createDB();
            Product product = new Product()
            {
                Description = "Des Test",
                Guid = Guid.NewGuid(),
                Name = "Pro Test",
                Price = 499.99M,
                Stock = 4
            };

            db.Products.Add(product);

            ShoppingCartItem shoppingCartItem = new ShoppingCartItem()
            {
                guid = Guid.NewGuid(),
                Pieces = 1,
                ProductNav = product,
            };
            db.ShoppingCartItems.Add(shoppingCartItem);

            ShoppingCartItem shoppingCartItem2 = new ShoppingCartItem()
            {
                guid = Guid.NewGuid(),
                Pieces = 1,
                ProductNav = product,
            };
            db.ShoppingCartItems.Add(shoppingCartItem);

            ShoppingCart shoppingCart = new ShoppingCart()
            {
                guid = Guid.NewGuid(),
            };
           
           bool worked = shoppingCart.AddShoppingCartItem(shoppingCartItem);
           bool worked2 = shoppingCart.AddShoppingCartItem(shoppingCartItem);

            db.ShoppingCarts.Add(shoppingCart);

           db.SaveChanges();

            Assert.Equal(1, db.ShoppingCarts.First().ShoppingCartItems.Count());
            Assert.Equal(2, db.ShoppingCarts.First().ShoppingCartItems.First().Pieces);
            
            Assert.Equal(1, db.ShoppingCarts.Count());
            Assert.Equal(1, db.ShoppingCartItems.Count());
            Assert.Equal(1, db.Products.Count());
        }

        [Fact]
        public void DomainModel_Add_Car_to_Product_Test()
        {
            AutoTeileShopContext db = createDB();

            Product product = new Product()
            {
                Description = "Test Product",
                Guid = Guid.NewGuid(),
                Name = "Pro Test",
                Price = 499.99M,
                Stock = 5,
                Quality = QualityType.Mittel,
                Image = "/src/img.jpg",
                Discount = 0,
                receive = DateTime.Now,

            };
            db.Products.Add(product);

            Car car = new Car()
            {
                Baujahr = DateTime.Now,
                Marke = "BMW",
                Modell = "M3",
                
            };
            //db.Cars.Add(car);

            product.AddProductFitsForCar(car);
            db.SaveChanges();
            
            Assert.Equal(1, db.Cars.Count());
            Assert.Equal(1, db.Products.Count());
            Assert.Equal(car, db.Products.First().ProductFitsForCar.First());
            Assert.Equal(product, db.Cars.First().FitsForProducts.First());
        }

        [Fact]
        public void DomainModel_Add_Product_to_Car_Test()
        {
            AutoTeileShopContext db = createDB();

            Product product = new Product()
            {
                Description = "Test Product",
                Guid = Guid.NewGuid(),
                Name = "Pro Test",
                Price = 499.99M,
                Stock = 5,
                Quality = QualityType.Mittel,
                Image = "/src/img.jpg",
                Discount = 0,
                receive = DateTime.Now,

            };
            //db.Products.Add(product);

            Car car = new Car()
            {
                Baujahr = DateTime.Now,
                Marke = "BMW",
                Modell = "M3",

            };
            db.Cars.Add(car);

            car.AddFitsForProducts(product);
            db.SaveChanges();

            Assert.Equal(1, db.Cars.Count());
            Assert.Equal(1, db.Products.Count());
            Assert.Equal(car, db.Products.First().ProductFitsForCar.First());
            Assert.Equal(product, db.Cars.First().FitsForProducts.First());
        }

        [Fact]
        public void DomainModel_Remove_ShoppingCarItem_From_ShoppingCar() 
        {            
            AutoTeileShopContext db = createDB();
            
            Product product = new Product()
            {
                Description = "Des Test",
                Guid = Guid.NewGuid(),
                Name = "Pro Test",
                Price = 499.99M,
                Stock = 4
            };

            db.Products.Add(product);

            ShoppingCartItem shoppingCartItem = new ShoppingCartItem()
            {
                guid = Guid.NewGuid(),
                Pieces = 1,
                ProductNav = product,
            };
            //db.ShoppingCartItems.Add(shoppingCartItem);

            ShoppingCart shoppingCart = new ShoppingCart()
            {
                guid = Guid.NewGuid(),
            };
            
            shoppingCart.AddShoppingCartItem(shoppingCartItem);
            db.ShoppingCarts.Add(shoppingCart);
            db.SaveChanges();

            Assert.Equal(1, db.ShoppingCarts.First().ShoppingCartItems.Count());
            Assert.Equal(shoppingCart, shoppingCartItem.ShoppingCartNav);

            shoppingCart.RemoveShoppingCartItem(shoppingCartItem);
            db.SaveChanges();

            Assert.Equal(null, db.ShoppingCartItems.First().ShoppingCartNav);
            Assert.Equal(0, db.ShoppingCarts.First().ShoppingCartItems.Count());
            Assert.Equal(1, db.ShoppingCarts.Count());
            Assert.Equal(1, db.ShoppingCartItems.Count());
            Assert.Equal(1, db.Products.Count());
        }

        [Fact]
        public void DomainModel_Remove_Product_From_CarList()
        {
            AutoTeileShopContext db = createDB();

            Product product = new Product()
            {
                Description = "Test Product",
                Guid = Guid.NewGuid(),
                Name = "Pro Test",
                Price = 499.99M,
                Stock = 5,
                Quality = QualityType.Mittel,
                Image = "/src/img.jpg",
                Discount = 0,
                receive = DateTime.Now,

            };
            //db.Products.Add(product);

            Car car = new Car()
            {
                Baujahr = DateTime.Now,
                Marke = "BMW",
                Modell = "M3",

            };
            db.Cars.Add(car);

            car.AddFitsForProducts(product);
            db.SaveChanges();

            Assert.Equal(1, db.Cars.Count());
            Assert.Equal(1, db.Products.Count());
            Assert.Equal(car, db.Products.First().ProductFitsForCar.First());
            Assert.Equal(product, db.Cars.First().FitsForProducts.First());

            car.RemoveFitsForProducts(product);
            db.SaveChanges();

            Assert.Equal(1, db.Cars.Count());
            Assert.Equal(1, db.Products.Count());
            Assert.Equal(0, db.Cars.First().FitsForProducts.Count());
            Assert.Equal(0, db.Products.First().ProductFitsForCar.Count());
        }


        [Fact]
        public void DomainModel_Remove_Car_From_ProductgList()
        {
            AutoTeileShopContext db = createDB();
            
            Car car = new Car()
            {
                Baujahr = DateTime.Now,
                Marke = "BMW",
                Modell = "M3",

            };
            //db.Cars.Add(car);
            Product product = new Product()
            {
                Description = "Test Product",
                Guid = Guid.NewGuid(),
                Name = "Pro Test",
                Price = 499.99M,
                Stock = 5,
                Quality = QualityType.Mittel,
                Image = "/src/img.jpg",
                Discount = 0,
                receive = DateTime.Now,

            };
            db.Products.Add(product);


            product.AddProductFitsForCar(car);
            db.SaveChanges();

            Assert.Equal(1, db.Cars.Count());
            Assert.Equal(1, db.Products.Count());
            Assert.Equal(car, db.Products.First().ProductFitsForCar.First());
            Assert.Equal(product, db.Cars.First().FitsForProducts.First());

            car.RemoveFitsForProducts(product);
            db.SaveChanges();

            Assert.Equal(1, db.Cars.Count());
            Assert.Equal(1, db.Products.Count());
            Assert.Equal(0, db.Cars.First().FitsForProducts.Count());
            Assert.Equal(0, db.Products.First().ProductFitsForCar.Count());
        }

        [Fact]
        public void DomainModel_Add_Product_To_CarList_With_Existing_Product() 
        {
            AutoTeileShopContext db = createDB();

            Car car = new Car()
            {
                Baujahr = DateTime.Now,
                Marke = "BMW",
                Modell = "M3",

            };
            db.Cars.Add(car);
            Product product = new Product()
            {
                Description = "Test Product",
                Guid = Guid.NewGuid(),
                Name = "Pro Test",
                Price = 499.99M,
                Stock = 5,
                Quality = QualityType.Mittel,
                Image = "/src/img.jpg",
                Discount = 0,
                receive = DateTime.Now,

            };
            db.Products.Add(product);



            car.AddFitsForProducts(product);
            db.SaveChanges();

            Assert.Equal(1, db.Cars.Count());
            Assert.Equal(1, db.Products.Count());
            Assert.Equal(car, db.Products.First().ProductFitsForCar.First());
            Assert.Equal(product, db.Cars.First().FitsForProducts.First());
        }
        

    }
}
