using System.Threading.Tasks;
using Identity.API.Controllers.Account;

namespace Identity.API.Abstraction.ViewModelBuilders
{
    public interface ILoggedOutViewModelBuilder
    {
        Task<LoggedOutViewModel> Build(string logoutId);
    }
}