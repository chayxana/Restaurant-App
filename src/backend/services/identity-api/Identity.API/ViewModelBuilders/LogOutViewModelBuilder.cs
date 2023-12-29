using System.Threading.Tasks;
using Identity.API.Abstraction.ViewModelBuilders;
using Identity.API.Controllers.Account;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Http;

namespace Identity.API.ViewModelBuilders
{
    public class LogOutViewModelBuilder : ILogOutViewModelBuilder
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogOutViewModelBuilder(
            IHttpContextAccessor httpContextAccessor,
            IIdentityServerInteractionService interaction)
        {
            _httpContextAccessor = httpContextAccessor;
            _interaction = interaction;
        }

        public Task<LogoutViewModel> Build(string logoutId) => BuildLogoutViewModelAsync(logoutId);

        private async Task<LogoutViewModel> BuildLogoutViewModelAsync(string logoutId)
        {
            var isAuthenticated = _httpContextAccessor.HttpContext?.User?.IsAuthenticated();
            var vm = new LogoutViewModel { LogoutId = logoutId, ShowLogoutPrompt = AccountOptions.ShowLogoutPrompt };

            if (isAuthenticated != true)
            {
                // if the user is not authenticated, then just show logged out page
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            var context = await _interaction.GetLogoutContextAsync(logoutId);
            if (context?.ShowSignoutPrompt == false)
            {
                // it's safe to automatically sign-out
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            // show the logout prompt. this prevents attacks where the user
            // is automatically signed out by another malicious web page.
            return vm;
        }
    }
}