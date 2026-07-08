using API.BusinessLayer;
using API.DTOs.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        public LoginController(ITokenService tokenService, IUserService userService)
        {
            _tokenService = tokenService;
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult ValidateUser([FromBody] LoginDto login)
        {
            var user = _userService.ValidateUser(login.Username, login.Password);

            if (user == null)
            {
                // Invalid credentials
                return Unauthorized();
            }

            // Valid credentials → issue JWT
            var token = _tokenService.GenerateToken(user.Username);

            return Ok(new
            {
                token,
                user = new
                {
                    user.Id,
                    user.Username
                }
            });
        }

        //public IActionResult Login([FromBody] LoginDto login)
        //{
        //    var user = _userService.ValidateUser(login.Username, login.Password);

        //    if (user != null)
        //    {
        //        // For demo: replace with real DB validation
        //        if (login.Username == "admin" && login.Password == "123")
        //        {
        //            var token = _tokenService.GenerateToken(login.Username);
        //            return Ok(new { token });
        //        }

        //        return Unauthorized();
        //    }
        //}
        //public UserViewModel ValidateUser(string username, string password)
        //{
        //    var user = _context.Users.FirstOrDefault(u => u.Username == username);
        //    if (user == null) return null;

        //    // If you’re still storing plain text (for now):
        //    if (user.Password != password)
        //        return null;

        //    // If you’ve moved to hash + salt:
        //    // recompute hash with entered password + stored salt and compare to PasswordHash

        //    return new UserViewModel
        //    {
        //        Id = user.Id,
        //        Username = user.Username,
        //        Email = user.Email
        //    };
        //}

        public class LoginDto
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
