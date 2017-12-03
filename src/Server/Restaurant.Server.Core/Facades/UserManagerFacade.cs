using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurant.Server.Abstraction.Facades;
using Restaurant.Server.DataProvider;
using Restaurant.Server.Models;

namespace Restaurant.Server.Core.Facades
{
	[ExcludeFromCodeCoverage]
	public class UserManagerFacade : IUserManagerFacade
	{
		private readonly DatabaseContext _context;
		private readonly UserManager<User> _userManager;

		public UserManagerFacade(UserManager<User> userManager, DatabaseContext context)
		{
			_userManager = userManager;
			_context = context;
		}

		public Task<IdentityResult> Create(User user, string password)
		{
			return _userManager.CreateAsync(user, password);
		}

		public async Task<User> GetAsync(ClaimsPrincipal principal)
		{
			var userId = principal.FindFirst(ClaimTypes.NameIdentifier).Value;

			return await _context.Users
				.Include(x => x.UserProfile)
				.SingleOrDefaultAsync(x => x.Id == userId);
		}

		public IEnumerable<User> GetAllUsers()
		{
			return _userManager.Users
				.Include(x => x.UserProfile)
				.Include(x => x.Orders)
				.ToList();
		}


		public Task<IdentityResult> UpdateAsync(User user)
		{
			return _userManager.UpdateAsync(user);
		}
	}
}