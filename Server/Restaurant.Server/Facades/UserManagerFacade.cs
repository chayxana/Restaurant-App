using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Restaurant.Server.Api.Abstractions.Facades;
using Restaurant.Server.Api.Models;

namespace Restaurant.Server.Api.Facades
{
    public class UserManagerFacade : IUserManagerFacade
    {
        private readonly UserManager<User> _userManager;

        public UserManagerFacade(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public Task<IdentityResult> Create(User user, string password)
        {
            return _userManager.CreateAsync(user, password);
        }

	    public Task<User> GetAsync(ClaimsPrincipal principal)
	    {
		    return _userManager.GetUserAsync(principal);
	    }
    }
}
