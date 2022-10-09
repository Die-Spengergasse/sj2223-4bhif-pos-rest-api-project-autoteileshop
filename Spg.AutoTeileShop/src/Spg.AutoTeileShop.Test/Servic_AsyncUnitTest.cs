using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;
using Spg.AutoTeileShop.Repository.Repos;
using Xunit;
namespace Spg.AutoTeileShop.Domain.Test
{
    public class Servic_AsyncUnitTest
    {
        private AutoTeileShopContext createDB()
        {
            DbContextOptions options = new DbContextOptionsBuilder()
                .UseSqlite("Data Source=AutoTeileShop.db")
                .Options;

            AutoTeileShopContext db = new AutoTeileShopContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            return db;
        }
        
        [Fact]
        public async Task DomainModel_Servis_Add_Customer_TestAsync()
        {
            AutoTeileShopContext db = createDB();

            Customer customer = new Customer()
            {
                Guid = Guid.NewGuid(),
                Vorname = "Max",
                Nachname = "Musterman",
                Email = "Max.Musterman@gmx.at",
                Strasse = "TestStaße ",
                Telefon = "0004514554"
            };

            Repository<Customer> customerRepo = new Repository<Customer>(db);
            await customerRepo.AddAsync(customer);

            Assert.Equal(customer, await customerRepo.GetByIdAsync(customer.Id));
        }

        [Fact]
        public async Task DomainModel_Servis_Find_Customer_TestAsync()
        {
            AutoTeileShopContext db = createDB();

            Customer customer = new Customer()
            {
                Guid = Guid.NewGuid(),
                Vorname = "Max",
                Nachname = "Musterman",
                Email = "Max.Musterman@gmx.at",
                Strasse = "TestStaße ",
                Telefon = "0004514554"
            };
            Repository<Customer> customerRepo = new Repository<Customer>(db);
            await customerRepo.AddAsync(customer);

            Assert.Equal(await customerRepo.GetByIdAsync(customer.Id), customer);
        }
    }
}
