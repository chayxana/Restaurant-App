using Microsoft.AspNetCore.Mvc;

namespace Menu.API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }
    }
}