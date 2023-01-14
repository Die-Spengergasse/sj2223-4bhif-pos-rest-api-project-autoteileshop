using Spg.AutoTeileShop.Domain.Interfaces.ShoppingCartItem_Interface;
using Spg.AutoTeileShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Application.Services
{
    public class ShoppingCartItemService : IDeleteAbleShoppingCartItemService, IAddUpdateableShoppingCartItemService, IReadOnlyShoppingCartService
    {
        private readonly IShoppingCartItemRepository _shoppingCartItemRepository;

        public ShoppingCartItemService(IShoppingCartItemRepository shoppingCartItemRepository)
        {
            _shoppingCartItemRepository = shoppingCartItemRepository;
        }

        public ShoppingCartItem Add(ShoppingCartItem shoppingCartItem)
        {
            return _shoppingCartItemRepository.Add(shoppingCartItem); 
        }

        public ShoppingCartItem Delete(ShoppingCartItem shoppingCartItem)
        {
            return _shoppingCartItemRepository.Delete(shoppingCartItem);
        }

        public ShoppingCartItem GetByGuid(Guid guid)
        {
            return _shoppingCartItemRepository.GetByGuid(guid);
        }

        public ShoppingCartItem GetById(int Id)
        {
            return _shoppingCartItemRepository.GetById(Id);
        }

        public ShoppingCartItem Update(ShoppingCartItem shoppingCartItem)
        {
            var sCI = _shoppingCartItemRepository.GetById(shoppingCartItem.Id);
            sCI.guid = shoppingCartItem.guid;
            sCI.Pieces = shoppingCartItem.Pieces;
            sCI.ShoppingCartId = shoppingCartItem.ShoppingCartId;
            sCI.ShoppingCartNav = shoppingCartItem.ShoppingCartNav;
            sCI.ProductNav = shoppingCartItem.ProductNav;
            sCI.ProductId = sCI.ProductId;

            return _shoppingCartItemRepository.Update(sCI);

        }
    }
}
