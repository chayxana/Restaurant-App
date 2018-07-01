using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Common.Constants;
using Restaurant.Server.Api.IdentityServer;

namespace Restaurant.Server.Api.IntegrationTests.Config
{
    public class IdentityServerSetup
    {
        private readonly string _apiName;
        private TestServer _identityServer;
        private const string TokenEndpoint = "http://localhost/connect/token";
        public HttpMessageHandler _handler;

        public IdentityServerSetup()
        {
	        _apiName = ApiConstants.ApiName;
        }

        
        public IdentityServerSetup Initialize()
        {
            InitializeIdentityServer();
            return this;
        }

        public void  GetIdentityServerAuthenticationOptions(IdentityServerAuthenticationOptions options)
        {
	        
        }

        public async Task<string> GetAccessTokenForUser(string userName, string password, string clientId = ApiConstants.ClientId, string clientSecret = ApiConstants.ClientSecret)
        {
            var client = new TokenClient(TokenEndpoint, clientId, clientSecret, innerHttpMessageHandler: _handler);
            
            var response = await client.RequestResourceOwnerPasswordAsync(userName, password, _apiName);
            return response.AccessToken;
        }

        public async Task SetAccessToken(HttpClient client, string username, string password, string clientId = ApiConstants.ClientId, string clientSecret = ApiConstants.ClientSecret)
        {
            var token = await GetAccessTokenForUser(username, password, clientId, clientSecret);
            client.SetBearerToken(token);
        }

        private void InitializeIdentityServer()
        {
	        var projectDir = Directory.GetCurrentDirectory();
	        var configuration = new ConfigurationBuilder()
		        .SetBasePath(projectDir)
		        .AddJsonFile("appsettings.json").Build();

	        var builder = new WebHostBuilder()
		        .UseEnvironment("Test")
		        .UseContentRoot(projectDir)
		        .UseConfiguration(configuration)
		        .ConfigureServices(
			        services =>
			        {
				        services.AddAuthentication(o =>
				        {
					        o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
					        o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				        }).AddJwtBearer(o =>
				        {
					        o.Authority = "http://localhost";
					        o.Audience = ApiConstants.ApiName;
					        o.RequireHttpsMetadata = false;
				        });
			        })
				.UseStartup<Startup>();


			_identityServer = new TestServer(builder);
            _handler = _identityServer.CreateHandler();
        }
		

        private void ConfigureIdentityServerApp(IApplicationBuilder app)
        {
            app.UseIdentityServer();
            app.UseMvc();
		}

		private void ConfigureIdentityServerServices(IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryPersistedGrants()
                .AddInMemoryIdentityResources(IdentityServer.Config.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServer.Config.GetApiResources())
                .AddInMemoryClients(IdentityServer.Config.GetClients())
                .AddTestUsers(DefaultUsers.Get());
            services.AddMvc();
        }
		
    }
}
