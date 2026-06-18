using API.DTOs.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;

public class HomeController : Controller
{
    private readonly HttpClient _http;

    public HomeController(IHttpClientFactory httpClientFactory)
    {
        _http = httpClientFactory.CreateClient();
    }

    public async Task<IActionResult> Index()
    {
        var response = await _http.GetAsync("http://localhost:50000/api/v1/Orders?pageNumber=1&pageSize=10");

        if (!response.IsSuccessStatusCode)
            return View(new List<OrderViewModel>());

        var json = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var orders = JsonSerializer.Deserialize<List<OrderViewModel>>(json, options);

        return View(orders);
    }
}
