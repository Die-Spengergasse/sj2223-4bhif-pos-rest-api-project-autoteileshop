using Microsoft.EntityFrameworkCore;
using Spg.AutoTeileShop.Domain.Interfaces.ShoppingCartItem_Interface;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;

namespace Spg.AutoTeileShop.Repository2.Repositories
{
    public class ShoppingCartItemRepository : IShoppingCartItemRepository
    {
        private readonly AutoTeileShopContext _db;

        public ShoppingCartItemRepository(AutoTeileShopContext db)
        {
            _db = db;
        }

        public ShoppingCartItem Add(ShoppingCartItem shoppingCartItem)
        {
            _db.ShoppingCartItems.Add(shoppingCartItem);
            _db.SaveChanges();
            return shoppingCartItem;
        }

        public ShoppingCartItem Delete(ShoppingCartItem shoppingCartItem)
        {
            _db.ShoppingCartItems.Remove(shoppingCartItem);
            _db.SaveChanges();
            return shoppingCartItem;
        }

        public List<ShoppingCartItem> GetAll()
        {
            return _db.ShoppingCartItems.ToList();
        }

        public ShoppingCartItem GetByGuid(Guid guid)
        {
            return (ShoppingCartItem)(_db.ShoppingCartItems.Where(s => s.guid == guid) ?? throw new KeyNotFoundException($"No Item found with Guid {guid}"));
        }

        public ShoppingCartItem GetById(int Id)
        {
            return (ShoppingCartItem)(_db.ShoppingCartItems.Where(s => s.Id == Id) ?? throw new KeyNotFoundException($"No Item found with Guid {Id}"));
        }

        public ShoppingCartItem Update(ShoppingCartItem shoppingCartItem)
        {
            _db.ShoppingCartItems.Update(shoppingCartItem);
            _db.SaveChanges();
            return shoppingCartItem;
        }

        public List<ShoppingCartItem> GetAllIncludeShoppingCartNav()
        {
            return _db.ShoppingCartItems.ToList();
        }
    }
}
