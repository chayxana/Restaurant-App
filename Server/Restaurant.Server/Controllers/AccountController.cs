using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Common.DataTransferObjects;
using Restaurant.Server.Api.Abstractions.Facades;
using Restaurant.Server.Api.Models;

namespace Restaurant.Server.Api.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
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
    }
}
