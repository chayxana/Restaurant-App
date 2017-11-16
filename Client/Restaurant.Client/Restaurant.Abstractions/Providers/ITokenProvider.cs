using System.Threading.Tasks;
using Restaurant.Common.DataTransferObjects;

namespace Restaurant.Abstractions.Providers
{
    public interface ITokenProvider
    {
        Task<TokenResponse> RequestResourceOwnerPasswordAsync(string userName, string password);
    }
}