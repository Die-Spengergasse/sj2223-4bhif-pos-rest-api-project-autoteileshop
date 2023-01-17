using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Spg.AutoTeileShop.Domain.DTO;
using Spg.AutoTeileShop.Domain.Interfaces.UserInterfaces;
using Spg.AutoTeileShop.Domain.Models;
using System.Text.Json;

namespace Spg.AutoTeileShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUserRegistrationService _userRegistService;

        public RegisterController(IUserRegistrationService userRegistService)
        {
            _userRegistService = userRegistService;
        }

        [HttpPost("")]
        [Produces("application/json")]
        public ActionResult<User> Register([FromBody()] UserRegistDTO userDTOJSON)
        {
            try
            {            
                User user = new(userDTOJSON);
                _userRegistService.Register_sendMail_Create_User(user, "");
                user.PW = null;
                return Created("/api/User/" + user.Guid, user);
            }
            catch (SqliteException ex)
            {
                if (ex.InnerException.Message.Contains("SQLite Error 19: 'UNIQUE constraint failed: Users.Email")) return BadRequest("Email already exists");
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        [HttpGet("CheckCode/{mail}/{code}")]
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
