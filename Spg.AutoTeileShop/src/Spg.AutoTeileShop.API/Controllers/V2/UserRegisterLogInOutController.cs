using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Spg.AutoTeileShop.Application.Services;
using Spg.AutoTeileShop.Domain.DTO;
using Spg.AutoTeileShop.Domain.Interfaces.UserInterfaces;
using Spg.AutoTeileShop.Domain.Models;
using System.Security.Claims;
//using Spg.AutoTeileShop.Application.Filter;

namespace Spg.AutoTeileShop.API.Controllers.V2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class UserRegisterLogInOutController : ControllerBase
    {
        private readonly IUserRegistrationService _userRegistService;
        private readonly AuthService _authService;
        private readonly ILogger<UserController> _logger;

        public UserRegisterLogInOutController(IUserRegistrationService userRegistService,
            ILogger<UserController> logger, AuthService authService)
        {
            _userRegistService = userRegistService;
            _logger = logger;
            _authService = authService;
        }

        // Register - Authorization

        [HttpPost("Regist")]
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

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Login(UserCredentials user)
        {
            try
            {
                string token = await _authService.GenerateToken(user);

                if (token == null)
                {
                    return Unauthorized();
                }

                return Ok(token);
            }
            catch (Exception ex)
            {
                if(ex.Message.Contains("User is not confirmed")) return Unauthorized(ex.Message);
                return BadRequest();
            }
        }

        [HttpPost("loginform")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [AllowAnonymous]

        public async Task<ActionResult<string>> LoginAsync([FromForm] UserCredentials user)
        {
            await HttpContext
                .SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            ClaimsIdentity claimsIdentity;
            if ((claimsIdentity = await _authService.GenerateIdentity(user)) != null)
            {
                // Spezielle Properties (Expires, ...) können als 3. Parameter mit einer
                // AuthenticationProperties Instanz übergeben werden.
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));
            }
            return Redirect("/");
        }

        [HttpGet("logout")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext
                .SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

    }
}
