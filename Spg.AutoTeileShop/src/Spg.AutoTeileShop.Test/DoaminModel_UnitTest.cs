using Microsoft.EntityFrameworkCore;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;

namespace Spg.AutoTeileShop.Domain.Test
{

    [Collection("Sequential tests")]
    public class DoaminModel_UnitTest
    {
        private AutoTeileShopContext createDB()
        {
            DbContextOptions options = new DbContextOptionsBuilder()
            //.UseSqlite(@"Data Source= D:/4 Klasse/Pos1 Repo/sj2223-4bhif-pos-rest-api-project-autoteileshop/Spg.AutoTeileShop/src/AutoTeileShop.db")      //Laptop
            //.UseSqlite(@"Data Source = I:\Dokumente 4TB\HTL\4 Klasse\POS1 Git Repo\sj2223-4bhif-pos-rest-api-project-autoteileshop\Spg.AutoTeileShop\src\Spg.AutoTeileShop.API\db\AutoTeileShop.db")     //Home PC relativ       
            //.UseSqlite("Data Source = I:\\Dokumente 4TB\\HTL\\4 Klasse\\POS1 Git Repo\\sj2223-4bhif-pos-rest-api-project-autoteileshop\\Spg.AutoTeileShop\\src\\AutoTeileShop.db")     //Home PC       
            .UseSqlite(ReadLineWithQuestionMark())
                .Options;

            AutoTeileShopContext db = new AutoTeileShopContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            //db.Seed();
            return db;
        }

        public static string ReadLineWithQuestionMark()
        {
            string relativeFilePath = "DataSource.txt";
            string currentDirectory = Environment.CurrentDirectory;
            int endIndex;
            string extractedPath;
            string filePath;
            if (currentDirectory.Contains($"\\src\\"))
            {
                endIndex = currentDirectory.IndexOf($"\\src\\") + $"\\src\\".Length;
                extractedPath = currentDirectory.Substring(0, endIndex);
                filePath = Path.Combine(extractedPath, relativeFilePath);
            }
            else
            {
                endIndex = currentDirectory.IndexOf($"\\tests\\");
                extractedPath = currentDirectory.Substring(0, endIndex);
                //filePath = Path.Combine(extractedPath, $"\\src\\", relativeFilePath);
                filePath = extractedPath + $"\\src\\" + relativeFilePath;
            }


            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Die Datei wurde nicht gefunden.", relativeFilePath);
            }

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                if (line.TrimStart().StartsWith("?"))
                {
                    return line.TrimStart('?').Trim();
                }
            }

