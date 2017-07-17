using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Restaurant.Server.Models;

namespace Restaurant.Server.Abstractions.Facades
{
    public interface IUserManagerFacade
    {
        Task<IdentityResult> Create(User user, string password);
    }
}