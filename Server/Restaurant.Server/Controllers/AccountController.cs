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
	    private readonly IMapperFacade _mapper;

	    public AccountController(
			IUserManagerFacade userManagerFacade,
			IMapperFacade mapper)
	    {
		    _userManagerFacade = userManagerFacade;
		    _mapper = mapper;
	    }

        [HttpPost]
		[Route("Register")]
		[AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var user = new User { Email = registerDto.Email, UserName = registerDto.UserName };
            var result = await _userManagerFacade.Create(user, registerDto.Password);

            return result.Succeeded ? Ok() : Error(result);
        }

		[HttpGet]
		[Authorize]
		[Route("GetUser")]
	    public async Task<UserDto> GetUser()
	    {
		    var user = await _userManagerFacade.GetAsync(User);
		    return _mapper.Map<UserDto>(user);
	    }
    }
}
