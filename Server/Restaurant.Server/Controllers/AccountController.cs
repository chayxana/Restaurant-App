using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Server.Abstractions.Facades;
using Restaurant.Server.Models;

namespace Restaurant.Server.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IUserManagerFacade _userManagerFacade;

        public AccountController(IUserManagerFacade userManagerFacade)
        {
            _userManagerFacade = userManagerFacade;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            var user = new User { Email = userDto.Email, UserName = userDto.UserName };
            var result = await _userManagerFacade.Create(user, userDto.Password);

            return result.Succeeded ? Ok() : Error(result);
        }

        private IActionResult Error(IdentityResult result)
        {
            foreach (var identityError in result.Errors)
            {
                ModelState.AddModelError(identityError.Code, identityError.Description);
            }
            return BadRequest(ModelState);
        }
    }

    public class UserDto
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
