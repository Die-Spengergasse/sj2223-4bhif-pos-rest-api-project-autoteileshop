using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Spg.AutoTeileShop.Application.Services.CQS.Car;
using Spg.AutoTeileShop.Application.Services.CQS.Car.Commands;
using Spg.AutoTeileShop.Application.Services.CQS.Car.Queries;
using Spg.AutoTeileShop.ApplicationTest.Helpers;
using Spg.AutoTeileShop.Domain;
using Spg.AutoTeileShop.Domain.Exeptions;
using Spg.AutoTeileShop.Domain.Interfaces;
using Spg.AutoTeileShop.Domain.Interfaces.Generic_Repository_Interfaces;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;
using Spg.AutoTeileShop.Repository2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Spg.AutoTeileShop.ApplicationTest
{
    public class CQS_UnitTests
    {
        private AutoTeileShopContext createDB()
        {
            DbContextOptions options = new DbContextOptionsBuilder()
                  .UseSqlite(ReadLineWithQuestionMark())
                //.UseSqlite(@"Data Source= D:/4 Klasse/Pos1 Repo/sj2223-4bhif-pos-rest-api-project-autoteileshop/Spg.AutoTeileShop/src/AutoTeileShop.db")      //Laptop
                //.UseSqlite(@"Data Source = I:\Dokumente 4TB\HTL\4 Klasse\POS1 Git Repo\sj2223-4bhif-pos-rest-api-project-autoteileshop\Spg.AutoTeileShop\src\Spg.AutoTeileShop.API\db\AutoTeileShop.db")     //Home PC       
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
            var serviceProvider = new TestServiceProvider();
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
            var serviceProvider = new TestServiceProvider();
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
            var serviceProvider = new TestServiceProvider();
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
            var serviceProvider = new TestServiceProvider();
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
            var serviceProvider = new TestServiceProvider();
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
            var serviceProvider = new TestServiceProvider();
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
        // ChatGPT:
        [Fact]
        public async Task CQS_GetCarsByMarkeAndModellAndBaujahr_TestAsync()
        {
            // Arrange
            var db = createDB();
            var serviceProvider = new TestServiceProvider();
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
            var serviceProvider = new TestServiceProvider();
            var mediator = (IMediator)serviceProvider.GetService(typeof(IMediator));

            var product = new Product() {
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
            var serviceProvider = new TestServiceProvider();
            var mediator = (IMediator)serviceProvider.GetService(typeof(IMediator));

            var baujahr = DateTime.Now;

            var query = new GetCarsByBaujahrQuery(baujahr);

            // Act
            var result = await mediator.QueryAsync<GetCarsByBaujahrQuery, IEnumerable<Car>>(query);

            // Assert
            // Fügen Sie hier Ihre Assertions hinzu, um das erwartete Ergebnis mit dem tatsächlichen Ergebnis zu vergleichen
        }

        [Fact]
        public async Task CQS_GetCarsByMarke_TestAsync()
        {
            // Arrange
            var db = createDB();
            var serviceProvider = new TestServiceProvider();
            var mediator = (IMediator)serviceProvider.GetService(typeof(IMediator));

            var marke = "BMW";

            var query = new GetCarsByMarkeQuery(marke);

            // Act
            var result = await mediator.QueryAsync<GetCarsByMarkeQuery, IEnumerable<Car>>(query);

            // Assert
            // Fügen Sie hier Ihre Assertions hinzu, um das erwartete Ergebnis mit dem tatsächlichen Ergebnis zu vergleichen
        }
    }
}

