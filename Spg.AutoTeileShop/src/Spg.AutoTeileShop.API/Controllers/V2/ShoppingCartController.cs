﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spg.AutoTeileShop.Domain.DTO;
using Spg.AutoTeileShop.Domain.Interfaces.ShoppingCart_Interfaces;
using Spg.AutoTeileShop.Domain.Interfaces.UserInterfaces;
using Spg.AutoTeileShop.Domain.Models;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace Spg.AutoTeileShop.API.Controllers.V2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IReadOnlyShoppingCartService _redOnlyShoppingCartService;
        private readonly IDeletableShoppingCartService _deletableShoppingCartService;
        private readonly IAddUpdateableShoppingCartService _addUpdatableShoppingCartService;
        private readonly IReadOnlyUserService _readOnlyUserService;

        public ShoppingCartController(IReadOnlyShoppingCartService shoppingCartService, IDeletableShoppingCartService deletableShoppingCartService, IAddUpdateableShoppingCartService updatableShoppingCartService, IReadOnlyUserService readOnlyUserService)
        {
            _redOnlyShoppingCartService = shoppingCartService;
            _deletableShoppingCartService = deletableShoppingCartService;
            _addUpdatableShoppingCartService = updatableShoppingCartService;
            _readOnlyUserService = readOnlyUserService;
        }

        // All - Authorization

        [HttpGet("")]
        [Authorize(Roles = "Admin")]
        public ActionResult<List<ShoppingCart>> GetAllShoppingCarts()
        {
            var carts = _redOnlyShoppingCartService.GetAll();
            if (carts.Count() == 0 || carts == null)
            {
                return NotFound();
            }
            return Ok(carts);
        }


        [HttpGet("{guid}")]
        public ActionResult<ShoppingCart> GetShoppingCartByGuid(Guid guid)
        {
            
            //if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == "Admin") return Unauthorized();
            try
            {
                var cart = _redOnlyShoppingCartService.GetByGuid(guid);
                if (cart == null)
                {
                    return NotFound();
                }
                // Check if User is mentiont in the Cart or the User is an Admin 
                if (
                !(bool)(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
                .Equals(cart.UserNav.Guid.ToString()))) return Unauthorized();

                if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value != "Admin" ) return Unauthorized();
   
                    
                return Ok(cart);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("ByUser")]
        public ActionResult<ShoppingCart> GetShoppingCartByUserNav([FromQuery] Guid userGuid)
        {
            try
            {

                var cart = _redOnlyShoppingCartService.GetByUserNav(userGuid);
                if (cart == null)
                {
                    return NotFound();
                }
                return Ok(cart);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("No Cart found with UserNav: " + userGuid)) return BadRequest(ex.Message);
                return BadRequest();
            }
        }

        [HttpPost("")]
        [Produces("application/json")]
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<ShoppingCart> AddShoppingCart(ShoppingCartPostDTO cartDTO)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            try
            {
                ShoppingCart cart = new(cartDTO);
                if (cartDTO is null) return BadRequest("User Navigation is null");
                cart.UserNav = _readOnlyUserService.GetById((int)cartDTO.UserId);
                var newCart = _addUpdatableShoppingCartService.AddShoppingCart(cart);
                return Created($"/api/ShoppingCart/{newCart.guid}", newCart);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains($"No User Found with Id: {cartDTO.UserId}")) return BadRequest("User Navigation: " + ex.Message);
                return BadRequest();
            }
        }

    }
}
