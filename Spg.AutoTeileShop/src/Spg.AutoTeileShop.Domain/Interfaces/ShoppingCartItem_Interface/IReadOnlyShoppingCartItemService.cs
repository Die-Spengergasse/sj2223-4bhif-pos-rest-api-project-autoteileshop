using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.Domain.Interfaces.ShoppingCartItem_Interface
{
    public interface IReadOnlyShoppingCartItemService
    {
        List<ShoppingCartItem> GetAll();
        ShoppingCartItem GetByGuid(Guid guid);
        ShoppingCartItem GetById(int Id);
        List<ShoppingCartItem> GetByShoppingCart(ShoppingCart shoppingCart);
    }
}
