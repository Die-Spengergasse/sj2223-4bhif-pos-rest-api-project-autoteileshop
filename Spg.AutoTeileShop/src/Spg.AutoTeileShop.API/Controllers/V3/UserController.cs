using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spg.AutoTeileShop.API.Helper;
using Spg.AutoTeileShop.Application.Helper;
using Spg.AutoTeileShop.Domain.DTO;
using Spg.AutoTeileShop.Domain.Helper;
using Spg.AutoTeileShop.Domain.Interfaces.UserInterfaces;
using Spg.AutoTeileShop.Domain.Models;
using System.Security.Claims;

namespace Spg.AutoTeileShop.API.Controllers.V3
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("3.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly IAddUpdateableUserService _addUpdateableUserService;
        private readonly IDeletableUserService _deletableUserService;
        private readonly IReadOnlyUserService _readOnlyUserService;
        private readonly IEnumerable<EndpointDataSource> _endpointSources;
        //requert for HATEOAS, List of Routes and Methodes
        private List<BuildRoutePattern> _routes;

        public UserController(IAddUpdateableUserService addUpdateableUserService, IDeletableUserService deletableUserService,
            IReadOnlyUserService readOnlyUserService, IEnumerable<EndpointDataSource> endpointSources,
            ListAllEndpoints listAllEndpoints)
        {
            _addUpdateableUserService = addUpdateableUserService;
            _deletableUserService = deletableUserService;
            _readOnlyUserService = readOnlyUserService;

            //requert for HATEOAS, List of Routes and Methodes
            _endpointSources = endpointSources;

            var apiVersionAttribute = (ApiVersionAttribute)Attribute.GetCustomAttribute(GetType(), typeof(ApiVersionAttribute));

            _routes = listAllEndpoints.ListAllEndpointsAndMethodes(GetType().Name, apiVersionAttribute?.Versions.FirstOrDefault()?.ToString(), this._endpointSources);

        }

        // All - Authorization

        //Add Methode für User ist in UserRegisterController da sie sonst nicht gebraucht wird
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<List<UserGetDTO>> GetAllUser()
        {
            IEnumerable<User> responseUser = _readOnlyUserService.GetAll();

            if (responseUser.ToList().Count == 0) { return NotFound(); }
            if (responseUser == null) { return NotFound(); }
            List<UserGetDTO> response = new List<UserGetDTO>();
            foreach (var user in responseUser)
            {
                response.Add(new UserGetDTO(user));
            }
            HateoasBuild<UserGetDTO, Guid> hb = new HateoasBuild<UserGetDTO, Guid>();

            return Ok(hb.buildHateoas(response.ToList(), response.Select(c => c.Guid).ToList(), _routes));
        }



        [HttpGet("{guid}")]
        [Authorize(Policy = "UserOrAdmin")]
        [ServiceFilter(typeof(UserOrAdminAuthorizationFilter))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<UserGetDTO> GetUserByGuid(Guid guid)
        {
            var userGuid = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            User response = null;

            try
            {
                response = _readOnlyUserService.GetByGuid(guid);
                if (response == null) return NotFound();

                //if (response.Guid.ToString().Equals(userGuid) == false && User.IsInRole("Admin") == false)
                    //return Unauthorized();
            }
            catch (Exception e)
            {
                if (e.Message.Contains("No User Found with Guid:"))
                {
                    return NotFound(e);
                }
                return BadRequest();
            }
            HateoasBuild<UserGetDTO, Guid> hb = new HateoasBuild<UserGetDTO, Guid>();
            return Ok(hb.buildHateoas(new UserGetDTO(response), response.Guid, _routes));
        }
        

        [HttpDelete("{guid}")]
        [Authorize(Policy = "UserOrAdmin")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<User> DeleteUserByGuid(Guid guid)
        {
            try
            {
                if (guid.ToString().Equals(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value)
                    == false && User.IsInRole("Admin") == false)
                    return Unauthorized();

                User response = _readOnlyUserService.GetByGuid(guid);
                if (response is not null)
                {
                    try
                    {
                        _deletableUserService.Delete(response);
                    }
                    catch (Exception e)
                    { }
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPut("{guid}")]
        [Authorize(Policy = "UserOrAdmin")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<User> UpdateUser([FromBody()] UserUpdateDTO userJSON, Guid guid)
        {
            try
            {
                if (guid.ToString().Equals(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value)
                    == false && User.IsInRole("Admin") == false)
                    return Unauthorized();
                _addUpdateableUserService.Update(guid, new User(userJSON));
                return Ok();
            }
            catch (Exception e)
            {
                if (e.Message.Contains("no User found"))
                {
                    return NotFound(e);
                }
                return BadRequest();
            }
        }

    }
}
