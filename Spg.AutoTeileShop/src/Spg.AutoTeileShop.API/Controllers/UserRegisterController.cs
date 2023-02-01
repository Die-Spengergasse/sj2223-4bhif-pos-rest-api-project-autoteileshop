using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Spg.AutoTeileShop.Domain.DTO;
using Spg.AutoTeileShop.Domain.Interfaces.UserInterfaces;
using Spg.AutoTeileShop.Domain.Models;
using System.Text.Json;
//using Spg.AutoTeileShop.Application.Filter;

namespace Spg.AutoTeileShop.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")] 
    [ApiController]
    [ApiVersion("1.0")]
    public class RegisterController : ControllerBase
    {
        private readonly IUserRegistrationService _userRegistService;

        public RegisterController(IUserRegistrationService userRegistService)
        {
            _userRegistService = userRegistService;
        }

        // Register - Authorization

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public ActionResult<User> Register([FromBody()] UserRegistDTO userDTOJSON)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {            
                User user = new(userDTOJSON);
                _userRegistService.Register_sendMail_Create_User(user, "");
                return Created("/api/User/" + user.Guid, new UserRegisterResponsDTO(user));
            }
            catch (SqliteException ex)
            {
                if (ex.InnerException.Message.Contains("SQLite Error 19: 'UNIQUE constraint failed: Users.Email")) return BadRequest("Email already exists");
                return BadRequest();
            }
            catch (Exception e)
            {
                if (e.InnerException.Message.Contains("UNIQUE constraint failed: Users.Email")) return BadRequest("Email already exists");

                return BadRequest();
            }
        }
        [HttpGet("CheckCode/{mail}/{code}")]
        [AllowAnonymous]
        public IActionResult CheckCode(string mail, string code)
        {
            try
            {
                bool isChecked = _userRegistService.CheckCode_and_verify(mail, code);
                if (isChecked) return Ok();
                return BadRequest("Code konnte nicht gefunden werden");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Code ist abgelaufen")) return BadRequest(ex);
                if (ex.Message.Contains("Falscher Code")) return BadRequest(ex);
                if (ex.Message.Contains("Es wurde keine passende Mail gefunden")) return BadRequest(ex);
                return BadRequest();
            }
        }


    }
}
