using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Identity.API.IntegrationTests.Config;
using IdentityModel.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Restaurant.Common.Constants;
using Restaurant.Server.Api.IntegrationTests.Config;

namespace Identity.API.IntegrationTests
{
	public class IntegrationTestBase
	{
		protected HttpClient HttpClient;
		private TestServer _testServer;
		private IdentityServerSetup _identityServerSetup;
		public HttpMessageHandler _handler;

		protected IntegrationTestBase()
		{
			InitializeIdentityServer();
			InitializeClient();
		}


		private void InitializeClient()
		{
			var projectDir = Directory.GetCurrentDirectory();
			var configuration = new ConfigurationBuilder()
				.SetBasePath(projectDir).Build();

			var builder = new WebHostBuilder()
				.UseEnvironment("Test")
				.UseContentRoot(projectDir)
				.UseConfiguration(configuration)
				.UseStartup<Startup>();

			_testServer = new TestServer(builder);
			_handler = _testServer.CreateHandler();
			HttpClient = _testServer.CreateClient();
		}


		private void InitializeIdentityServer()
		{
			_identityServerSetup = new IdentityServerSetup()
				.Initialize();
		}

		protected Task SetUpTokenFor(string username, string password)
		{
			return _identityServerSetup.SetAccessToken(HttpClient, username, password);
		}

		public async Task<string> GetAccessTokenForUser(string userName, string password, string clientId = ApiConstants.ClientId, string clientSecret = ApiConstants.ClientSecret)
		{
			var client = new TokenClient("https://localhost:6200/connect/token", clientId, clientSecret);

			var response = await client.RequestResourceOwnerPasswordAsync(userName, password, ApiConstants.ApiName);
			return response.AccessToken;
		}

		public async Task SetAccessToken(string username, string password, string clientId = ApiConstants.ClientId, string clientSecret = ApiConstants.ClientSecret)
		{
			var token = await GetAccessTokenForUser(username, password, clientId, clientSecret);
			HttpClient.SetBearerToken(token);
		}
	}
}
