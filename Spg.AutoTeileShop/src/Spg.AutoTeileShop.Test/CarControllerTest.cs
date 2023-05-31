using Microsoft.EntityFrameworkCore;
using Spg.AutoTeileShop.API.Controllers.V1;
using Spg.AutoTeileShop.Application.Services;
using Spg.AutoTeileShop.Domain.Interfaces.Car_Interfaces;
using Spg.AutoTeileShop.Infrastructure;
using Spg.AutoTeileShop.Repository2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Test
{
    public class CarControllerTest
    {
        private CarRepository _carRepo;
        private IReadOnlyCarService _readOnlycarService;
        private IDeletableCarService _deletableCarService;
        private IAddUpdateableCarService _addUpdateableCarService;
        private CarController _carController;

        private AutoTeileShopContext createDB()
        {
            DbContextOptions options = new DbContextOptionsBuilder()
                  .UseSqlite(ReadLineWithQuestionMark())
                  //.UseSqlite(@"Data Source= D:/4 Klasse/Pos1 Repo/sj2223-4bhif-pos-rest-api-project-autoteileshop/Spg.AutoTeileShop/src/AutoTeileShop.db")      //Laptop
                  .UseSqlite(@"Data Source = I:\Dokumente 4TB\HTL\4 Klasse\POS1 Git Repo\sj2223-4bhif-pos-rest-api-project-autoteileshop\Spg.AutoTeileShop\src\Spg.AutoTeileShop.API\db\AutoTeileShop.db")     //Home PC       
                .Options;

            AutoTeileShopContext db = new AutoTeileShopContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            db.Seed();
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

        private CarController getController(AutoTeileShopContext db)
        {
            _carRepo = new(db);
            _readOnlycarService = new CarService(_carRepo);
            _deletableCarService = new CarService(_carRepo);
            _addUpdateableCarService = new CarService(_carRepo);
            return _carController = new(_readOnlycarService, _deletableCarService, _addUpdateableCarService);
        }

        //[Fact]
        public void Controller_GetAll_Test()
        {
            AutoTeileShopContext db = createDB();
            CarController carController = getController(db);

            var result = carController.GetAllCars();

            Assert.NotNull(result);
            Assert.Equal(50, result.Value.Count());
        }
    }

    
}
