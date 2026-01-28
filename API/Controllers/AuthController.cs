using API.BusinessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto login)
        {
            // For demo: replace with real DB validation
            if (login.Username == "admin" && login.Password == "123")
            {
                var token = _tokenService.GenerateToken(login.Username);
                return Ok(new { token });
            }

            return Unauthorized();
        }
    }

    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

}
