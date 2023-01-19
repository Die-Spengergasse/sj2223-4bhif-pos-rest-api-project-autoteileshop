﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spg.AutoTeileShop.Domain.Interfaces.Car_Interfaces;
using Spg.AutoTeileShop.Domain.Interfaces.ShoppingCart_Interfaces;
using Spg.AutoTeileShop.Domain.Interfaces.ShoppingCartItem_Interface;
using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartItemController : ControllerBase
    {
        private readonly IDeleteAbleShoppingCartItemService _deleteAbleShoppingCartItemService;
        private readonly IAddUpdateableShoppingCartItemService _addUpdateableShoppingCartItemService;
        private readonly IReadOnlyShoppingCartItemService _readOnlyShoppingCartItemService;
        private readonly IReadOnlyShoppingCartService _readOnlyShoppingCartService;

        public ShoppingCartItemController(IDeleteAbleShoppingCartItemService deleteAbleShoppingCartItemService, IAddUpdateableShoppingCartItemService addUpdateableShoppingCartItemService, IReadOnlyShoppingCartItemService readOnlyShoppingCartItemService , IReadOnlyShoppingCartService readOnlyShoppingCartService)
        {
            _deleteAbleShoppingCartItemService = deleteAbleShoppingCartItemService;
            _addUpdateableShoppingCartItemService = addUpdateableShoppingCartItemService;
            _readOnlyShoppingCartItemService = readOnlyShoppingCartItemService;
            _readOnlyShoppingCartService = readOnlyShoppingCartService;
        }

        [HttpGet("")]
        public ActionResult<List<ShoppingCartItem>> GetAll()
        {
            var items = _readOnlyShoppingCartItemService.GetAll();
            if (items.Count() == 0 || items is null)
                return NotFound();
            return Ok(items);
        }

        [HttpGet("/{guid}")]
        public ActionResult<ShoppingCartItem> GetByGuid(Guid guid)
        {
            try
            {
                var item = _readOnlyShoppingCartItemService.GetByGuid(guid);
                if (item is null)
                    return NotFound();
                return Ok(item);
            }
            catch (KeyNotFoundException knfe)
            {
                return NotFound(knfe.Message);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("/ShoppingCart")]
        public ActionResult<List<ShoppingCartItem>> GetByShoppingCart([FromQuery]int shoppingCartId)
        {
            try
            {
                if (shoppingCartId == 0) return BadRequest();
                var shoppingCart = _readOnlyShoppingCartService.GetById(shoppingCartId);
                var items = _readOnlyShoppingCartItemService.GetByShoppingCart(shoppingCart);
                if (items.Count() == 0 || items is null)
                    return NotFound();
                return Ok(items);
            }
            catch (KeyNotFoundException knfe)
            {
                return NotFound(knfe.Message);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost("")]
        [Produces("application/json")]
        public ActionResult<ShoppingCartItem> Add(ShoppingCartItem shoppingCartItem)
        {
            try
            {
                var item = _addUpdateableShoppingCartItemService.Add(shoppingCartItem);
                return CreatedAtAction(nameof(GetByGuid), new { guid = item.guid }, item);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPut("")]
        [Produces("application/json")]
        public ActionResult<ShoppingCartItem> Update(ShoppingCartItem shoppingCartItem)
        {
            try
            {
                var item = _addUpdateableShoppingCartItemService.Update(shoppingCartItem);
                return Ok(item);
            }
            catch (KeyNotFoundException kE) { return BadRequest(kE.Message); }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpDelete("")]
        public ActionResult<ShoppingCartItem> Delete(Guid guid)
        {
            try
            {
                var item = _deleteAbleShoppingCartItemService.Delete(_readOnlyShoppingCartItemService.GetByGuid(guid));
                return Ok(item);
            }
            catch (KeyNotFoundException kE) { return Ok(); }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
