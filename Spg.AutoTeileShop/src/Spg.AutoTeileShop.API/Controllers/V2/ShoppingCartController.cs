using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spg.AutoTeileShop.Application.Helper;
using Spg.AutoTeileShop.Domain.DTO;
using Spg.AutoTeileShop.Domain.Helper;
using Spg.AutoTeileShop.Domain.Interfaces.ShoppingCart_Interfaces;
using Spg.AutoTeileShop.Domain.Interfaces.UserInterfaces;
using Spg.AutoTeileShop.Domain.Models;

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

        //Hateaos
        private readonly IEnumerable<EndpointDataSource> _endpointSources;
        private List<BuildRoutePattern> _routes;

        public ShoppingCartController
        (IReadOnlyShoppingCartService shoppingCartService, IDeletableShoppingCartService deletableShoppingCartService,
        IAddUpdateableShoppingCartService updatableShoppingCartService, IReadOnlyUserService readOnlyUserService, IEnumerable<EndpointDataSource> endpointSources,
        ListAllEndpoints listAllEndpoints)

        {
            _redOnlyShoppingCartService = shoppingCartService;
            _deletableShoppingCartService = deletableShoppingCartService;
            _addUpdatableShoppingCartService = updatableShoppingCartService;
            _readOnlyUserService = readOnlyUserService;
            
            //Hateaos
            _endpointSources = endpointSources;
            var apiVersionAttribute = (ApiVersionAttribute)Attribute.GetCustomAttribute(GetType(), typeof(ApiVersionAttribute));
            _routes = listAllEndpoints.ListAllEndpointsAndMethodes(GetType().Name, apiVersionAttribute?.Versions.FirstOrDefault()?.ToString(), this._endpointSources);

        }

        // All - Authorization

        [HttpGet("")]
        [Authorize(Roles = "Admin")]
        public ActionResult<List<ShoppingCart>> GetAllShoppingCarts()
        {
            try
            {

                List<ShoppingCart> carts = _redOnlyShoppingCartService.GetAll().ToList();
                HateoasBuild<ShoppingCart, int> hb = new HateoasBuild<ShoppingCart, int>();

                if (carts.Count == 0) { return NotFound(); }
                return Ok(hb.buildHateoas(carts.ToList(), carts.Select(s => s.Id).ToList(), _routes));
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("{guid}")]
        [Authorize(Policy = "UserOrAdmin")]
        public ActionResult<ShoppingCart> GetShoppingCartByGuid(Guid guid)
        {

            try
            {
                var cart = _redOnlyShoppingCartService.GetByGuid(guid);
                HateoasBuild<ShoppingCart, int> hb = new HateoasBuild<ShoppingCart, int>();

                if (cart == null)
                {
                    return NotFound();
                }
                // Check if User is mentiont in the Cart or the User is an admin 

                if (
                (bool)(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
                .Equals(cart.UserNav.Guid.ToString())) == false
                &&
                (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == "admin") == false) return Unauthorized();

                return Ok(hb.buildHateoas(cart, cart.Id, _routes));
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
        [Authorize(Policy = "UserOrAdmin")]  
        public ActionResult<ShoppingCart> GetShoppingCartByUserNav([FromQuery] Guid userGuid)
        {
            try
            {

                var cart = _redOnlyShoppingCartService.GetByUserNav(userGuid);
                HateoasBuild<ShoppingCart, int> hb = new HateoasBuild<ShoppingCart, int>();

                if (cart == null)
                {
                    return NotFound();
                }
                if (
               (bool)(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
               .Equals(cart.UserNav.Guid.ToString())) == false
               &&
               (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == "admin") == false) return Unauthorized();

                return Ok(hb.buildHateoas(cart, cart.Id, _routes));
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
        [Authorize(Policy = "UserOrAdmin")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<ShoppingCart> AddShoppingCart(ShoppingCartPostDTO cartDTO)
        {
            try
            {
                ShoppingCart cart = new(cartDTO);
                if (cartDTO is null) return BadRequest("User Navigation is null");
                cart.UserNav = _readOnlyUserService.GetById((int)cartDTO.UserId);


                // Check if User who is mentiont in the Cart is the same as the User who is logged in or the User is an admin
                if (
                (bool)(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
                .Equals(cart.UserNav.Guid.ToString())) == false
                &&
                (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == "admin") == false) return Unauthorized();


                var newCart = _addUpdatableShoppingCartService.AddShoppingCart(cart);

                return Created($"/api/ShoppingCart/{newCart.guid}", newCart);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains($"No User Found with Id: {cartDTO.UserId}")) return BadRequest("User Navigation: " + ex.Message);
                return BadRequest();
            }
        }

        [HttpDelete("{guid}")]
        [Produces("application/json")]
        [Authorize(Policy = "UserOrAdmin")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult DeleteShoppingCart(Guid guid)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            try
            {
                ShoppingCart cart = _redOnlyShoppingCartService.GetByGuid(guid);
                if (cart is null) return BadRequest("No ShoppingCart with this Guid");


                // Check if User who is mentiont in the Cart is the same as the User who is logged in or the User is an admin
                if (
                (bool)(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
                .Equals(cart.UserNav.Guid.ToString())) == false
                &&
                (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == "admin") == false) return Unauthorized();


                var newCart = _deletableShoppingCartService.Remove(cart);

                return Accepted(newCart);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }
}
