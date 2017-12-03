using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Restaurant.Common.Constants;

namespace Restaurant.Server.Auth
{
	public static class AuthenticationExtensions
	{
		public static void SetupAuthentication(this IServiceCollection services)
		{
			services.AddAuthentication(o =>
			{
				o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(o =>
			{
				o.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateAudience = false,
					ValidateIssuer = false
				};
#if DEBUG
				o.Authority = "http://localhost:5000";
#elif RELEASE
			    o.Authority = "https://restaurantserverapi.azurewebsites.net";
#endif
				o.Audience = ApiConstants.ApiName;
				o.RequireHttpsMetadata = false;
			}).AddGoogle(o =>
			{
				o.ClientId = "888429033582-bs4fs5gdml47pibjrmgojc968bpaj4qs.apps.googleusercontent.com";
				o.ClientSecret = "UsYgfgIOn2FC0QinvxUtq8ol";
			});
		}
	}
}
