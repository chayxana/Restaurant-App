using System.Threading.Tasks;
using Identity.API.Controllers.Account;
using Microsoft.AspNetCore.Identity;

namespace Identity.API.Abstraction.Providers
{
    public interface ILoginProvider
    {
        Task<SignInResult> LoginUser(LoginInputModel model);

        Task LogOut();
    }
}