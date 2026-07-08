using Microsoft.AspNetCore.Mvc;
using MVC2026july.Models;
using System.Diagnostics;

namespace MVC2026july.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            model.Username = "admin";
            model.Password = "202cb962ac59075b964b07152d234b70";
            using var client = new HttpClient();
            var response = await client.PostAsJsonAsync("http://localhost:50000/api/Login/login", model);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
                HttpContext.Session.SetString("JWToken", result.Token);
                return RedirectToAction("Index", "Orders");
            }

            ViewBag.Error = "Invalid credentials";
            return View(model);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
