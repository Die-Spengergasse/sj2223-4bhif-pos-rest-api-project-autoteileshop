using Spg.AutoTeileShop.Domain.Interfaces.Car_Interfaces;
using Spg.AutoTeileShop.Domain.Interfaces.ShoppingCart_Interfaces;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Repository2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Application.Services
{
    public class ShoppingCartService : IAddUpdateableShoppingCartService, IDeletableShoppingCartService, IReadOnlyShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

        public ShoppingCart AddShoppingCart(ShoppingCart shoppingCart)
        {
            return _shoppingCartRepository.AddShoppingCart(shoppingCart);
        }

        public IEnumerable<ShoppingCart> GetAll()
        {
            return _shoppingCartRepository.GetAll_includeItems();
        }

        public ShoppingCart GetById(int Id)
        {
            return _shoppingCartRepository.GetById(Id);
        }

        public ShoppingCart GetByGuid(Guid guid)
        {
            return _shoppingCartRepository.GetByGuid(guid);
        }

        public ShoppingCart? GetByUserNav(Guid userGuid)
        {
            return _shoppingCartRepository.GetByUserNav(userGuid);
        }

        public ShoppingCart Remove(ShoppingCart shoppingCart)
        {
            return _shoppingCartRepository.Remove(shoppingCart);
        }

        public ShoppingCart UpdateShoppingCart(ShoppingCart item)
        {
            var shoppingCart2 = _shoppingCartRepository.GetById(item.Id);
            shoppingCart2.UserNav = item.UserNav;
            shoppingCart2.UserId = item.UserId;
            shoppingCart2.guid = item.guid;

            foreach (ShoppingCartItem item2 in item.ShoppingCartItems)
            {
                if (!shoppingCart2.ShoppingCartItems.Contains(item2))
                {
                    shoppingCart2.AddShoppingCartItem(item2);
                }
            }

            foreach (ShoppingCartItem item2 in shoppingCart2.ShoppingCartItems)
            {
                if (item.ShoppingCartItems.Contains(item2))
                {
                    shoppingCart2.RemoveShoppingCartItem(item2);
                }
            }

            return _shoppingCartRepository.UpdateShoppingCart(shoppingCart2);
        }
    }
}
