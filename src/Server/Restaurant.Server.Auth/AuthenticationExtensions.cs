using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
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
#if DEBUG
				o.Authority = "https://localhost:6200";
#elif RELEASE
			    o.Authority = "https://restaurantserverapi.azurewebsites.net";
#endif
				o.Audience = ApiConstants.ApiName;
				o.RequireHttpsMetadata = false;
			});
		}
	}
}
