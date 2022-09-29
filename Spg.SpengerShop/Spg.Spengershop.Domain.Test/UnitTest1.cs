using Microsoft.EntityFrameworkCore;
using Spg.Spengershop.Domain.Model;
using Spg.Spengershop.Infrastructure;
using Xunit;
namespace Spg.Spengershop.Domain.Test
{
    public class UnitTest1
    {
        public SpengerShopContext db;

        private void createDB()
        {
            DbContextOptions options = new DbContextOptionsBuilder()
                .UseSqlite("Data Source=SpengerShop.db")
                .Options;

            db = new SpengerShopContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }
        
        [Fact]
        public void Create_Product_Test()
        {
            createDB();
            //DB
           

            Product newP = new Product()
            {
                guid = Guid.NewGuid(),
                Price = 153.5M,
                Menge = 5
            };

            db.Products.Add(newP);
            db.SaveChanges();

            Assert.Equal(1, db.Products.Count());
        }

        [Fact]
        public void Create_Customer_Test()
        {
            createDB();

            //DB
            
            

            Customer newP = new Customer()
            {
                guid = Guid.NewGuid(),
                Address = "test",
                Tel = "006523",
                Email = "dastsf",
                Firstname = "test",
                Lastname = "test",
                            };

            db.Customers.Add(newP);
            db.SaveChanges();

            Assert.Equal(1, db.Customers.Count());
        }

        [Fact]
        public void DomainModel_Add_Customer_To_ShoppingCart_Success_Test()
        {
            createDB();


            Customer newCustomer = new Customer()
            {
                guid = Guid.NewGuid(),
                Address = "test2",
                Tel = "0065232",
                Email = "dast2sf",
                Firstname = "tes2t",
                Lastname = "test2",
            };

            db.Customers.Add(newCustomer);
            //db.SaveChanges();

            ShoppingCart NewShoppingChart = new ShoppingCart()
            {
                guid = Guid.NewGuid(),
                CustomerNav = newCustomer,
                
                
            };

            db.ShoppingCarts.Add(NewShoppingChart);
            db.SaveChanges();

            Assert.Equal(1, db.ShoppingCarts.Count());
            Assert.Equal(1, db.Customers.Count());

        }

        //[Fact]
        //public void DomainModel_Add_ShoppingCart_To_Customers_Success_Test()
        //{
        //    createDB();


        //    Customer newCustomer = new Customer()
        //    {
        //        guid = Guid.NewGuid(),
        //        Address = "test2",
        //        Tel = "0065232",
        //        Email = "dast2sf",
        //        Firstname = "tes2t",
        //        Lastname = "test2",
        //    };

        //    //db.Customers.Add(newCustomer);
        //    //db.SaveChanges();

        //    ShoppingCart NewShoppingChart = new ShoppingCart()
        //    {
        //        guid = Guid.NewGuid(),
        //        //CustomerNav = newCustomer,


        //    };
        //    //NewShoppingChart.Add();

        //    db.ShoppingCarts.Add(NewShoppingChart);
        //    db.SaveChanges();

        //    //Assert.Equal(1, db.ShoppingCarts.Count());
        //    //Assert.Equal(1, db.Customers.Count());

        //}



    }
}
