﻿using Microsoft.EntityFrameworkCore;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;
using Spg.AutoTeileShop.Repository2.Repositories;

namespace Spg.AutoTeileShop.RepositoryTest
{
    [Collection("Sequential tests")]
    public class Repository_UnitTest
    {
        private AutoTeileShopContext createDB()
        {
            DbContextOptions options = new DbContextOptionsBuilder()
                  //.UseSqlite(ReadLineWithQuestionMark())
                  .UseSqlite(@"Data Source= D:/4 Klasse/Pos1 Repo/sj2223-4bhif-pos-rest-api-project-autoteileshop/Spg.AutoTeileShop/src/AutoTeileShop.db")
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
        private CarRepository createCartRepository(AutoTeileShopContext db)
        {
            return new CarRepository(db);
        }

        [Fact]
        public void Repository_Car_Add_Test()
        {
            var _db = createDB();
            var cRepo = createCartRepository(_db);
            var car = new Car()
            {
                Marke = "VW",
                Modell = "Golf",
                Baujahr = DateTime.Now,
            };

            cRepo.Add(car);

            Assert.Equal(1, _db.Cars.Count());
            Assert.Equal(car, _db.Cars.First());
        }

        [Fact]
        public void Repository_Car_Update_Test()
        {
            var _db = createDB();
            var cRepo = createCartRepository(_db);
            var car = new Car()
            {
                Marke = "VW",
                Modell = "Golf",
                Baujahr = DateTime.Now,
            };

            cRepo.Add(car);

            car.Marke = "Audi";
            car.Modell = "A3";
            cRepo.Update(car);

            Assert.Equal(1, _db.Cars.Count());
            Assert.Equal(car, _db.Cars.First());
        }

        //[Fact]
        //public void Repository_Car_Update2_with_Tracking_Off_Test() //should not work
        //{
        //    var _db = createDB();
        //    _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        //    _db.ChangeTracker.AutoDetectChangesEnabled = false;
        //    _db.ChangeTracker.LazyLoadingEnabled = false;
        //    var cRepo = createCartRepository(_db);
        //    var car = new Car()
        //    {
        //        Marke = "VW",
        //        Modell = "Golf",
        //        Baujahr = new DateTime(2021, 1, 1),
        //    };

        //    //cRepo.Add(car);

        //    //var carChanges = new Car(car.Id, "Audi", "A3", new DateTime(2021, 1, 1), new List<Product>());
        //    var car2 = cRepo.Update2(car);

        //    Assert.Equal(1, _db.Cars.Count());
        //    Assert.Equal(car2.ToString(), _db.Cars.First().ToString());
        //}

        [Fact]
        public void Repository_Car_Update3_useUpdate2_with_Tracking_ON_Test()
        {
            var _db = createDB();
            var cRepo = createCartRepository(_db);
            var car = new Car()
            {
                Marke = "VW",
                Modell = "Golf",
                //Baujahr = DateTime.Now,
            };

            cRepo.Add(car);

            car.Marke = "Audi";
            car.Modell = "A3";
            cRepo.Update2(car);

            Assert.Equal(1, _db.Cars.Count());
            Assert.Equal(car, _db.Cars.First());
        }

        [Fact]
        public void Repository_Car_Update4_useUpdate3_with_Tracking_ON_Test()
        {
            var _db = createDB();
            var cRepo = createCartRepository(_db);
            var car = new Car()
            {
                Marke = "VW",
                Modell = "Golf",
                //Baujahr = DateTime.Now,
            };

            cRepo.Add(car);

            car.Marke = "Audi";
            car.Modell = "A3";
            cRepo.Update3(car);

            Assert.Equal(1, _db.Cars.Count());
            Assert.Equal(car, _db.Cars.First());
        }

    }
}
