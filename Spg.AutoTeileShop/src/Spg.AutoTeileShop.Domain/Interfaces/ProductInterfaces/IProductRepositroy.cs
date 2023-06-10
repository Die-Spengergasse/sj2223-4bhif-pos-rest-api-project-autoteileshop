using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.Domain.Interfaces.ProductServiceInterfaces
{
    public interface IProductRepositroy
    {
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetByCatagory(Catagory catagory);
        Product? GetById(int Id);
        Product? GetByName(string name);
        Product? Delete(Product product);
        Product? Add(Product product);
        Product? Update(Product product);
    }
}
