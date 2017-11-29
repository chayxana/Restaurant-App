using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Restaurant.Server.Auth
{
    public static class IdentityServerExtensions
    {
        public static IIdentityServerBuilder SetupIdentityServer(this IServiceCollection services, IHostingEnvironment env)
        {
            var identityServerBuilder = services.AddIdentityServer();
            
            if (env.IsEnvironment("Test"))
                identityServerBuilder.AddTestUsers(DefaultUsers.Get());

            return identityServerBuilder
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients());
	        
        }
    }
}
