using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
