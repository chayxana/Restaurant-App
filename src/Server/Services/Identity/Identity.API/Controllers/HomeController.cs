using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() 
        {
            return new RedirectResult("~/swagger");
        }
    }
}