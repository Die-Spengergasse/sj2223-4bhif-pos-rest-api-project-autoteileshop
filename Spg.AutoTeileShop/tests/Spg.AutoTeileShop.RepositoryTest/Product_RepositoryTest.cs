using Microsoft.EntityFrameworkCore;
using Spg.AutoTeileShop.Domain.Interfaces.ProductServiceInterfaces;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;
using Spg.AutoTeileShop.Repository2.Repositories;

namespace Spg.AutoTeileShop.RepositoryTest
{
    public class Product_RepositoryTest
    {
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
            db.Seed();
            return db;
        }
        [Fact]
        public void Create_SuccesTest()
        {
            AutoTeileShopContext db = createDB();
            ProductRepository repo = new(db);
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

            repo.Add(product);

            //Assert

            Assert.Single(db.Products.ToList());
        }
    }
}