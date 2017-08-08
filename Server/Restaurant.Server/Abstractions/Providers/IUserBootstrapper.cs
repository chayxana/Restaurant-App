using System.Threading.Tasks;

namespace Restaurant.Server.Api.Abstractions.Providers
{
    public interface IUserBootstrapper
    {
        Task CreateDefaultUsersAndRoles();
    }
}
