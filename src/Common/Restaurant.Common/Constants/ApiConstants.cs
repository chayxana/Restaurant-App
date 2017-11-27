namespace Restaurant.Common.Constants
{
	public struct ApiConstants
	{
		public const string ApiName = "api1";
		public const string ClientId = "user_password.client";
		public const string ClientSecret = "secret";
		public const string GrantType = "password";
		public const int TokenLifeTime = 3600;

		private const string AzureClientUrl = "https://restaurantserverapi.azurewebsites.net/";
		private const string ApiClientUrlForAndroidEmulator = "https://10.0.2.2:6200/";
		public static string ApiClientUrl => ApiClientUrlForAndroidEmulator;
	}
}