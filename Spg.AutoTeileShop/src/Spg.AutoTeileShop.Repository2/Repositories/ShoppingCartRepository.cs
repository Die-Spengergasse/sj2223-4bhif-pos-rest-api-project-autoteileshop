using Spg.AutoTeileShop.Domain.Interfaces.ShoppingCart_Interfaces;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public IEnumerable<ShoppingCartItem> GetAll()
        {
            return (IEnumerable<ShoppingCartItem>)_db.ShoppingCarts.ToList();
        }

        public ShoppingCart GetById(int Id)
        {
            return _db.ShoppingCarts.Find(Id);
        }

        public ShoppingCart? GetByUserNav(User user)
        {
            return _db.ShoppingCarts.Where(c => c.UserNav == user).SingleOrDefault();
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

        public IEnumerable<>
    }
}
