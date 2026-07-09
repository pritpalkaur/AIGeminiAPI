using Microsoft.AspNetCore.Mvc;
using MVC2026july.Models;

namespace MVC2026july.Controllers
{
    [AuthorizeSession]
    public class CheckController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
