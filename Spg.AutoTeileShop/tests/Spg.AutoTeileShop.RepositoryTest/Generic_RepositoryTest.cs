using Microsoft.EntityFrameworkCore;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;
using Spg.AutoTeileShop.Repository2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.RepositoryTest
{
    public class Generic_RepositoryTest
    {
        private AutoTeileShopContext createDB()
        {
            DbContextOptions options = new DbContextOptionsBuilder()
                    .UseSqlite(ReadLineWithQuestionMark())
                  //.UseSqlite("Data Source=AutoTeileShopTest.db")
                  //.UseSqlite(@"Data Source= D:/4 Klasse/Pos1 Repo/sj2223-4bhif-pos-rest-api-project-autoteileshop/Spg.AutoTeileShop/src/AutoTeileShop.db")      //Laptop
                  .UseSqlite(@"Data Source = I:\Dokumente 4TB\HTL\4 Klasse\POS1 Git Repo\sj2223-4bhif-pos-rest-api-project-autoteileshop\Spg.AutoTeileShop\src\Spg.AutoTeileShop.API\db\AutoTeileShop.db")     //Home PC       
                .Options;

            AutoTeileShopContext db = new AutoTeileShopContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
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
        public void Create_Car_SuccesTest()
        {
            AutoTeileShopContext db = createDB();
            RepositoryBase<Car> repo = new(db);
            Car car = new Car()
            {
                Baujahr = DateTime.Now,
                Marke = "BMW",
                Modell = "M3",
            };

            //Act

            repo.Create(car);

            //Assert

            Assert.Single(db.Cars.ToList());
            Assert.Equal(car, db.Cars.First());
        }

        [Fact]
        public void Delete_Car_SuccesTest()
        {
            AutoTeileShopContext db = createDB();
            RepositoryBase<Car> repo = new(db);
            Car car = new Car()
            {
                Baujahr = DateTime.Now,
                Marke = "BMW",
                Modell = "M3",
            };

            //Act 1

            repo.Create(car);

            //Assert 1 

            Assert.Single(db.Cars.ToList());
            Assert.Equal(car, db.Cars.First());

            //Act 2

            repo.Delete(car.Id);

            //Assert 2

            Assert.Empty(db.Cars.ToList());
        }


        [Fact]
        public void Update_Car_SuccesTest() 
        {
            AutoTeileShopContext db = createDB();
            RepositoryBase<Car> repo = new(db);
            Car car = new Car()
            {
                Baujahr = DateTime.Now,
                Marke = "BMW",
                Modell = "M3",
            };

            //Act 1

            repo.Create(car);

            //Assert 1 

            Assert.Single(db.Cars.ToList());
            Assert.Equal(car, db.Cars.First());

            //Act 2
            car.Marke = "Audi";
            repo.Update(car.Id,car);

            //Assert 2

            Assert.Single(db.Cars.ToList());
            Assert.Equal(car, db.Cars.First());
        }

        [Fact]
        public void ReadActions_GetAll_Car_Test()
        {
            AutoTeileShopContext db = createDB();
            RepositoryBase<Car> repo = new(db);
            ReadOnlyRepositoryBase<Car> repoRead = new(db);
            Car carBMW = new Car()
            {
                Baujahr = DateTime.Now,
                Marke = "BMW",
                Modell = "M3",
            };
            Car carAudi = new Car()
            {
                Baujahr = DateTime.Now,
                Marke = "Audi",
                Modell = "A3",
            };

            //Act 1
            repo.Create(carAudi);
            repo.Create(carBMW);
            
            var cars = repoRead.GetAll();

            //Assert 1 

            Assert.Equal(2, cars.Result.Count());
            Assert.Equal(carAudi, cars.Result.First());
            Assert.Equal(carBMW, cars.Result.ToList().ElementAt(1));
        }

        [Fact]
        public void ReadActions_GetById_Car_Test()
        {
            AutoTeileShopContext db = createDB();
            RepositoryBase<Car> repo = new(db);
            ReadOnlyRepositoryBase<Car> repoRead = new(db);
            Car carBMW = new Car()
            {
                Baujahr = DateTime.Now,
                Marke = "BMW",
                Modell = "M3",
            };
            Car carAudi = new Car()
            {
                Baujahr = DateTime.Now,
                Marke = "Audi",
                Modell = "A3",
            };

            //Act 1
            repo.Create(carAudi);
            repo.Create(carBMW);

            var car1 = repoRead.GetById(carAudi.Id);

            //Assert 1 

            Assert.Equal(carAudi, car1);
        }


        // ChatGPT
        [Fact]
        public async Task GetQueryable_Car_ReturnsExpectedResult()
        {
            // Arrange
            AutoTeileShopContext db = createDB();
            ReadOnlyRepositoryBase<Car> repo = new ReadOnlyRepositoryBase<Car>(db);
            Car car1 = new Car()
            {
                Baujahr = DateTime.Now,
                Marke = "BMW",
                Modell = "M3",
            };
            Car car2 = new Car()
            {
                Baujahr = DateTime.Now,
                Marke = "Audi",
                Modell = "A3",
            };
            db.Cars.Add(car1);
            db.Cars.Add(car2);
            db.SaveChanges();

            // Act
            IQueryable<Car> queryable = await repo.GetQueryable(
                filter: null,
                sortOrder: null,
                includeNavigationProperty: "",
                skip: null,
                take: null
            );

            // Assert
            List<Car> resultList = queryable.ToList();
            Assert.Equal(2, resultList.Count);
            Assert.Contains(car1, resultList);
            Assert.Contains(car2, resultList);
        }

        [Fact]
        public void GetById_Car_ReturnsExpectedResult()
        {
            // Arrange
            AutoTeileShopContext db = createDB();
            ReadOnlyRepositoryBase<Car> repo = new ReadOnlyRepositoryBase<Car>(db);
            Car car1 = new Car()
            {
                Baujahr = DateTime.Now,
                Marke = "BMW",
                Modell = "M3",
            };
            Car car2 = new Car()
            {
                Baujahr = DateTime.Now,
                Marke = "Audi",
                Modell = "A3",
            };
            db.Cars.Add(car1);
            db.Cars.Add(car2);
            db.SaveChanges();

            // Act
            Car result1 = repo.GetById(1);
            Car result2 = repo.GetById(2);
            Car result3 = repo.GetById(3);

            // Assert
            Assert.Equal(car1, result1);
            Assert.Equal(car2, result2);
            Assert.Null(result3);
        }

        //[Fact]
        //public void GetSingleOrDefaultByGuid_Car_ReturnsExpectedResult()
        //{
        //    // Arrange
        //    AutoTeileShopContext db = createDB();
        //    ReadOnlyRepositoryBase<Car> repo = new ReadOnlyRepositoryBase<Car>(db);
        //    Car car1 = new Car()
        //    {
        //        Baujahr = DateTime.Now,
        //        Marke = "BMW",
        //        Modell = "M3",
        //    };
        //    Car car2 = new Car()
        //    {
        //        Baujahr = DateTime.Now,
        //        Marke = "Audi",
        //        Modell = "A3",
        //    };
        //    db.Cars.Add(car1);
        //    db.Cars.Add(car2);
        //    db.SaveChanges();

        //    // Act
        //    Car result1 = repo.GetSingleOrDefaultByGuid<Car>(Guid.NewGuid());

        //    // Assert
        //    Assert.Null(result1);
        //}

        [Fact]
        public async Task GetAll_Car_ReturnsExpectedResult()
        {
            // Arrange
            AutoTeileShopContext db = createDB();
            ReadOnlyRepositoryBase<Car> repo = new ReadOnlyRepositoryBase<Car>(db);
            Car car1 = new Car()
            {
                Baujahr = DateTime.Now,
                Marke = "BMW",
                Modell = "M3",
            };
            Car car2 = new Car()
            {
                Baujahr = DateTime.Now,
                Marke = "Audi",
                Modell = "A3",
            };
            db.Cars.Add(car1);
            db.Cars.Add(car2);
            db.SaveChanges();

            // Act
            IQueryable<Car> queryable = await repo.GetAll(
                orderBy: null,
                includeNavigationProperty: "",
                skip: null,
                take: null
            );

            // Assert
            List<Car> resultList = queryable.ToList();
            Assert.Equal(2, resultList.Count);
            Assert.Contains(car1, resultList);
            Assert.Contains(car2, resultList);
        }

        [Fact]
        public async Task Get_Car_ReturnsExpectedResult()
        {
            // Arrange
            AutoTeileShopContext db = createDB();
            ReadOnlyRepositoryBase<Car> repo = new ReadOnlyRepositoryBase<Car>(db);
            Car car1 = new Car()
            {
                Baujahr = DateTime.Now,
                Marke = "BMW",
                Modell = "M3",
            };
            Car car2 = new Car()
            {
                Baujahr = DateTime.Now,
                Marke = "Audi",
                Modell = "A3",
            };
            db.Cars.Add(car1);
            db.Cars.Add(car2);
            db.SaveChanges();

            // Act
            IQueryable<Car> queryable = await repo.Get(
                filter: null,
                orderBy: null,
                includeNavigationProperty: "",
                skip: null,
                take: null
            );

            // Assert
            List<Car> resultList = queryable.ToList();
            Assert.Equal(2, resultList.Count);
            Assert.Contains(car1, resultList);
            Assert.Contains(car2, resultList);
        }

        [Fact]
        public async Task GetQueryable_User_ReturnsExpectedResult()
        {
            // Arrange
            AutoTeileShopContext db = createDB();
            ReadOnlyRepositoryBase<User> repo = new ReadOnlyRepositoryBase<User>(db);
            User user1 = new User(Guid.NewGuid(), "John", "Doe", "Address 1", "123456789", "john@example.com", "password", Roles.User, true);
            User user2 = new User(Guid.NewGuid(), "Jane", "Doe", "Address 2", "987654321", "jane@example.com", "password", Roles.Admin, true);
            db.Users.Add(user1);
            db.Users.Add(user2);
            db.SaveChanges();

            // Act
            IQueryable<User> queryable = await repo.GetQueryable(
                filter: null,
                sortOrder: null,
                includeNavigationProperty: "",
                skip: null,
                take: null
            );

            // Assert
            List<User> resultList = queryable.ToList();
            Assert.Equal(2, resultList.Count);
            Assert.Contains(user1, resultList);
            Assert.Contains(user2, resultList);
        }

        [Fact]
        public void GetById_User_ReturnsExpectedResult()
        {
            // Arrange
            AutoTeileShopContext db = createDB();
            ReadOnlyRepositoryBase<User> repo = new ReadOnlyRepositoryBase<User>(db);
            User user1 = new User(Guid.NewGuid(), "John", "Doe", "Address 1", "123456789", "john@example.com", "password", Roles.User, true);
            User user2 = new User(Guid.NewGuid(), "Jane", "Doe", "Address 2", "987654321", "jane@example.com", "password", Roles.Admin, true);
            db.Users.Add(user1);
            db.Users.Add(user2);
            db.SaveChanges();

            // Act
            User result1 = repo.GetById(user1.Id);
            User result2 = repo.GetById(user2.Id);
            User result3 = repo.GetById(3);

            // Assert
            Assert.Equal(user1, result1);
            Assert.Equal(user2, result2);
            Assert.Null(result3);
        }

        [Fact]
        public void GetSingleOrDefaultByGuid_User_ReturnsExpectedResult()
        {
            // Arrange
            AutoTeileShopContext db = createDB();
            ReadOnlyRepositoryBase<User> repo = new ReadOnlyRepositoryBase<User>(db);
            User user1 = new User(Guid.NewGuid(), "John", "Doe", "Address 1", "123456789", "john@example.com", "password", Roles.User, true);
            User user2 = new User(Guid.NewGuid(), "Jane", "Doe", "Address 2", "987654321", "jane@example.com", "password", Roles.Admin, true);
            db.Users.Add(user1);
            db.Users.Add(user2);
            db.SaveChanges();

            // Act
            User result1 = repo.GetSingleOrDefaultByGuid<User>(user1.Guid);
            User result2 = repo.GetSingleOrDefaultByGuid<User>(user2.Guid);
            User result3 = repo.GetSingleOrDefaultByGuid<User>(Guid.NewGuid());

            // Assert
            Assert.Equal(user1, result1);
            Assert.Equal(user2, result2);
            Assert.Null(result3);
        }

       
    }
}