            return null;
        }

        [Fact]
        public void DomainModel_Create_Catagory_Test()
        {
            //Arrange
            AutoTeileShopContext db = createDB();
            Catagory catagory = new Catagory()
            {
                CategoryType = CategoryTypes.MotorTeile,
                Description = "Catagory for MotorTeile Desc",
                Name = "Test Cat"
            };

            //Act
            db.Catagories.Add(catagory);
            db.SaveChanges();

            //Assert
            Assert.Equal(1, db.Catagories.Count());
            Assert.Equal(catagory, db.Catagories.First());

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
            Assert.Equal(db.Catagories.Find(catagory.Id).TopCatagory, TopCatagory);

        }

        [Fact]
        public void DomainModel_Create_Product_with_Catagory_Test()
        {
            AutoTeileShopContext db = createDB();

            Catagory catagory = new Catagory()
            {
                CategoryType = CategoryTypes.Fahrwerk,
                Description = "test1",
                Name = "test1",
            };
            db.Catagories.Add(catagory);

            Product product = new Product()
            {
                catagory = catagory,
                Description = "Des Test",
                Guid = Guid.NewGuid(),
                Name = "Pro Test",
                Price = 499.99M
            };
            db.Products.Add(product);
            db.SaveChanges();

            Assert.Equal(1, db.Catagories.Count());
            Assert.Equal(1, db.Products.Count());
            Assert.Equal(db.Products.Find(product.Id).catagory, catagory);
        }

        [Fact]
        public void DomainModel_Create_User_Test()
        {
            AutoTeileShopContext db = createDB();

            User User = new User()
            {
                Guid = Guid.NewGuid(),
                Vorname = "Max",
                Nachname = "Musterman",
                Email = "Max.Musterman@gmx.at",
                Addrese = "TestStaﬂe ",
                Telefon = "0004514554",
                Role = Roles.User
            };
            db.Users.Add(User);
            db.SaveChanges();
            Assert.Equal(1, db.Users.Count());
        }

        [Fact]
        public void DomainModel_Create_User_2_times_Test()
        {
            AutoTeileShopContext db = createDB();

            User User = new User()
            {
                Guid = Guid.NewGuid(),
                Vorname = "Max",
                Nachname = "Musterman",
                Email = "Max.Musterman@gmx.at",
                Addrese = "TestStaﬂe ",
                Telefon = "0004514554",
                Role = Roles.User
            };
            db.Users.Add(User);
            db.SaveChanges();

            try
            {
                User User2 = new User()
                {
                    Guid = Guid.NewGuid(),
                    Vorname = "Max",
                    Nachname = "Musterman",
                    Email = "Max.Musterman@gmx.at",
                    Addrese = "safasfasf ",
                    Telefon = "14561454",
                    Role = Roles.User
                };
                db.Users.Add(User2);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Assert.True(true);
            }
            Assert.Equal(1, db.Users.Count());
        }


        [Fact]
        public void DomainModel_Create_User_2_times_withoutMail_Test()
        {
            AutoTeileShopContext db = createDB();

            User User = new User()
            {
                Guid = Guid.NewGuid(),
                Vorname = "Max",
                Nachname = "Musterman",
                Addrese = "TestStaﬂe ",
                Telefon = "0004514554",
                Role = Roles.User
            };
            db.Users.Add(User);
            db.SaveChanges();

            try
            {
                User User2 = new User()
                {
                    Guid = Guid.NewGuid(),
                    Vorname = "Max",
                    Nachname = "Musterman",
                    Addrese = "safasfasf ",
                    Telefon = "14561454",
                    Role = Roles.User
                };
                db.Users.Add(User2);
                db.SaveChanges();

            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                Assert.True(true);
            }
            Assert.Equal(1, db.Users.Count());
        }

        [Fact]
        public void DomainModel_Create_ShoppingCartItem_Test()
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
        public void DomainModel_Create_ShoppingCart_Test()
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
        public void DomainModel_Create_ShoppingCart_with_ShoppingCartItem_with_Product_and_with_User___over_ShoppingCartNav_Test()
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

            User User = new User()
            {
                Guid = Guid.NewGuid(),
                Vorname = "Max",
                Nachname = "Musterman",
                Email = "Max.Muster@gmx.at",
                Addrese = "TestStaﬂe ",
                Telefon = "0004514554",
                Role = Roles.User
            };
            db.Users.Add(User);

            ShoppingCart shoppingCart = new ShoppingCart()
            {
                guid = Guid.NewGuid(),
                UserNav = User

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
            Assert.Equal(1, db.Users.Count());
            Assert.Equal(1, db.Products.Count());
            Assert.Equal(1, db.ShoppingCarts.Count());
            Assert.Equal(2, db.ShoppingCartItems.Count());
            Assert.Equal(1, db.Products.Count());

            //Relationen Test
            Assert.Equal(db.ShoppingCarts.Find(shoppingCart.Id).UserNav, User);
            Assert.Equal(db.ShoppingCartItems.Find(shoppingCartItem.Id).ProductNav, product);
            Assert.Equal(db.ShoppingCartItems.Find(shoppingCartItem.Id).ShoppingCartNav, shoppingCart);
            Assert.Equal(db.ShoppingCartItems.Find(shoppingCartItem2.Id).ProductNav, product);
            Assert.Equal(db.ShoppingCartItems.Find(shoppingCartItem2.Id).ShoppingCartNav, shoppingCart);

        }

        [Fact]
        public void DomainModel_Create_ShoppingCart_with_ShoppingCartItem_with_Product_and_with_User___over_ShoppingCartItemList_Test()
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

            User User = new User()
            {
                Guid = Guid.NewGuid(),
                Vorname = "Max",
                Nachname = "Musterman",
                Email = "Max.Muster@gmx.at",
                Addrese = "TestStaﬂe ",
                Telefon = "0004514554",
                Role = Roles.User
            };
            db.Users.Add(User);

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
                UserNav = User
            };
            shoppingCart.AddShoppingCartItem(shoppingCartItem);
            shoppingCart.AddShoppingCartItem(shoppingCartItem2);

            Console.WriteLine(shoppingCart.ShoppingCartItems.Count());

            db.ShoppingCarts.Add(shoppingCart);


            db.SaveChanges();

            //Count Test
            Assert.Equal(1, db.Users.Count());
            Assert.Equal(2, db.Products.Count());
            Assert.Equal(1, db.ShoppingCarts.Count());
            Assert.Equal(2, db.ShoppingCartItems.Count());
            Assert.Equal(2, db.Products.Count());

            //Relationen Test
            Assert.Equal(db.ShoppingCarts.Find(shoppingCart.Id).UserNav, User);
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
            Assert.Throws<Exception>(() => shoppingCart.AddShoppingCartItem(shoppingCartItem));


            db.ShoppingCarts.Add(shoppingCart);

            db.SaveChanges();

            Assert.Equal(1, db.ShoppingCarts.Count());
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

            Assert.Single(db.ShoppingCarts.First().ShoppingCartItems);
            Assert.Equal(2, db.ShoppingCarts.First().ShoppingCartItems.First().Pieces);

            Assert.Single(db.ShoppingCarts);
            Assert.Single(db.ShoppingCartItems);
            Assert.Single(db.Products);
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
        public void DomainModel_Remove_ShoppingCarItem_From_ShoppingCar_Test()
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

            Assert.Single(db.ShoppingCarts.First().ShoppingCartItems);
            Assert.Equal(shoppingCart, shoppingCartItem.ShoppingCartNav);

            shoppingCart.RemoveShoppingCartItem(shoppingCartItem);
            db.SaveChanges();

            Assert.Null(db.ShoppingCartItems.First().ShoppingCartNav);
            Assert.Empty(db.ShoppingCarts.First().ShoppingCartItems);
            Assert.Single(db.ShoppingCarts);
            Assert.Single(db.ShoppingCartItems);
            Assert.Single(db.Products);
        }

        [Fact]
        public void DomainModel_Remove_ShoppingCarItem_From_ShoppingCar_but_Item_does_not_exist_Test()
        {
            AutoTeileShopContext db = createDB();


            ShoppingCart shoppingCart = new ShoppingCart()
            {
                guid = Guid.NewGuid(),
            };

            db.ShoppingCarts.Add(shoppingCart);
            db.SaveChanges();

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

            Assert.Throws<Exception>(() => shoppingCart.RemoveShoppingCartItem(shoppingCartItem));

        }

        [Fact]
        public void DomainModel_Remove_ShoppingCarItem_From_ShoppingCar_decrease_Pieces_Test()
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
                Pieces = 2,
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


            ShoppingCartItem shoppingCartItem2 = new ShoppingCartItem()
            {
                guid = shoppingCartItem.guid,
                Pieces = 1,
                ProductNav = product,
            };


            shoppingCart.RemoveShoppingCartItem(shoppingCartItem2);
            db.SaveChanges();

            Assert.Equal(1, db.ShoppingCarts.First().ShoppingCartItems.First().Pieces);
            Assert.Equal(shoppingCartItem, db.ShoppingCartItems.First());
            Assert.Equal(shoppingCartItem, db.ShoppingCarts.First().ShoppingCartItems.First());
            Assert.Single(db.ShoppingCartItems);


        }

        [Fact]
        public void DomainModel_Remove_Product_From_CarList_Test()
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

            Assert.Single(db.Cars);
            Assert.Single(db.Products);
            Assert.Equal(car, db.Products.First().ProductFitsForCar.First());
            Assert.Equal(product, db.Cars.First().FitsForProducts.First());

            car.RemoveFitsForProducts(product);
            db.SaveChanges();

            Assert.Single(db.Cars);
            Assert.Single(db.Products);
            Assert.Empty(db.Cars.First().FitsForProducts);
            Assert.Empty(db.Products.First().ProductFitsForCar);
        }


        [Fact]
        public void DomainModel_Remove_Car_From_ProductList_Test()
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

            Assert.Single(db.Cars);
            Assert.Single(db.Products);
            Assert.Equal(car, db.Products.First().ProductFitsForCar.First());
            Assert.Equal(product, db.Cars.First().FitsForProducts.First());

            car.RemoveFitsForProducts(product);
            db.SaveChanges();

            Assert.Single(db.Cars);
            Assert.Single(db.Products);
            Assert.Empty(db.Cars.First().FitsForProducts);
            Assert.Empty(db.Products.First().ProductFitsForCar);
        }

        [Fact]
        public void DomainModel_Add_Product_To_CarList_With_Existing_Product_Test()
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

        [Fact]
        public void DomainModel_Add_ShoppingCartItem_to_ShoppingCart_Check_for_Pices_in_ShoppingCartItem_Test()
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

            Assert.True(1 == db.ShoppingCarts.First().ShoppingCartItems.First().Pieces);

            shoppingCart.AddShoppingCartItem(shoppingCartItem);
            db.SaveChanges();

            Assert.True(2 == db.ShoppingCarts.First().ShoppingCartItems.First().Pieces);
            Assert.True(1 == db.ShoppingCarts.First().ShoppingCartItems.Count());
            Assert.True(1 == db.ShoppingCartItems.Count());

        }

        [Fact]
        public void DomainModel_Add_ShoppingCartItem_to_ShoppingCart_Check_for_Pices_in_Stock_Test()
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
            ShoppingCartItem shoppingCartItem = new ShoppingCartItem()
            {
                guid = Guid.NewGuid(),
                Pieces = 2,
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

            Assert.True(3 == db.ShoppingCarts.First().ShoppingCartItems.First().ProductNav.Stock);
        }

        // Servis Async Tests -- Repository 1 Test

        // [Fact] 
        //public async void Async_DomainModel_Service_Add_User_Test()
        //{
        //    AutoTeileShopContext db = createDB();

        //    User User = new User()
        //    {
        //        Guid = Guid.NewGuid(),
        //        Vorname = "Max",
        //        Nachname = "Musterman",
        //        Email = "Max.Musterman@gmx.at",
        //        Addrese = "TestStaﬂe ",
        //        Telefon = "0004514554",
        //        Role = Roles.User

        //    };

        //    Repository<User> UserRepo = new Repository<User>(db);
        //    await UserRepo.AddAsync(User);

        //    Assert.Equal(User, await UserRepo.GetByIdAsync(User.Id));
        //}

        //// [Fact]
        //public async void Async_DomainModel_Service_Find_User_TestAsync()
        //{
        //    AutoTeileShopContext db = createDB();

        //    User User = new User()
        //    {
        //        Guid = Guid.NewGuid(),
        //        Vorname = "Max",
        //        Nachname = "Musterman",
        //        Email = "Max.Musterman@gmx.at",
        //        Addrese = "TestStaﬂe ",
        //        Telefon = "0004514554",
        //        Role = Roles.User
        //    };
        //    Repository<User> UserRepo = new Repository<User>(db);
        //    await UserRepo.AddAsync(User);
        //    Assert.Equal(await UserRepo.GetByIdAsync(User.Id), User);
        //}

        [Fact]
        public void Z_DomainModel_Create_DB_Seed()
        {
            AutoTeileShopContext db = createDB();
            db.Seed();
            Assert.Equal(db.Users.Count(), 50);
            Assert.Equal(db.Cars.Count(), 60);
            Assert.Equal(db.Catagories.Count(), 20);
            Assert.Equal(db.Products.Count(), 500);
            Assert.Equal(db.ShoppingCartItems.Count(), 100);
            Assert.Equal(db.ShoppingCarts.Count(), 50);
            Assert.Equal(db.UserMailConfirms.Count(), 50);


        }
        [Fact]
        public void Z_DomainModel_Create_DB()
        {
            AutoTeileShopContext db = createDB();
        }

        [Fact]
        public void DomainModel_ShoppingCart_ShoppingCartItem_LazyLoading_Test()
        {
            AutoTeileShopContext db = createDB();
            db.Seed();
            Assert.NotNull(db.ShoppingCartItems.First().ShoppingCartNav.Id);
        }
    }
}
