using Microsoft.AspNetCore.Mvc;
using Spg.AutoTeileShop.Domain.DTO;
using Spg.AutoTeileShop.Domain.DTO_MVC;
using Spg.AutoTeileShop.Domain.Interfaces.Authentication;

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
                HttpContext.Response.Cookies.Append("4bhif_login", dto.Email);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            return View();
        }
    }
}
