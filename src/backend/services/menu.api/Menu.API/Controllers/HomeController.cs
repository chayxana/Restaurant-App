using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Menu.API.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;

        public HomeController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public IActionResult Index()
        {
            var pathBase = this.configuration["PATH_BASE"];
            var routePrefix = (!string.IsNullOrEmpty(pathBase) ? "/" + pathBase : string.Empty);
            return new RedirectResult($"~{routePrefix}/swagger/index.html");
        }
    }
}