using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spg.AutoTeileShop.Domain.DTO;
using Spg.AutoTeileShop.Domain.Interfaces.UserInterfaces;
using Spg.AutoTeileShop.Domain.Models;
using System.Text.Json;

namespace Spg.AutoTeileShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        //Add Methode für User ist in UserRegisterController da sie sonst nicht gebraucht wird
        [HttpGet("")]
        public IActionResult GetAllUser()
        {
            IReadOnlyList<User> response = _readOnlyUserService.GetAll();
            if (response.Count == 0) { return NotFound(); }
            if (response == null) { return NotFound(); }
            return Ok(response);
        }

        [HttpGet("id/{id}")] //fürs Tests, wird noch gelöcht
        public IActionResult GetUserById(int id)
        {
            User response = _readOnlyUserService.GetById(id);
            if (response == null) { return NotFound(); }
            return Ok(response);
        }

        [HttpGet("{guid}")]
        public IActionResult GetUserByGuid(Guid guid)
        {
            User response = _readOnlyUserService.GetByGuid(guid);
            if (response == null) { return NotFound(); }
            return Ok(response);
        }

        [HttpDelete("{guid}")]
        public IActionResult DeleteUserByGuid(Guid guid)
        {
            try
            {
                User response = _readOnlyUserService.GetByGuid(guid);
                if (response is not null)
                {
                    _deletableUserService.Delete(response);
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut("{guid}")]
        public IActionResult UpdateUser([FromBody()] UserUpdateDTO userJSON, Guid guid)
        {
            try
            {
                _addUpdateableUserService.Update(guid, new User(userJSON));
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
         
    }
}
