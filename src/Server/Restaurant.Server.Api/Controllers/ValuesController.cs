using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Restaurant.Server.Api.Controllers
{
	[Route("api/[controller]")]
    public class ValuesController : Controller
    {
		[HttpGet]
		[Authorize]
	    public IEnumerable<string> Get()
		{
			return new[] {"Test1", "Test1", "Test1", "Test1", "Test1", "Test1", "Test1", "Test1"};
		}
    }
}
