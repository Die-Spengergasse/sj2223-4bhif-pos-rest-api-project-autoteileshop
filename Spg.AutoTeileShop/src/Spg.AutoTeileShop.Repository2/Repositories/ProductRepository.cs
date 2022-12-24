using Spg.AutoTeileShop.Domain.Interfaces.ProductServiceInterfaces;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;

namespace Spg.AutoTeileShop.Repository2.Repositories
{
    public class ProductRepository : IProductRepositroy
    {
        private readonly AutoTeileShopContext _db;

        public ProductRepository(AutoTeileShopContext db)
        {
            _db = db;
        }

        public IEnumerable<Product> GetAll()
        {
            return _db.Products.ToList();
        }

        public Product? GetByName(string name)
        { 
            return _db.Products.SingleOrDefault(p => p.Name == name)
                ?? throw new KeyNotFoundException($"Product {name} not found");
        }

    }
}