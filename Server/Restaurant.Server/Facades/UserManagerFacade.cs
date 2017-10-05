using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurant.Server.Api.Abstractions.Facades;
using Restaurant.Server.Api.Models;

namespace Restaurant.Server.Api.Facades
{
    public class UserManagerFacade : IUserManagerFacade
    {
        private readonly UserManager<User> _userManager;
	    private readonly DatabaseContext _context;

	    public UserManagerFacade(UserManager<User> userManager, DatabaseContext context)
	    {
		    _userManager = userManager;
		    _context = context;
	    }

        public Task<IdentityResult> Create(User user, string password)
        {
            return _userManager.CreateAsync(user, password);
        }

	    public Task<User> GetAsync(ClaimsPrincipal principal)
	    {
		    return _userManager.GetUserAsync(principal);
	    }

	    public Task<List<User>> GetAllUsers()
	    {
		    return _context.Users.ToListAsync();
	    }
    }
}
