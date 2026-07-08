using Microsoft.AspNetCore.Mvc;
using MVC2026july.Models;

namespace MVC2026july.Controllers
{
    public class OrdersController : Controller
    {
        public async Task<IActionResult> Index()
        {
            using var client = new HttpClient();
            var orders = await client.GetFromJsonAsync<List<OrderViewModel>>(
                "http://localhost:50000/api/v1/Orders?pageNumber=1&pageSize=10");

            return View(orders);
        }
    }
}
