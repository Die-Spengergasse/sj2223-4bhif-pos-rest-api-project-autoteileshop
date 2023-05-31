using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Spg.AutoTeileShop.Application.Services.CQS.Car;
using Spg.AutoTeileShop.Application.Services.CQS.Car.Commands;
using Spg.AutoTeileShop.Application.Services.CQS.Car.Queries;
using Spg.AutoTeileShop.ApplicationTest.Helpers;
using Spg.AutoTeileShop.Domain;
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
                  //.UseSqlite("Data Source=AutoTeileShopTest.db")
                  .UseSqlite(@"Data Source= D:/4 Klasse/Pos1 Repo/sj2223-4bhif-pos-rest-api-project-autoteileshop/Spg.AutoTeileShop/src/AutoTeileShop.db")      //Laptop
                  //.UseSqlite(@"Data Source = I:\Dokumente 4TB\HTL\4 Klasse\POS1 Git Repo\sj2223-4bhif-pos-rest-api-project-autoteileshop\Spg.AutoTeileShop\src\Spg.AutoTeileShop.API\db\AutoTeileShop.db")     //Home PC       
                .Options;

            AutoTeileShopContext db = new AutoTeileShopContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            //db.Seed();
            return db;
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


            //Act
            Car car = new()
            {
                Baujahr = DateTime.Now,
                Marke = "BMW",
                Modell = "M3"
            };

            CreateCarCommand commandCreate = new CreateCarCommand(car);
            await mediator.ExecuteAsync<CreateCarCommand, Car>(commandCreate);

            GetCarByIdQuery queryReadById = new GetCarByIdQuery(1);
            Car result = await mediator.QueryAsync<GetCarByIdQuery, Car>(queryReadById);
            //commandReadById = new GetCarByIdQuery(0);
            //result = await mediator.QueryAsync<GetCarByIdQuery, Car>(commandReadById);
            Assert.NotNull(result);
            Assert.Equal(car, result);
            Assert.Single(db.Cars.ToList());
    
            //Assert

            //Assert.NotNull(result);
            //Assert.Equal(car, result);
            //Assert.Single(db.Cars.ToList());
        }
    }
}
