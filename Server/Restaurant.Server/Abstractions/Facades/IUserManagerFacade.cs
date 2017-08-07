using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Restaurant.Server.Api.Models;

namespace Restaurant.Server.Api.Abstractions.Facades
{
    public interface IUserManagerFacade
    {
        Task<IdentityResult> Create(User user, string password);
    }
}