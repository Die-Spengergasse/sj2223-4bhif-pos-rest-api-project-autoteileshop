using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spg.AutoTeileShop.Domain.DTO;
using Spg.AutoTeileShop.Domain.Interfaces.UserInterfaces;
using Spg.AutoTeileShop.Domain.Models;
using System.Security.Claims;

namespace Spg.AutoTeileShop.API.Controllers.V2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly IAddUpdateableUserService _addUpdateableUserService;
        private readonly IDeletableUserService _deletableUserService;
        private readonly IReadOnlyUserService _readOnlyUserService;

        public UserController(IAddUpdateableUserService addUpdateableUserService, IDeletableUserService deletableUserService, IReadOnlyUserService readOnlyUserService)
        {
            _addUpdateableUserService = addUpdateableUserService;
            _deletableUserService = deletableUserService;
            _readOnlyUserService = readOnlyUserService;
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
            return Ok(response);
        }



        [HttpGet("{guid}")]
        [Authorize(Policy = "UserOrAdmin")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<UserGetDTO> GetUserByGuid(Guid guid)
        {
            var userGuid = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            User response = null;
            try
            {
                response = _readOnlyUserService.GetByGuid(guid);
                if (response == null) return NotFound();

                if (response.Guid.ToString().Equals(userGuid) == false && User.IsInRole("Admin") == false)
                    return Unauthorized();
            }
            catch (Exception e)
            {
                if (e.Message.Contains("No User Found with Guid:"))
                {
                    return NotFound(e);
                }
                return BadRequest();

            }
            return Ok(new UserGetDTO(response));
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
        [Authorize(Roles = "UserOrAdmin")]
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
