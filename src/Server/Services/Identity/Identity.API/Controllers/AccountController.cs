using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Identity.API.Abstraction;
using Identity.API.Model.DataTransferObjects;
using Identity.API.Model.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class AccountController : Controller
    {
        private readonly IUserManagerFacade _userManagerFacade;
        private readonly IAccountFileUploadManager _accountFileUploadManager;
        private readonly IMapper _mapper;

        public AccountController(
            IUserManagerFacade userManagerFacade,
            IAccountFileUploadManager accountFileUploadManager,
            IMapper mapper)
        {
            _userManagerFacade = userManagerFacade;
            _accountFileUploadManager = accountFileUploadManager;
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
                UserProfile = new UserProfile
                {
                    Picture = "http://via.placeholder.com/200x200"
                }
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
        [Authorize(Roles = "Admin")]
        public IEnumerable<UserDto> Users()
        {
            var users = _userManagerFacade.GetAllUsers();
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
            return userDtos;
        }

        [HttpPost]
        [Route("UpdateUserProfilePicture")]
        [Authorize]
        public async Task<IActionResult> UpdatePicture([Bind] IFormFile file)
        {
            var user = await _userManagerFacade.GetAsync(User);

            await _accountFileUploadManager.Upload(file, user.Id);

            user.UserProfile.Picture = _accountFileUploadManager.GetUploadedFileByUniqId(user.Id);

            var result = await _userManagerFacade.UpdateAsync(user);

            return result.Succeeded ? Ok() : Error(result);
        }

        private IActionResult Error(IdentityResult result)
        {
            foreach (var identityError in result.Errors)
                ModelState.AddModelError(identityError.Code, identityError.Description);
            return BadRequest(ModelState);
        }
    }
}