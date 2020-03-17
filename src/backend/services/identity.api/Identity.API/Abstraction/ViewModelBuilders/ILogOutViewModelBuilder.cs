using System.Threading.Tasks;
using Identity.API.Controllers.Account;

namespace Identity.API.Abstraction.ViewModelBuilders
{
    public interface ILogOutViewModelBuilder
    {
        Task<LogoutViewModel> Build(string logoutId);
    }
}