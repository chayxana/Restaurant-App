using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Restaurant.Common.Constants;
using Restaurant.Server.Api.Abstractions.Facades;
using Restaurant.Server.Api.Abstractions.Providers;
using Restaurant.Server.Api.Abstractions.Repositories;
using Restaurant.Server.Api.Constants;
using Restaurant.Server.Api.Facades;
using Restaurant.Server.Api.Mappers;
using Restaurant.Server.Api.Models;
using Restaurant.Server.Api.Providers;
using Restaurant.Server.Api.Repositories;

namespace Restaurant.Server.Api
{
	public class Startup
	{
		private readonly IConfiguration _configuration;

		public Startup(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			var connectionString = _configuration["ConnectionStrings:DefaultConnection"];
			services.AddDbContext<DatabaseContext>(options =>
				options.UseSqlServer(connectionString));

			services.AddIdentity<User, IdentityRole>(options =>
			{
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
			}).AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();


			services.AddScoped<IRepository<DailyEating>, DailyEatingRepository>();
			services.AddScoped<IRepository<Food>, FoodRepository>();
			services.AddScoped<IRepository<Category>, CategoryRepository>();
			services.AddScoped<IMapperFacade, MapperFacade>();
			services.AddScoped<IUserBootstrapper, UserBootstrapper>();
			services.AddScoped<IUserManagerFacade, UserManagerFacade>();
			services.AddSingleton<IFileUploadProvider, FileUploadProvider>();

			services.AddLogging();

			services.AddCors(o => o.AddPolicy("ServerPolicy", builder =>
			{
				builder.AllowAnyOrigin()
					   .AllowAnyMethod()
					   .AllowAnyHeader()
					   .WithExposedHeaders("WWW-Authenticate");
			}));

			services.AddMvc();

			services.AddIdentityServer()
				.AddDeveloperSigningCredential()
				.AddInMemoryIdentityResources(Config.GetIdentityResources())
				.AddInMemoryApiResources(Config.GetApiResources())
				.AddInMemoryClients(Config.GetClients())
				.AddAspNetIdentity<User>();


			services.AddAuthentication(o =>
			{
				o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

			}).AddJwtBearer(o =>
			{
				o.Authority = "http://localhost:6200";
				o.Audience = ApiConstants.ApiName;
				o.RequireHttpsMetadata = false;
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public async void Configure(IApplicationBuilder app,
			IHostingEnvironment env,
			ILoggerFactory loggerFactory,
			IConfiguration configuration)
		{
			loggerFactory.AddConsole(configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			app.UseCors("ServerPolicy");
			app.UseDeveloperExceptionPage();
			app.UseIdentityServer();
			app.UseMvc();
			app.UseStaticFiles();

			AutoMapperConfiguration.Configure();
		}
	}
}
