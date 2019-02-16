using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Identity.API.Abstraction;
using Identity.API.Model.DataTransferObjects;
using Identity.API.Model.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers.Account
{
    [Route("api/v1/[controller]")]
    public class AccountApiController : Controller
    {
        private readonly IUserManagerFacade _userManagerFacade;
        private readonly IMapper _mapper;

        public AccountApiController(
            IUserManagerFacade userManagerFacade,
            IMapper mapper)
        {
            _userManagerFacade = userManagerFacade;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var user = new ApplicationUser
            {
                Email = registerDto.Email,
                UserName = registerDto.Email,
                UserProfile = new UserProfile()
            };
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

        [HttpGet]
        [Route("GetAllUsers")]
        public IEnumerable<UserDto> Users()
        {
            var users = _userManagerFacade.GetAllUsers();
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
            return userDtos;
        }

        private IActionResult Error(IdentityResult result)
        {
            foreach (var identityError in result.Errors)
                ModelState.AddModelError(identityError.Code, identityError.Description);
            return BadRequest(ModelState);
        }
    }
}