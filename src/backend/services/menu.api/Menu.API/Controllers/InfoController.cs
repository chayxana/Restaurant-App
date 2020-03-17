using Microsoft.AspNetCore.Mvc;

namespace Menu.API.Controllers
{
    public class InfoController : Controller
    {
        public string Index()
        {
            return "Menu API";
        }
    }
}