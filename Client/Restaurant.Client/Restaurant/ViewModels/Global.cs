using Refit;
using Restaurant.Model;
using System.Net.Http;

namespace Restaurant.ViewModels
{
    internal class Global
    {
        public static HttpClient AuthenticatedClient { get; set; }

        private static IRestaurantApi _authenticatedApi;
        public static IRestaurantApi AuthenticatedApi
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
