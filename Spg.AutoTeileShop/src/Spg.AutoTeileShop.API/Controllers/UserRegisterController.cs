using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spg.AutoTeileShop.Application.Services;
using Spg.AutoTeileShop.Domain.DTO;
using Spg.AutoTeileShop.Domain.Interfaces.UserInterfaces;
using Spg.AutoTeileShop.Domain.Interfaces.UserMailConfirmInterface;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;
using System.Text.Json;
using System.Text.Json.Serialization;

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

        //User user = new User();
        //user.Vorname = Vorname;
        //    user.Nachname = Nachname;
        //    user.Addrese = Addrese;
        //    user.Telefon = Telefon;
        //    user.Email = Email;
        //    user.PW = PW;
        
        [HttpPost()]
        [Produces("application/json")]
        public IActionResult Regist([FromBody()] JsonElement userDTOJSON)
        {
            try
            {
                UserRegistDTO userDTO = JsonSerializer.Deserialize<UserRegistDTO>(userDTOJSON);
                  User user = new(userDTO);
                  _userRegistService.Register_sendMail_Create_User(user, "");
                return Created("/api/User/" + user.Id, user);
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("SQLite Error 19: 'UNIQUE constraint failed: Users.Email")) return BadRequest("Email already exists");
                return StatusCode(StatusCodes.Status400BadRequest, ex.InnerException);
            }
        }
        [HttpGet("CheckCode/{mail}/{code}")]
        public IActionResult CheckCode(string mail, string code)
        {
            try
            {
                bool isChecked = _userRegistService.CheckCode_and_verify(mail, code);
                if(isChecked) return Ok();
                return BadRequest("Code konnte nicht gefunden werden");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
    