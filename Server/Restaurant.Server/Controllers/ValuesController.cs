using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Server.Api.Abstractions.Providers;
using Restaurant.Server.Api.Models;

namespace Restaurant.Server.Api.Controllers
{
	[Route("api/[controller]")]
	[Authorize]
	public class ValuesController : Controller
	{
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IUserBootstrapper _userBootsrapper;
		private readonly UserManager<User> _userManager;

		public ValuesController(
			UserManager<User> userManager,
			RoleManager<IdentityRole> roleManager,
			IUserBootstrapper userBootsrapper)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_userBootsrapper = userBootsrapper;
		}

		// GET api/values
		[HttpGet]
		public User Get()
		{
			var user = _userManager.GetUserAsync(User).Result;

			var result = _userManager.IsInRoleAsync(user, "Admin").Result;

			return user;
		}


		// GET api/values/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		// POST api/values
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}

		[HttpGet]
		[Route("CreateUsersAndRoles")]
		public async Task CreateUsersAndRoles()
		{
			await _userBootsrapper.CreateDefaultUsersAndRoles();
		}
	}
}