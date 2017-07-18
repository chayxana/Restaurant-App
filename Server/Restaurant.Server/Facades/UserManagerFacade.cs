using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Restaurant.Server.Abstractions.Facades;
using Restaurant.Server.Models;

namespace Restaurant.Server.Facades
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
    }
}
