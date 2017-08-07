using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Server.Api.Models;

namespace Restaurant.Server.Api.Controllers
{
    [Route("api/[controller]")]
	[AllowAnonymous]
    public class ValuesController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ValuesController(
            UserManager<User> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        // GET api/values
        [HttpGet]
        [Authorize(Roles = "Member")]
        public IEnumerable<string> Get()
        {
            //var user = _userManager.GetUserAsync(User).Result;

            //var result =_userManager.IsInRoleAsync(user, "Admin").Result;

            return new string[] { "value1", "value2" };
        }
		

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
