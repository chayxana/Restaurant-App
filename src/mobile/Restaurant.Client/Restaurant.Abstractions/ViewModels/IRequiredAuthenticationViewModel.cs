using System.Threading.Tasks;

namespace Restaurant.Abstractions.ViewModels
{
    public interface IRequiredAuthenticationViewModel
    {
	    Task<string> GetAccessToken();
	}
}
