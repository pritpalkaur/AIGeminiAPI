using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MVC2026july.Models;

namespace MVC2026july.Controllers
{
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
            var orders = await _client.GetFromJsonAsync<List<OrderViewModel>>(
            _apiSettings.OrdersEndpoint + "?pageNumber=1&pageSize=10");

            return View(orders);
        }
    }
}
