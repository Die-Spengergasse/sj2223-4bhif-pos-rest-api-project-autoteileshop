using Microsoft.EntityFrameworkCore;
using Spg.AutoTeileShop.Application.Services;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;
using Spg.AutoTeileShop.Repository2.Repositories;

namespace Spg.AutoTeileShop.ApplicationTest
{
    public class Product_ServiceTestMock
    {
        private AutoTeileShopContext createDB()
        {
            DbContextOptions options = new DbContextOptionsBuilder()
                  //.UseSqlite(ReadLineWithQuestionMark())
                  .UseSqlite("Data Source= D:/4 Klasse/Pos1 Repo/sj2223-4bhif-pos-rest-api-project-autoteileshop/Spg.AutoTeileShop/src/AutoTeileShop.db")      //Laptop
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
        public void Create_Succes_Test()
        {
            //Arrange
            //Datenbank
            AutoTeileShopContext db = createDB();

            ProductService productService = new(new ProductRepository(db));
            Product product = new Product()
            {
                Ean13 = "dagasgasf",
                Description = "Des Test",
                Guid = Guid.NewGuid(),
                Name = "Pro Test",
                Price = 499.99M,
                Stock = 1
            };

            //Act

            productService.Add(product);

            //Assert

            Assert.Single(db.Products.ToList());
        }
    }
}