using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spg.AutoTeileShop.Application.Helper;
using Spg.AutoTeileShop.Domain.DTO;
using Spg.AutoTeileShop.Domain.Helper;
using Spg.AutoTeileShop.Domain.Interfaces.ShoppingCart_Interfaces;
using Spg.AutoTeileShop.Domain.Interfaces.ShoppingCartItem_Interface;
using Spg.AutoTeileShop.Domain.Models;
using System.Security.Claims;

namespace Spg.AutoTeileShop.API.Controllers.V2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ShoppingCartItemController : ControllerBase
    {
        private readonly IDeleteAbleShoppingCartItemService _deleteAbleShoppingCartItemService;
        private readonly IAddUpdateableShoppingCartItemService _addUpdateableShoppingCartItemService;
        private readonly IReadOnlyShoppingCartItemService _readOnlyShoppingCartItemService;
        private readonly IReadOnlyShoppingCartService _readOnlyShoppingCartService;

        //Hateaos
        private readonly IEnumerable<EndpointDataSource> _endpointSources;
        private List<BuildRoutePattern> _routes;

        public ShoppingCartItemController
        (IDeleteAbleShoppingCartItemService deleteAbleShoppingCartItemService, IAddUpdateableShoppingCartItemService addUpdateableShoppingCartItemService,
        IReadOnlyShoppingCartItemService readOnlyShoppingCartItemService, IReadOnlyShoppingCartService readOnlyShoppingCartService, IEnumerable<EndpointDataSource> endpointSources,
        ListAllEndpoints listAllEndpoints)
        {
            _deleteAbleShoppingCartItemService = deleteAbleShoppingCartItemService;
            _addUpdateableShoppingCartItemService = addUpdateableShoppingCartItemService;
            _readOnlyShoppingCartItemService = readOnlyShoppingCartItemService;
            _readOnlyShoppingCartService = readOnlyShoppingCartService;

            //Hateaos
            _endpointSources = endpointSources;
            var apiVersionAttribute = (ApiVersionAttribute)Attribute.GetCustomAttribute(GetType(), typeof(ApiVersionAttribute));
            _routes = listAllEndpoints.ListAllEndpointsAndMethodes(GetType().Name, apiVersionAttribute?.Versions.FirstOrDefault()?.ToString(), this._endpointSources);

        }

        [HttpGet("")]
        [Authorize(Roles = "Admin")]
        public ActionResult<List<ShoppingCartItem>> GetAllShoppingCartItems()
        {
            var items = _readOnlyShoppingCartItemService.GetAll();
            HateoasBuild<ShoppingCartItem, int> hb = new HateoasBuild<ShoppingCartItem, int>();
            
            if (items.Count() == 0 || items is null)
                return NotFound();
            return Ok(hb.buildHateoas(items.ToList(), items.Select(s => s.Id).ToList(), _routes));
        }

        [HttpGet("{guid}")]
        [Authorize(Roles = "UserOrAdmin")]
        public ActionResult<ShoppingCartItem> GetShoppingCartItemByGuid(Guid guid)
        {
            try
            {
                var item = _readOnlyShoppingCartItemService.GetByGuid(guid);

                // Check if User who is mentiont in the Cart is the same as the User who is logged in or the User is an admin
                if (
                (bool)(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
                .Equals(item.ShoppingCartNav.UserNav.Guid.ToString())) == false
                &&
                (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == "Admin") == false) return Unauthorized();


                if (item is null)
                    return NotFound();
                HateoasBuild<ShoppingCartItem, int> hb = new HateoasBuild<ShoppingCartItem, int>();
                return Ok(hb.buildHateoas(item, item.Id, _routes));
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

        [HttpGet("ShoppingCart")]
        [Authorize(Roles = "UserOrAdmin")]
        public ActionResult<List<ShoppingCartItemDTOGet>> GetShoppingCartItemByShoppingCart([FromQuery] int shoppingCartId)
        {
            try
            {
                HateoasBuild<ShoppingCartItemDTOGet, int> hb = new HateoasBuild<ShoppingCartItemDTOGet, int>();

                if (shoppingCartId == 0) return BadRequest();
                var shoppingCart = _readOnlyShoppingCartService.GetById(shoppingCartId);

                // Check if User who is mentiont in the Cart is the same as the User who is logged in or the User is an admin
                if (
                (bool)(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
                .Equals(shoppingCart.UserNav.Guid.ToString())) == false
                &&
                (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == "admin") == false) return Unauthorized();

                var items = _readOnlyShoppingCartItemService.GetByShoppingCart(shoppingCart);
                if (items.Count() == 0 || items is null)
                    return NotFound();
            
                List<ShoppingCartItemDTOGet> itemsDTO = new();
                foreach (ShoppingCartItem item in items)
                {
                    itemsDTO.Add(new ShoppingCartItemDTOGet(item));   
                }
                return Ok(hb.buildHateoas(itemsDTO.ToList(), itemsDTO.Select(s => s.Id).ToList(), _routes));
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
        [Authorize(Roles = "UserOrAdmin")]
        public ActionResult<ShoppingCartItem> AddShoppingCartItem([FromBody] ShoppingCartItemPostDTO shoppingCartItem)
        {
            try
            {
                if (
                (bool)(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
                .Equals(shoppingCartItem.ShoppingCartNav.UserNav.Guid.ToString())) == false
                &&
                (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == "admin") == false) return Unauthorized();

                var item = _addUpdateableShoppingCartItemService.Add(new ShoppingCartItem(shoppingCartItem));
                return CreatedAtAction(nameof(GetShoppingCartItemByGuid), new { item.guid }, item);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPut("")]
        [Produces("application/json")]
        [Authorize(Roles = "UserOrAdmin")]
        public ActionResult<ShoppingCartItem> UpdateShoppingCartItem(ShoppingCartItem shoppingCartItem)
        {
            try
            {
                if (
                (bool)(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
                .Equals(shoppingCartItem.ShoppingCartNav.UserNav.Guid.ToString())) == false
                &&
                (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == "admin") == false) return Unauthorized();

                var item = _addUpdateableShoppingCartItemService.Update(shoppingCartItem);
                HateoasBuild<ShoppingCartItem, int> hb = new HateoasBuild<ShoppingCartItem, int>();

                return Ok(hb.buildHateoas(item, item.Id, _routes));
            }
            catch (KeyNotFoundException kE) { return BadRequest(kE.Message); }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{guid}")]
        [Authorize(Roles = "UserOrAdmin")]
        public ActionResult<ShoppingCartItem> DeleteShoppingCartItem([FromQuery] Guid guid)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            try
            {
                var itemS = _readOnlyShoppingCartItemService.GetByGuid(guid);

                if (
                (bool)(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
                .Equals(itemS.ShoppingCartNav.UserNav.Guid.ToString())) == false
                &&
                (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == "admin") == false) return Unauthorized();

                var item = _deleteAbleShoppingCartItemService.Delete(itemS);
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
