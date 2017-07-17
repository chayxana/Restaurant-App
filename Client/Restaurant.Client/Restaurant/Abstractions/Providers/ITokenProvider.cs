using System.Threading.Tasks;
using Restaurant.DataTransferObjects;

namespace Restaurant.Abstractions.Providers
{
    public interface ITokenProvider
    {
        Task<TokenResponse> RequestResourceOwnerPasswordAsync(string userName, string password);
    }
}
