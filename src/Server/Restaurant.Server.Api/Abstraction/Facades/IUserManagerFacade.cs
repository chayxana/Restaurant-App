using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Restaurant.Server.Api.Models;

namespace Restaurant.Server.Api.Abstraction.Facades
{
	public interface IUserManagerFacade
	{
		Task<IdentityResult> Create(User user, string password);

		Task<IdentityResult> UpdateAsync(User user);

		Task<User> GetAsync(ClaimsPrincipal principal);
		
		IEnumerable<User> GetAllUsers();
	}
}