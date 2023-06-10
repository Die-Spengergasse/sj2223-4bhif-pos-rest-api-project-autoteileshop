using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.Domain.Interfaces.ProductServiceInterfaces
{
    public interface IReadOnlyProductService
    {
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetByCatagory(Catagory catagory);

        Product? GetByName(string name);
        Product? GetById(int id);
        int GetStockById(int id);
        int GetDiscountById(int id);
        DateTime GetReceiveById(int id);
        string GetImageById(int id);
        string GetEan13ById(int id);
        decimal GetPriceById(int id);

    }
}
