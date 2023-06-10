using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.Domain.Interfaces.ShoppingCartItem_Interface
{
    public interface IShoppingCartItemRepository
    {
        List<ShoppingCartItem> GetAll();
        List<ShoppingCartItem> GetAllIncludeShoppingCartNav();
        ShoppingCartItem GetById(int Id);
        ShoppingCartItem GetByGuid(Guid guid);
        ShoppingCartItem Update(ShoppingCartItem shoppingCartItem);
        ShoppingCartItem Add(ShoppingCartItem shoppingCartItem);
        ShoppingCartItem Delete(ShoppingCartItem shoppingCartItem);

    }
}
