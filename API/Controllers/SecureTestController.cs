using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // This requires a valid JWT token
    public class SecureTestController : ControllerBase
    {
        [HttpGet("hello")]
        public IActionResult GetHello()
        {
            return Ok(new
            {
                Message = "JWT authentication successful!",
                User = User.Identity?.Name,
                Time = DateTime.UtcNow
            });
        }

        [HttpGet("products")]
        public IActionResult GetProducts()
        {
            var products = new[]
            {
                new { Id = 1, Name = "Laptop", Price = 1200 },
                new { Id = 2, Name = "Mouse", Price = 25 },
                new { Id = 3, Name = "Keyboard", Price = 45 }
            };

            return Ok(products);
        }
    }
}
