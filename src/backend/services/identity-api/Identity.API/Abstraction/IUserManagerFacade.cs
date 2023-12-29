using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Identity.API.Model.Entities;
using Microsoft.AspNetCore.Identity;

namespace Identity.API.Abstraction
{
    public interface IUserManagerFacade
    {
        Task<IdentityResult> Create(ApplicationUser user, string password);

        Task<IdentityResult> UpdateAsync(ApplicationUser user);

        Task<ApplicationUser> GetAsync(ClaimsPrincipal principal);

        IEnumerable<ApplicationUser> GetAllUsers();
    }
}