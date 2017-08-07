using System.Threading.Tasks;

namespace Restaurant.Server.Api.Abstractions.Providers
{
    public interface IUserAndRoleBootstrapper
    {
        Task CreateDefaultUsersAndRoles();
    }
}
