using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MVC2026july.Models;

namespace MVC2026july.Controllers
{
    [AuthorizeSession]
    public class OrdersController : Controller
    {
        private readonly HttpClient _client;
        private readonly ApiSettings _apiSettings;
        public OrdersController(IHttpClientFactory factory, IOptions<ApiSettings> apiSettings)
        {
            _client = factory.CreateClient();
            _apiSettings = apiSettings.Value;
        }
        public async Task<IActionResult> Index()
        {
            //var token = HttpContext.Session.GetString("JWToken");

            //if (string.IsNullOrEmpty(token))
            //{
            //    // Not logged in → redirect to Login page
            //    return RedirectToAction("Login", "Home");
            //}

            // Attach token to HttpClient
            //_client.DefaultRequestHeaders.Authorization =
            //    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var orders = await _client.GetFromJsonAsync<List<OrderViewModel>>(
            _apiSettings.OrdersEndpoint + "?pageNumber=1&pageSize=10");

            return View(orders);
        }
    }
}
