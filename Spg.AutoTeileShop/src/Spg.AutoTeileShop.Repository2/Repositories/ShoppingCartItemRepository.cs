using Spg.AutoTeileShop.Domain.Interfaces.ShoppingCartItem_Interface;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public ShoppingCartItem GetByGuid(Guid guid)
        {
            return _db.ShoppingCartItems.Where(s => s.guid == guid).SingleOrDefault();
        }

        public ShoppingCartItem GetById(int Id)
        {
            return _db.ShoppingCartItems.Find(Id);
        }

        public ShoppingCartItem Update(ShoppingCartItem shoppingCartItem)
        {
            _db.ShoppingCartItems.Update(shoppingCartItem);
            _db.SaveChanges();
            return shoppingCartItem;
        }
    }
}
