using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spg.AutoTeileShop.Application.Helper;
using Spg.AutoTeileShop.Domain.DTO;
using Spg.AutoTeileShop.Domain.Helper;
using Spg.AutoTeileShop.Domain.Interfaces.UserInterfaces;
using Spg.AutoTeileShop.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Spg.AutoTeileShop.API.Controllers.V2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
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
        public ActionResult<UserGetDTO> GetUserByGuid(Guid guid)
        {
            User response = null;
            try
            {
                response = _readOnlyUserService.GetByGuid(guid);
                if (response == null) { return NotFound(); }
            }
            catch (Exception e)
            {
                if (e.Message.Contains("No User Found with Guid:"))
                {
                    return NotFound(e);
                }
                return BadRequest();

            }
            return Ok(new UserGetDTO(response)); // Hateaos implementation failed caused by return statement (nothing to return)
        }

        [HttpDelete("{guid}")]
        public ActionResult<User> DeleteUserByGuid(Guid guid)
        {
            try
            {
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
        public ActionResult<User> UpdateUser([FromBody()] UserUpdateDTO userJSON, Guid guid)
        {
            try
            {
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
