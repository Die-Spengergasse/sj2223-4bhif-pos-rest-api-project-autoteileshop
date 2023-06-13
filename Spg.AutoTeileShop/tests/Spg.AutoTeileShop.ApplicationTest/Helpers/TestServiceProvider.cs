using Microsoft.EntityFrameworkCore;
using Spg.AutoTeileShop.Application.Services.CQS;
using Spg.AutoTeileShop.Application.Services.CQS.Car.Commands;
using Spg.AutoTeileShop.Application.Services.CQS.Car.Queries;
using Spg.AutoTeileShop.Domain;
using Spg.AutoTeileShop.Domain.Interfaces;
using Spg.AutoTeileShop.Domain.Interfaces.Generic_Repository_Interfaces;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;
using Spg.AutoTeileShop.Repository2;
using Spg.AutoTeileShop.Repository2.CustomGenericRepositories;

namespace Spg.AutoTeileShop.ApplicationTest.Helpers
{
    public class TestServiceProvider : IServiceProvider
    {
        public AutoTeileShopContext db;
        public TestServiceProvider(AutoTeileShopContext _db)
        {
            db = _db;
        }

        public object GetService(Type serviceType)
        {
            //AutoTeileShopContext db = createDB();
            ReadOnlyRepositoryBase<Car> readOnlyRepo = new ReadOnlyRepositoryBase<Car>(db);
            RepositoryBase<Car> repo = new RepositoryBase<Car>(db);
            CarRepositoryCustom carRepo = new CarRepositoryCustom(db);

            // Hier können Sie die Instanzen Ihrer Abhängigkeiten erstellen und zurückgeben
            if (serviceType == typeof(ICommandHandler<CreateCarCommand, Car>))
            {
                return new CreateCarCommandHandler(repo);
            }
            else if (serviceType == typeof(IQueryHandler<GetCarByIdQuery, Car>))
            {
                return new GetCarByIdQueryHandler(readOnlyRepo);
            }
            //GetAllCarsQueryHandler
            else if (serviceType == typeof(ICommandHandler<DeleteCarCommand, int>))
            {
                return new DeleteCarCommandHandler(repo);
            }
            else if (serviceType == typeof(ICommandHandler<UpdateCarCommand, Car>))
            {
                return new UpdateCarCommandHandler(repo);
            }
            else if (serviceType == typeof(IQueryHandler<GetAllCarsQuery, IQueryable<Spg.AutoTeileShop.Domain.Models.Car>>))
            {
                return new GetAllCarsQueryHandler(readOnlyRepo);
            }
            else if (serviceType == typeof(IQueryHandler<GetCarsByBaujahrQuery, IEnumerable<Car>>))
            {
                return new GetCarsByBaujahrQueryHandler(carRepo);
            }
            else if (serviceType == typeof(IQueryHandler<GetCarsByMarkeQuery, IEnumerable<Car>>))
            {
                return new GetCarsByMarkeQueryHandler(carRepo);
            }
            else if (serviceType == typeof(IQueryHandler<GetCarsByModellQuery, IEnumerable<Car>>))
            {
                return new GetCarsByModellQueryHandler(carRepo);
            }
            else if (serviceType == typeof(IQueryHandler<GetCarsByMarkeAndModellQuery, IEnumerable<Car>>))
            {
                return new GetCarsByMarkeAndModellQueryHandler(carRepo);
            }
            else if (serviceType == typeof(IQueryHandler<GetCarsByMarkeAndModellAndBaujahrQuery, IEnumerable<Car>>))
            {
                return new GetCarsByMarkeAndModellAndBaujahrQueryHandler(carRepo);
            }
            else if (serviceType == typeof(IQueryHandler<GetCarsByFitProductQuery, IEnumerable<Car>>))
            {
                return new GetCarsByFitProductQueryHandler(carRepo);
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
                  .UseLazyLoadingProxies()
                  .UseSqlite("DataSource=D:/4 Klasse/Pos1 Repo/sj2223-4bhif-pos-rest-api-project-autoteileshop/Spg.AutoTeileShop/src/AutoTeileShop.db")

                //.UseSqlite(@"Data Source= D:/4 Klasse/Pos1 Repo/sj2223-4bhif-pos-rest-api-project-autoteileshop/Spg.AutoTeileShop/src/AutoTeileShop.db")      //Laptop
                //.UseSqlite(ReadLineWithQuestionMark())     //Home PC       
                .Options;

            AutoTeileShopContext db = new AutoTeileShopContext(options);
            //db.Database.EnsureDeleted();
            //db.Database.EnsureCreated();
            //db.Seed();
            return db;
        }

        private AutoTeileShopContext createDB_Del_Create_DB()
        {
            DbContextOptions options = new DbContextOptionsBuilder()
                  //.UseSqlite("Data Source=AutoTeileShopTest.db")
                  //.UseSqlite(@"Data Source= D:/4 Klasse/Pos1 Repo/sj2223-4bhif-pos-rest-api-project-autoteileshop/Spg.AutoTeileShop/src/AutoTeileShop.db")      //Laptop
                  .UseSqlite(ReadLineWithQuestionMark())     //Home PC       
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
    }

}
