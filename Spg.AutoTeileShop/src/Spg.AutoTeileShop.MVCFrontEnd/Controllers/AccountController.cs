using Microsoft.AspNetCore.Mvc;
using Spg.AutoTeileShop.Domain.DTO;
using Spg.AutoTeileShop.Domain.DTO_MVC;
using Spg.AutoTeileShop.Domain.Helper;
using Spg.AutoTeileShop.Domain.Interfaces.Authentication;
using System.Text.Json;

namespace Spg.AutoTeileShop.MVCFrontEnd.Controllers
{
    public class AccountController : Controller
    {
        private readonly IDbAuthService _dbAuthService;
        public AccountController(IDbAuthService dbAuthService)
        {
            _dbAuthService = dbAuthService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            string userName = HttpContext.Request.Cookies["User"] ?? "not loged in";
            return View("Login", userName);
        }

        [HttpPost]
        public IActionResult Login(LoginDTO dto)
        {
            (UserUpdateDTO? user, bool b1) = _dbAuthService.CheckCredentials(dto.Email, "testPW");
            if (b1)
            {
                user.Signature = HashHelpers.CalculateHash($"{user.Email}{user.Role}", "gI976UUn3/m59A==");
                string json = JsonSerializer.Serialize(user);
                HttpContext.Response.Cookies.Append("4bhif_login", json);
                return RedirectToAction("Index", "Home");
            }
            return View("Login", new LoginDTO() { Email = "" });
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            return View();
        }
    }
}
