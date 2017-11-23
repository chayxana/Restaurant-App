using System.Threading.Tasks;

namespace Restaurant.Server.Abstraction.Providers
{
	public interface IUserBootstrapper
	{
		Task CreateDefaultUsersAndRoles();
	}
}