using Spg.AutoTeileShop.Application.Services.CQS.Car.Commands;
using Spg.AutoTeileShop.Application.Services.CQS.Car.Queries;
using Spg.AutoTeileShop.Domain.Interfaces.Generic_Repository_Interfaces;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Domain;
using Spg.AutoTeileShop.Repository2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spg.AutoTeileShop.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Spg.AutoTeileShop.Domain.Interfaces;
using Spg.AutoTeileShop.Application.Services.CQS.Car;

namespace Spg.AutoTeileShop.ApplicationTest.Helpers
{
    public class TestServiceProvider : IServiceProvider
    {
        public object GetService(Type serviceType)
        {
            AutoTeileShopContext db = createDB();
            ReadOnlyRepositoryBase<Car> readOnlyRepo = new ReadOnlyRepositoryBase<Car>(db);
            RepositoryBase<Car> repo = new RepositoryBase<Car>(db);

            // Hier können Sie die Instanzen Ihrer Abhängigkeiten erstellen und zurückgeben
            if (serviceType == typeof(ICommandHandler<CreateCarCommand, Car>))
            {
                return new CreateCarCommandHandler(repo);
            }
            else if (serviceType == typeof(IQueryHandler<GetCarByIdQuery, Car>))
            {
                return new GetCarByIdQueryHandler(readOnlyRepo);
            }
            else if (serviceType == typeof(IReadOnlyRepositoryBase<Car>))
            {
                return new ReadOnlyRepositoryBase<Car>(db);
            }
            else if (serviceType == typeof(IRepositoryBase<Car>))
            {
                return new RepositoryBase<Car>(db);
            }
            else if (serviceType == typeof(IMediator))
            {
                // Wenn der Mediator selbst erstellt wird, übergeben Sie einfach eine Instanz von sich selbst
                return new Mediator(this);
            }

            return null; // Oder eine entsprechende Ausnahme werfen, wenn der Dienst nicht gefunden wurde
        }

        private AutoTeileShopContext createDB()
        {
            DbContextOptions options = new DbContextOptionsBuilder()
                  //.UseSqlite("Data Source=AutoTeileShopTest.db")
                  //.UseSqlite(@"Data Source= D:/4 Klasse/Pos1 Repo/sj2223-4bhif-pos-rest-api-project-autoteileshop/Spg.AutoTeileShop/src/AutoTeileShop.db")      //Laptop
                  .UseSqlite(@"Data Source = I:\Dokumente 4TB\HTL\4 Klasse\POS1 Git Repo\sj2223-4bhif-pos-rest-api-project-autoteileshop\Spg.AutoTeileShop\src\Spg.AutoTeileShop.API\db\AutoTeileShop.db")     //Home PC       
                .Options;

            AutoTeileShopContext db = new AutoTeileShopContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            //db.Seed();
            return db;
        }
    }

}
