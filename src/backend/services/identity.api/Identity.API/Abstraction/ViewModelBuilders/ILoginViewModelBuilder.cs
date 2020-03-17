using System.Threading.Tasks;
using Identity.API.Controllers.Account;

namespace Identity.API.Abstraction.ViewModelBuilders
{
    public interface ILoginViewModelBuilder
    {
        Task<LoginViewModel> Build(string returnUrl);
    }
}