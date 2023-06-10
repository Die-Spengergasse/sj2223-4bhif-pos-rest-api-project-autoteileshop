using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.Domain.Interfaces.ProductServiceInterfaces
{
    public interface IAddUpdateableProductService
    {
        IEnumerable<Product> GetAll();
        Product? Add(Product product);
        Product? Update(Product product);

    }
}
