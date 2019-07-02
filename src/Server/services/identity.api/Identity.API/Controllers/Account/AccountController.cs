using System;
using System.Threading.Tasks;
using Identity.API.Abstraction.Providers;
using Identity.API.Abstraction.ViewModelBuilders;
using Identity.API.Model.Entities;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Identity.API.Controllers.Account
{
    [SecurityHeaders]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly ILoginProvider _loginProvider;
        private readonly ILoggedOutViewModelBuilder _loggedOutViewModelBuilder;
        private readonly ILoginViewModelBuilder _loginViewModelBuilder;
        private readonly ILogOutViewModelBuilder _logOutViewModelBuilder;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IEventService _events;

        public AccountController(
            ILoginProvider loginProvider,
            ILoggedOutViewModelBuilder loggedOutViewModelBuilder,
            ILoginViewModelBuilder loginViewModelBuilder,
            ILogOutViewModelBuilder logOutViewModelBuilder,
            IIdentityServerInteractionService interaction,
            IClientStore clientStore)
        {
            _loginProvider = loginProvider;
            _loggedOutViewModelBuilder = loggedOutViewModelBuilder;
            _loginViewModelBuilder = loginViewModelBuilder;
            _logOutViewModelBuilder = logOutViewModelBuilder;
            _interaction = interaction;
            _clientStore = clientStore;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var vm = await _loginViewModelBuilder.Build(returnUrl);
            if (vm.IsExternalLoginOnly) // we only have one option for logging in and it's an external provider
            {
                return RedirectToAction(nameof(ExternalController.Challenge), "External", new { provider = vm.ExternalLoginScheme, returnUrl });
            }
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginInputModel model)
        {
            var vm = await BuildLoginViewModel(model);
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var result = await _loginProvider.LoginUser(model);
            if (result == SignInResult.Success)
            {
                var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);
                if (context != null)
                {
                    if (await _clientStore.IsPkceClientAsync(context.ClientId))
                    {
                        // if the client is PKCE then we assume it's native, so this change in how to
                        // return the response is for better UX for the end user.
                        return View("Redirect", new RedirectViewModel { RedirectUrl = model.ReturnUrl });
                    }
                    // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                    return Redirect(model.ReturnUrl);
                }

                if (string.IsNullOrEmpty(model.ReturnUrl))
                {
                    return Redirect("~/");
                }

                // request for a local page
                if (Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }

                // user might have clicked on a malicious link - should be logged
                throw new Exception("invalid return URL");
            }

            ModelState.AddModelError("", "Invalid username or password");
            return View(vm);
        }

        private async Task<LoginViewModel> BuildLoginViewModel(LoginInputModel model)
        {
            var vm = await _loginViewModelBuilder.Build(model.ReturnUrl);
            vm.Username = model.Username;
            vm.RememberLogin = model.RememberLogin;
            return vm;
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            var vm = await _logOutViewModelBuilder.Build(logoutId);
            if (!vm.ShowLogoutPrompt)
            {
                // if the request for logout was properly authenticated from IdentityServer, then
                // we don't need to show the prompt and can just log the user out directly.
                return await Logout(vm);
            }

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(LogoutInputModel model)
        {
            // build a model so the logged out page knows what to display
            var vm = await _loggedOutViewModelBuilder.Build(model.LogoutId);

            await _loginProvider.LogOut();

            // check if we need to trigger sign-out at an upstream identity provider
            if (vm.TriggerExternalSignout)
            {
                // build a return URL so the upstream provider will redirect back
                // to us after the user has logged out. this allows us to then
                // complete our single sign-out processing.
                string url = Url.Action("Logout", new { logoutId = vm.LogoutId });
                // this triggers a redirect to the external provider for sign-out
                return SignOut(new AuthenticationProperties { RedirectUri = url }, vm.ExternalAuthenticationScheme);
            }

            return View("LoggedOut", vm);
        }
    }
}