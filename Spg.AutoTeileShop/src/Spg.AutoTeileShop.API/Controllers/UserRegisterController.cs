using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spg.AutoTeileShop.Application.Services;
using Spg.AutoTeileShop.Domain.Interfaces.UserMailConfirmInterface;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;

namespace Spg.AutoTeileShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUserMailService _userMailService;

        public RegisterController(IUserMailService userMailService)
        {
            _userMailService = userMailService;
        }

        //User user = new User();
        //user.Vorname = Vorname;
        //    user.Nachname = Nachname;
        //    user.Addrese = Addrese;
        //    user.Telefon = Telefon;
        //    user.Email = Email;
        //    user.PW = PW;
        
        [HttpPost("")]
        public IActionResult Regist()
        {
        }
    }
}
