namespace Restaurant.Common.Constants
{
    public static class ApiConstants
    {
        public const string ApiName = "api1";
        public const string ClientId = "user_password.client";
        public const string ClientSecret = "secret";
        public const string GrantType = "password";
        private const string AzureClientUrl = "https://restaurantserverapi.azurewebsites.net/";
	    private const string ApiClientUrlForAndroidEmulator = "https://10.0.2.2:6200/";

	    public static string ApiClientUrl => ApiClientUrlForAndroidEmulator;
    }
}