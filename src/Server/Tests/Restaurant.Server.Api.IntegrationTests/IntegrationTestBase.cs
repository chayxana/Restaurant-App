using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Restaurant.Common.Constants;

namespace Restaurant.Server.Api.IntegrationTests
{
    public class IntegrationTestBase : IDisposable
    {
        private readonly string _apiName = ApiConstants.ApiName;
        private const string TokenEndpoint = "http://localhost/connect/token";
        private readonly TestServer _testServer;
        protected readonly HttpClient HttpClient;
        private HttpMessageHandler _handler;

        protected IntegrationTestBase()
        {
            var projectDir = Directory.GetCurrentDirectory();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(projectDir)
                .AddJsonFile("appsettings.json").Build();

            var builder = new WebHostBuilder()
                .UseEnvironment("Test")
                .UseContentRoot(projectDir)
                .UseConfiguration(configuration)
                .UseStartup<Startup>();

            _testServer = new TestServer(builder);
            _handler = _testServer.CreateHandler();
            HttpClient = _testServer.CreateClient();
        }

        protected Task SetUpTokenFor(string username, string password)
        {
            return SetAccessToken(HttpClient, username, password);
        }

   
        private async Task<string> GetAccessTokenForUser(string userName, string password, string clientId = ApiConstants.ClientId, string clientSecret = ApiConstants.ClientSecret)
        {
            var handler = _testServer.CreateHandler();
            var discoveryClient = new DiscoveryClient("http://localhost", handler);
            var discoveryDocument = await discoveryClient.GetAsync();

            var client = new TokenClient(discoveryDocument.TokenEndpoint, clientId, clientSecret, innerHttpMessageHandler : handler);

            var response = await client.RequestResourceOwnerPasswordAsync(userName, password, _apiName);
            return response.AccessToken;
        }

        private async Task SetAccessToken(HttpClient client, string username, string password, string clientId = ApiConstants.ClientId, string clientSecret = ApiConstants.ClientSecret)
        {
            var token = await GetAccessTokenForUser(username, password, clientId, clientSecret);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public virtual void Dispose()
        {
            _testServer.Dispose();
            HttpClient?.Dispose();
        }
    }
}
