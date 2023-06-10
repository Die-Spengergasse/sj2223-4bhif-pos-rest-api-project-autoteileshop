using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;
//using Spg.AutoTeileShop.Repository;
using Spg.AutoTeileShop.Repository2;
using Spg.AutoTeileShop.RepositoryTest.Helpers;

namespace Spg.AutoTeileShop.RepositoryTest
{
    public class RepositorxBaseTest
    {
        //private AutoTeileShopContext GetDB()
        //{
        //    SqliteConnection connection = new SqliteConnection("DataSource=:memory:");
        //    connection.Open();

        //    return new DbContextOptionsBuild()
        //        .UseSqlite(connection)
        //        .Options;
        //}


        [Fact()]
        public void Product_Create_Success_Test()
        {
            using (AutoTeileShopContext db = new AutoTeileShopContext(DatabaseUtilities.GetDbOptions()))
            {

                // Arrange
                DatabaseUtilities.InitializeDatabase(db);


                Product newProduct = new Product(
                    Guid.NewGuid(),
                    "Test Product3",
                    19.99m,
                    null,
                    "This is a test product3.",
                    "test-image.jpg",
                    QualityType.SehrGut,
                    100,
                    10,
                    DateTime.Now.AddDays(-15),
                    DatabaseUtilities.GetSeedingCars_without_Product());

                // Act
                new RepositoryBase<Product>(db).Create(newProduct);

                // Assert
                Assert.Equal(3, db.Products.Count());
            }
        }
    }


}
