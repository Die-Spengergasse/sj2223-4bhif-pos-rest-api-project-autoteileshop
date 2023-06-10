using Microsoft.EntityFrameworkCore;
using Spg.AutoTeileShop.Domain.Interfaces.ShoppingCart_Interfaces;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;

namespace Spg.AutoTeileShop.Repository2.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly AutoTeileShopContext _db;

        public ShoppingCartRepository(AutoTeileShopContext db)
        {
            _db = db;
        }

        public ShoppingCart AddShoppingCart(ShoppingCart item)
        {
            _db.ShoppingCarts.Add(item);
            return item;
        }

        public IEnumerable<ShoppingCart> GetAll_includeItems()
        {
            return _db.ShoppingCarts.Include(s => s.ShoppingCartItems).ToList();
        }

        public ShoppingCart GetById(int Id)
        {
            return _db.ShoppingCarts.Find(Id) ?? throw new KeyNotFoundException("ShoppingCart with Id " + Id + " not found");
        }

        public ShoppingCart GetByGuid(Guid guid)
        {
            return _db.ShoppingCarts.Include(s => s.UserNav).Where(s => s.guid == guid).SingleOrDefault() ?? throw new KeyNotFoundException("ShoppingCart with guid " + guid + " not found");
        }

        public ShoppingCart? GetByUserNav(Guid userGuid)
        {
            return _db.ShoppingCarts.Where(c => c.UserNav.Guid == userGuid).SingleOrDefault() ?? throw new Exception("No Cart found with UserNav: " + userGuid);
        }

        public ShoppingCart Remove(ShoppingCart shoppingCart)
        {
            _db.ShoppingCarts.Remove(shoppingCart);
            return shoppingCart;
        }

        public ShoppingCart UpdateShoppingCart(ShoppingCart item)
        {
            _db.ShoppingCarts.Update(item);
            return item;
        }

    }
}
