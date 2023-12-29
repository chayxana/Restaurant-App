using System;
using System.Threading.Tasks;
using Identity.API.Abstraction.Providers;
using Identity.API.Controllers.Account;
using Identity.API.Model.Entities;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Identity.API.Providers
{
    public class LoginProvider : ILoginProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEventService _events;

        public LoginProvider(
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEventService events)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _signInManager = signInManager;
            _events = events;
        }

        public async Task<SignInResult> LoginUser(LoginInputModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
            if (result != SignInResult.Success) return result;
            var user = await _userManager.FindByNameAsync(model.Username);
            await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id, user.UserName));
            return result;
        }

        public async Task LogOut()
        {
            var user = _httpContextAccessor.HttpContext.User;
            if (user.IsAuthenticated())
            {
                // delete local authentication cookie
                await _httpContextAccessor.HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

                // raise the logout event
                await _events.RaiseAsync(new UserLogoutSuccessEvent(user.GetSubjectId(), user.GetDisplayName()));
            }
        }
    }
}