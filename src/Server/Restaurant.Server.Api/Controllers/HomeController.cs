using Microsoft.AspNetCore.Mvc;

namespace Restaurant.Server.Api.Controllers
{
	public class HomeController : Controller
    {
        public IActionResult Index()
        {
	        return new RedirectResult("~/swagger");
		}
    }
}