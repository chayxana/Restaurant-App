using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Common.DataTransferObjects;
using Restaurant.Server.Abstraction.Facades;
using Restaurant.Server.Models;

namespace Restaurant.Server.Api.Controllers
{
	[Route("api/[controller]")]
	public class AccountController : Controller
	{
		private readonly IMapperFacade _mapper;
		private readonly IUserManagerFacade _userManagerFacade;

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
			var user = new User {Email = registerDto.Email, UserName = registerDto.UserName};
			var result = await _userManagerFacade.Create(user, registerDto.Password);

			return result.Succeeded ? Ok() : Error(result);
		}

		[HttpGet]
		[Authorize]
		[Route("GetUserInfo")]
		public async Task<UserDto> GetUserInfo()
		{
			var user = await _userManagerFacade.GetAsync(User);
			return _mapper.Map<UserDto>(user);
		}

	    private IActionResult Error(IdentityResult result)
	    {
	        foreach (var identityError in result.Errors)
	            ModelState.AddModelError(identityError.Code, identityError.Description);
	        return BadRequest(ModelState);
	    }
    }
}