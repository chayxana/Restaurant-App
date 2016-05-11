using Refit;
using Restaurant.Model;
using System.Net.Http;

namespace Restaurant.ViewModels
{
    internal abstract class Global
    {
        private static AuthenticationManager _authenticationManager;
        public static AuthenticationManager AuthenticationManager
        {
            get
            {
                if (_authenticationManager == null)
                {
                    _authenticationManager = new AuthenticationManager();
                }
                return _authenticationManager;
            }
        }
    }

    internal class AuthenticationManager
    {
        public HttpClient AuthenticatedClient { get; set; }

        private IRestaurantApi _authenticatedApi;
        public IRestaurantApi AuthenticatedApi
        {
            get
            {
                if (_authenticatedApi == null)
                {
                    _authenticatedApi = RestService.For<IRestaurantApi>(AuthenticatedClient);
                }
                return _authenticatedApi;
            }
        }
    }
}
