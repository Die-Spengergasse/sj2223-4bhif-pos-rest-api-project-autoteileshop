using Bogus.DataSets;
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

        public Product? Add(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
            return product;
        }

        public Product? Delete(Product product)
        {
            _db.Products.Remove(product);
            _db.SaveChanges();
            return product;
        }

        public IEnumerable<Product> GetAll()
        {
            return _db.Products.ToList();
        }

        public IEnumerable<Product> GetByCatagory(Catagory catagory)
        {
            return _db.Products.Where(p => p.catagory == catagory).ToList();
        }

        public Product? GetById(int Id)
        {
            return _db.Products.Find(Id) ?? throw new KeyNotFoundException($"Product: {Id} not found"); ;
        }

        public Product? GetByName(string name)
        { 
            return _db.Products.SingleOrDefault(p => p.Name == name)
                ?? throw new KeyNotFoundException($"Product {name} not found");
        }

        public Product? Update(Product product)
        {
            _db.Products.Update(product);
            _db.SaveChanges();
            return product;
        }
    }
}