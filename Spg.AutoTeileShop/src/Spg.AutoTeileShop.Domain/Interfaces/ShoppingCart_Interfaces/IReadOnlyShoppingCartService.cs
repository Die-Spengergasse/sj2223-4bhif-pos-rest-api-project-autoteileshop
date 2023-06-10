using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.Domain.Interfaces.ShoppingCart_Interfaces
{
    public interface IReadOnlyShoppingCartService
    {
        ShoppingCart GetById(int Id);
        ShoppingCart GetByGuid(Guid guid);
        ShoppingCart? GetByUserNav(Guid userGuid);
        IEnumerable<ShoppingCart> GetAll();
    }
}
