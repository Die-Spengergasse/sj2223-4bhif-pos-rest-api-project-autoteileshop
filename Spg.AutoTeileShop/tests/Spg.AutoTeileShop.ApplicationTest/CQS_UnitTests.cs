using Microsoft.EntityFrameworkCore;
using Spg.AutoTeileShop.Application.Services.CQS.Car.Commands;
using Spg.AutoTeileShop.Application.Services.CQS.Car.Queries;
using Spg.AutoTeileShop.ApplicationTest.Helpers;
using Spg.AutoTeileShop.Domain.Exeptions;
using Spg.AutoTeileShop.Domain.Interfaces;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;

namespace Spg.AutoTeileShop.ApplicationTest
{
    public class CQS_UnitTests
    {
        private AutoTeileShopContext createDB()
        {
            DbContextOptions options = new DbContextOptionsBuilder()
                //  .UseSqlite(ReadLineWithQuestionMark())
                //.UseLazyLoadingProxies()
                .UseSqlite("DataSource=D:/4 Klasse/Pos1 Repo/sj2223-4bhif-pos-rest-api-project-autoteileshop/Spg.AutoTeileShop/src/AutoTeileShop.db")     //Home PC       
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
        public async Task CQS_Create_Car_TestAsync()
        {
            //Arrange
            //Datenbank
            AutoTeileShopContext db = createDB();
            var serviceProvider = new TestServiceProvider(db);
            var mediator = (IMediator)serviceProvider.GetService(typeof(IMediator));
            //Act
            Car car = new()
            {
                Baujahr = DateTime.Now,
                Marke = "BMW",
                Modell = "M3"
            };

            CreateCarCommand command = new CreateCarCommand(car);
            var result = await mediator.ExecuteAsync<CreateCarCommand, Car>(command);
            //Assert

            Assert.NotNull(result);
            Assert.Equal(car, result);
            Assert.Single(db.Cars.ToList());

        }

        [Fact]
        public async Task CQS_GetById_Car_TestAsync()
        {
            //Arrange
            //Datenbank
            AutoTeileShopContext db = createDB();
            var serviceProvider = new TestServiceProvider(db);
            var mediator = (IMediator)serviceProvider.GetService(typeof(IMediator));
            Car car = new()
            {
                Baujahr = DateTime.Now,
                Marke = "BMW",
                Modell = "M3"
            };
            CreateCarCommand commandCreate = new CreateCarCommand(car);
            var car1 = await mediator.ExecuteAsync<CreateCarCommand, Car>(commandCreate);


            Assert.Equal(car.Id, car1.Id);
            Assert.Single(db.Cars.ToList());

            //Act



            GetCarByIdQuery queryReadById = new GetCarByIdQuery(1);
            Car result = await mediator.QueryAsync<GetCarByIdQuery, Car>(queryReadById);


            //Assert

            car.Baujahr = result.Baujahr;
            Assert.NotNull(result);
            Assert.Equal(car.ToString(), result.ToString());
            Assert.Single(db.Cars.ToList());

        }

        [Fact]
        public async Task CQS_GetById_Car__throw_CarNotFoundException_TestAsync()
        {
            //Arrange
            //Datenbank
            AutoTeileShopContext db = createDB();
            var serviceProvider = new TestServiceProvider(db);
            var mediator = (IMediator)serviceProvider.GetService(typeof(IMediator));
            Car car = new()
            {
                Baujahr = DateTime.Now,
                Marke = "BMW",
                Modell = "M3"
            };
            CreateCarCommand commandCreate = new CreateCarCommand(car);
            var car1 = await mediator.ExecuteAsync<CreateCarCommand, Car>(commandCreate);


            Assert.Equal(car.Id, car1.Id);
            Assert.Single(db.Cars.ToList());

            //Act - Assert

            GetCarByIdQuery queryReadById = new GetCarByIdQuery(2);
            Assert.ThrowsAsync<CarNotFoundException>(async () => await mediator.QueryAsync<GetCarByIdQuery, Car>(queryReadById));
        }

        [Fact]
        public async Task CQS_Delete_Car_TestAsync()
        {
            //Arrange
            //Datenbank
            AutoTeileShopContext db = createDB();
            var serviceProvider = new TestServiceProvider(db);
            var mediator = (IMediator)serviceProvider.GetService(typeof(IMediator));

            Car car = new()
            {
                Baujahr = DateTime.Now,
                Marke = "BMW",
                Modell = "M3"
            };

            CreateCarCommand command = new CreateCarCommand(car);
            var carResult = await mediator.ExecuteAsync<CreateCarCommand, Car>(command);
            //Act

            var result = await mediator.ExecuteAsync<DeleteCarCommand, int>(new DeleteCarCommand(car));

            //Assert

            Assert.NotNull(result);
            Assert.Equal(car.Id, result);
            Assert.Empty(db.Cars.ToList());

        }

        [Fact]
        public async Task CQS_Update_Car_TestAsync()
        {
            //Arrange
            //Datenbank
            AutoTeileShopContext db = createDB();
            var serviceProvider = new TestServiceProvider(db);
            var mediator = (IMediator)serviceProvider.GetService(typeof(IMediator));

            Car car = new()
            {
                Baujahr = DateTime.Now,
                Marke = "BMW",
                Modell = "M3"
            };

            CreateCarCommand command = new CreateCarCommand(car);
            var carResult = await mediator.ExecuteAsync<CreateCarCommand, Car>(command);
            //Act

            car.Marke = "Benz";
            var result = await mediator.ExecuteAsync<UpdateCarCommand, Car>(new UpdateCarCommand(car));

            //Assert

            Assert.NotNull(result);
            Assert.Equal(car.Marke, result.Marke);
            Assert.Equal("Benz", result.Marke);
            Assert.Single(db.Cars.ToList());
        }

        [Fact]
        public async Task CQS_GetAll_Cars_TestAsync()
        {
            //Arrange
            //Datenbank
            AutoTeileShopContext db = createDB();
            var serviceProvider = new TestServiceProvider(db);
            var mediator = (IMediator)serviceProvider.GetService(typeof(IMediator));
            Car car = new()
            {
                Baujahr = DateTime.Now,
                Marke = "BMW",
                Modell = "M3"
            };
            Car car2 = new()
            {
                Baujahr = DateTime.Now,
                Marke = "Audi",
                Modell = "A3"
            };
            CreateCarCommand commandCreate = new CreateCarCommand(car);
            var car1 = await mediator.ExecuteAsync<CreateCarCommand, Car>(commandCreate);
            CreateCarCommand commandCreate2 = new CreateCarCommand(car2);
            var car1_2 = await mediator.ExecuteAsync<CreateCarCommand, Car>(commandCreate2);

            Assert.Equal(2, db.Cars.Count());

            //Act

            GetAllCarsQuery queryGetAllCars = new GetAllCarsQuery();
            IQueryable<Car> result = await mediator.QueryAsync<GetAllCarsQuery, IQueryable<Car>>(queryGetAllCars);


            //Assert
            foreach (Car c in result)
            {
                c.Baujahr = DateTime.Today;
            }

            for (int i = 0; i < result.Count(); i++)
            {
                Assert.Equal(result.ToList().ElementAt(i).Id, db.Cars.ToList().ElementAt(i).Id);
            }

            Assert.NotNull(result);
            Assert.Equal(result.ToList().ToString(), db.Cars.ToList().ToString());

        }
        //IDK
        [Fact]
        public async Task CQS_GetCarsByMarkeAndModellAndBaujahr_TestAsync()
        {
            // Arrange
            var db = createDB();
            var serviceProvider = new TestServiceProvider(db);
            var mediator = (IMediator)serviceProvider.GetService(typeof(IMediator));

            var marke = "BMW";
            var modell = "M3";
            var baujahr = DateTime.Now;

            Car car = new Car()
            {
                Marke = marke,
                Modell = modell,
                Baujahr = baujahr

            };
            CreateCarCommand command = new CreateCarCommand(car);
            var result1 = await mediator.ExecuteAsync<CreateCarCommand, Car>(command);

            Car car2 = new Car()
            {
                Marke = "test",
                Modell = "test",
                Baujahr = baujahr

            };
            var query = new GetCarsByMarkeAndModellAndBaujahrQuery(marke, modell, baujahr);

            // Act
            var result = await mediator.QueryAsync<GetCarsByMarkeAndModellAndBaujahrQuery, IEnumerable<Car>>(query);

            // Assert
            car.Baujahr = DateTime.Today;
            result.First().Baujahr = DateTime.Today;
            Assert.Equal(result.First().ToString(), car.ToString());
        }

        [Fact]
        public async Task CQS_GetCarsByFitProduct_TestAsync()
        {
            // Arrange
            var db = createDB();
            var serviceProvider = new TestServiceProvider(db);
            var mediator = (IMediator)serviceProvider.GetService(typeof(IMediator));

            var product = new Product()
            {
                Description = "test",
                Guid = Guid.NewGuid(),
                Image = "test",
                Name = "test",
                Price = 1.0M,
            };
            db.Products.Add(product);
            db.SaveChanges();

            Car car = new Car()
            {
                Marke = "BMW",
                Modell = "M1",
                Baujahr = DateTime.Now

            };

            car.AddFitsForProducts(product);
            Car car2 = new Car()
            {
                Marke = "test",
                Modell = "test",
                Baujahr = DateTime.Now

            };

            db.Cars.Add(car);
            db.Cars.Add(car2);
            db.SaveChanges();


            var query = new GetCarsByFitProductQuery(product);

            // Act
            var result = await mediator.QueryAsync<GetCarsByFitProductQuery, IEnumerable<Car>>(query);

            // Assert
            car.Baujahr = DateTime.Today;
            result.First().Baujahr = DateTime.Today;

            Assert.Equal(car.ToString(), result.First().ToString());
        }

        [Fact]
        public async Task CQS_GetCarsByBaujahr_TestAsync()
        {
            // Arrange
            var db = createDB();
            var serviceProvider = new TestServiceProvider(db);
            var mediator = (IMediator)serviceProvider.GetService(typeof(IMediator));

            var baujahr = DateTime.Now;

            var query = new GetCarsByBaujahrQuery(baujahr);

            Car car = new Car()
            {
                Marke = "BMW",
                Modell = "M1",
                Baujahr = baujahr

            };

            Car car2 = new Car()
            {
                Marke = "Audi",
                Modell = "A3 Sport",
                Baujahr = baujahr

            };

            Car car3 = new Car()
            {
                Marke = "test",
                Modell = "test",
                Baujahr = DateTime.Now.AddYears(3)

            };

            db.Cars.Add(car);
            db.Cars.Add(car2);
            db.Cars.Add(car3);

            db.SaveChanges();


            // Act
            var result = await mediator.QueryAsync<GetCarsByBaujahrQuery, IEnumerable<Car>>(query);

            // Assert
            //car.Baujahr = DateTime.Today;
            //result.First().Baujahr = DateTime.Today;

            Assert.Equal(car.ToString(), result.First().ToString());
            Assert.Equal(car2.ToString(), result.Last().ToString());
            Assert.False(result.Contains(car3));
        }

        [Fact]
        public async Task CQS_GetCarsByMarke_TestAsync()
        {
            // Arrange
            var db = createDB();
            var serviceProvider = new TestServiceProvider(db);
            var mediator = (IMediator)serviceProvider.GetService(typeof(IMediator));

            var marke = "BMW";

            var query = new GetCarsByMarkeQuery(marke);

            Car car = new Car()
            {
                Marke = "BMW",
                Modell = "M1",
                Baujahr = DateTime.Now

            };

            Car car2 = new Car()
            {
                Marke = "BMW",
                Modell = "A3 Sport",
                Baujahr = DateTime.Now

            };

            Car car3 = new Car()
            {
                Marke = "test",
                Modell = "test",
                Baujahr = DateTime.Now.AddDays(50)

            };

            db.Cars.Add(car);
            db.Cars.Add(car2);
            db.Cars.Add(car3);

            db.SaveChanges();


            // Act
            var result = await mediator.QueryAsync<GetCarsByMarkeQuery, IEnumerable<Car>>(query);

            // Assert

            Assert.Equal(car.ToString(), result.First().ToString());
            Assert.Equal(car2.ToString(), result.Last().ToString());
            Assert.False(result.Contains(car3));
        }

        //Filtering Paging, Sorting
        [Fact]
        public async Task CQS_GetAllCarsQuery_Filter_And_Sor_TestAsync()
        {
            // Arrange
            // Datenbank
            AutoTeileShopContext db = createDB();
            var serviceProvider = new TestServiceProvider(db);
            var mediator = (IMediator)serviceProvider.GetService(typeof(IMediator));

            Car car1 = new Car
            {
                Baujahr = DateTime.Now,
                Marke = "BMW",
                Modell = "M3"
            };
            Car car2 = new Car
            {
                Baujahr = DateTime.Now,
                Marke = "Audi",
                Modell = "A3"
            };


            db.Cars.AddRange(car1, car2);
            db.SaveChanges();

            Assert.Equal(2, db.Cars.Count());

            // Act
            GetAllCarsQuery query = new GetAllCarsQuery
            {
                Filter = c => c.Marke == "BMW",    // Beispiel für Filterung
                SortBy = c => c.Modell,            // Beispiel für Sortierung nach Modell
                SortDescending = false,            // Aufsteigende Sortierung
                PageNumber = 1,                    // Erste Seite
                PageSize = 1                        // Eine Seite mit einem Element
            };

            var result = await mediator.QueryAsync<GetAllCarsQuery, IQueryable<Car>>(query);

            // Assert
            Assert.Single(result);   // Es sollte nur ein Auto zurückgegeben werden, da wir nur eine Seite mit einem Element angefordert haben

            Car carResult = result.FirstOrDefault();
            Assert.NotNull(carResult);   // Das zurückgegebene Auto sollte nicht null sein
            Assert.Equal(car1.Id, carResult.Id);   // Das zurückgegebene Auto sollte das erwartete Auto sein

            // Die folgenden Assertions können je nach Anforderungen angepasst werden
            Assert.Equal("BMW", carResult.Marke);
            Assert.Equal("M3", carResult.Modell);
        }

        [Fact]
        public async Task CQS_GetAllCarsQuery_Filter_And_Sort_multibleCars_TestAsync()
        {
            // Arrange
            // Datenbank
            AutoTeileShopContext db = createDB();
            var serviceProvider = new TestServiceProvider(db);
            var mediator = (IMediator)serviceProvider.GetService(typeof(IMediator));

            Car car1 = new Car
            {
                Baujahr = DateTime.Now,
                Marke = "BMW",
                Modell = "M3"
            };
            Car car2 = new Car
            {
                Baujahr = DateTime.Now,
                Marke = "BMW",
                Modell = "A3"
            };
            Car car3 = new Car
            {
                Baujahr = DateTime.Now,
                Marke = "Audi",
                Modell = "A3"
            };


            db.Cars.AddRange(car1, car2, car3);
            db.SaveChanges();

            Assert.Equal(3, db.Cars.Count());

            // Act
            GetAllCarsQuery query = new GetAllCarsQuery
            {
                Filter = c => c.Marke == "BMW",    // Beispiel für Filterung
                SortBy = c => c.Modell,            // Beispiel für Sortierung nach Modell
                SortDescending = false,            // Aufsteigende Sortierung
                PageNumber = 1,                    // Erste Seite
                PageSize = 10                       // Eine Seite mit einem Element
            };

            var result = await mediator.QueryAsync<GetAllCarsQuery, IQueryable<Car>>(query);

            // Assert
            Assert.Equal(result.Count(), 2);   // Es sollte nur ein Auto zurückgegeben werden, da wir nur eine Seite mit einem Element angefordert haben

            Car carResult1 = result.FirstOrDefault();
            Car carResult2 = result.Last();

            Assert.NotNull(carResult1);   // Das zurückgegebene Auto sollte nicht null sein
            Assert.NotNull(carResult2);
            Assert.Equal(car2.Id, carResult1.Id);   // Das zurückgegebene Auto sollte das erwartete Auto sein
            Assert.Equal(car1.Id, carResult2.Id);

            Assert.Equal("BMW", carResult1.Marke);
            Assert.Equal("A3", carResult1.Modell);
        }

        [Fact]
        public async Task CQS_GetAllCarsQuery_Filter__And_SortDescending_multibleCars_TestAsync()
        {
            // Arrange
            // Datenbank
            AutoTeileShopContext db = createDB();
            var serviceProvider = new TestServiceProvider(db);
            var mediator = (IMediator)serviceProvider.GetService(typeof(IMediator));

            Car car1 = new Car
            {
                Baujahr = DateTime.Now,
                Marke = "BMW",
                Modell = "M3"
            };
            Car car2 = new Car
            {
                Baujahr = DateTime.Now,
                Marke = "BMW",
                Modell = "A3"
            };
            Car car3 = new Car
            {
                Baujahr = DateTime.Now,
                Marke = "Audi",
                Modell = "A3"
            };


            db.Cars.AddRange(car1, car2, car3);
            db.SaveChanges();

            Assert.Equal(3, db.Cars.Count());

            // Act
            GetAllCarsQuery query = new GetAllCarsQuery
            {
                Filter = c => c.Marke == "BMW",    // Beispiel für Filterung
                SortBy = c => c.Modell,            // Beispiel für Sortierung nach Modell
                SortDescending = true,            // Aufsteigende Sortierung
                PageNumber = 1,                    // Erste Seite
                PageSize = 10                       // Eine Seite mit einem Element
            };

            var result = await mediator.QueryAsync<GetAllCarsQuery, IQueryable<Car>>(query);

            // Assert
            Assert.Equal(result.Count(), 2);   // Es sollte nur ein Auto zurückgegeben werden, da wir nur eine Seite mit einem Element angefordert haben

            Car carResult1 = result.FirstOrDefault();
            Car carResult2 = result.Last();

            Assert.NotNull(carResult1);   // Das zurückgegebene Auto sollte nicht null sein
            Assert.NotNull(carResult2);
            Assert.Equal(car1.Id, carResult1.Id);   // Das zurückgegebene Auto sollte das erwartete Auto sein
            Assert.Equal(car2.Id, carResult2.Id);

            Assert.Equal("BMW", carResult1.Marke);
            Assert.Equal("M3", carResult1.Modell);
        }

        [Fact]
        public async Task CQS_GetAllCarsQuery_Paging_multibleCars_TestAsync()
        {
            // Arrange
            // Datenbank
            AutoTeileShopContext db = createDB();
            var serviceProvider = new TestServiceProvider(db);
            var mediator = (IMediator)serviceProvider.GetService(typeof(IMediator));

            Car car1 = new Car
            {
                Baujahr = DateTime.Now,
                Marke = "BMW",
                Modell = "M3"
            };
            Car car2 = new Car
            {
                Baujahr = DateTime.Now,
                Marke = "BMW",
                Modell = "A3"
            };
            Car car3 = new Car
            {
                Baujahr = DateTime.Now,
                Marke = "Audi",
                Modell = "A3"
            };

            Car car4 = new Car
            {
                Baujahr = DateTime.Now,
                Marke = "Audi",
                Modell = "A2"
            };


            db.Cars.AddRange(car1, car2, car3, car4);
            db.SaveChanges();

            Assert.Equal(4, db.Cars.Count());

            // Act
            GetAllCarsQuery query = new GetAllCarsQuery
            {
                Filter = null,    // Beispiel für Filterung
                SortBy = null,            // Beispiel für Sortierung nach Modell
                SortDescending = false,            // Aufsteigende Sortierung
                PageNumber = 1,                    // Erste Seite
                PageSize = 2                       // Eine Seite mit einem Element
            };

            GetAllCarsQuery query2 = new GetAllCarsQuery
            {
                Filter = null,    // Beispiel für Filterung
                SortBy = null,            // Beispiel für Sortierung nach Modell
                SortDescending = false,            // Aufsteigende Sortierung
                PageNumber = 2,                    // Erste Seite
                PageSize = 2                       // Eine Seite mit einem Element
            };
            var result = await mediator.QueryAsync<GetAllCarsQuery, IQueryable<Car>>(query);

            var result2 = await mediator.QueryAsync<GetAllCarsQuery, IQueryable<Car>>(query2);


            // Assert
            Assert.Equal(result.Count(), 2);   // Es sollte nur ein Auto zurückgegeben werden, da wir nur eine Seite mit einem Element angefordert haben

            Car carResult1 = result.FirstOrDefault();
            Car carResult2 = result.OrderBy(r => r.Id).Last();

            Car carResult3 = result2.FirstOrDefault();
            Car carResult4 = result2.OrderBy(r => r.Id).Last();

            Assert.NotNull(carResult1);   // Das zurückgegebene Auto sollte nicht null sein
            Assert.NotNull(carResult2);
            Assert.NotNull(carResult3);
            Assert.NotNull(carResult4);
            Assert.Equal(car1.Id, carResult1.Id);   // Das zurückgegebene Auto sollte das erwartete Auto sein
            Assert.Equal(car2.Id, carResult2.Id);
            Assert.Equal(car3.Id, carResult3.Id);
            Assert.Equal(car4.Id, carResult4.Id);

            Assert.Equal("BMW", carResult1.Marke);
            Assert.Equal("M3", carResult1.Modell);
            Assert.Equal("BMW", carResult2.Marke);
            Assert.Equal("A3", carResult2.Modell);
        }

    }
}

