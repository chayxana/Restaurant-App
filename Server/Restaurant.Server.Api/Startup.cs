using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Restaurant.Common.Constants;
using Restaurant.Server.Abstraction.Facades;
using Restaurant.Server.Abstraction.Providers;
using Restaurant.Server.Abstraction.Repositories;
using Restaurant.Server.Auth;
using Restaurant.Server.Core.Facades;
using Restaurant.Server.Core.Mappers;
using Restaurant.Server.Core.Providers;
using Restaurant.Server.DataProvider;
using Restaurant.Server.DataProvider.Repositories;
using Restaurant.Server.Models;

namespace Restaurant.Server.Api
{
    [ExcludeFromCodeCoverage]
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

            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString,
                b => b.MigrationsAssembly("Restaurant.Server.Api")));

            //services.AddDbContext<DatabaseContext>(opt => opt.UseNpgsql(connectionString, 
            //    b => b.MigrationsAssembly("Restaurant.Server.Api")));

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();


            services.AddScoped<IRepository<DailyEating>, DailyEatingRepository>();
            services.AddScoped<IRepository<Food>, FoodRepository>();
            services.AddScoped<IRepository<Category>, CategoryRepository>();
            services.AddScoped<IRepository<Order>, OrderRepository>();
            services.AddScoped<IMapperFacade, MapperFacade>();
            services.AddScoped<IUserBootstrapper, UserBootstrapper>();
            services.AddScoped<IUserManagerFacade, UserManagerFacade>();

            services.AddSingleton<IFileInfoFacade, FileInfoFacade>();
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
#if DEBUG
                o.Authority = "http://localhost:6200";
#elif RELEASE
				o.Authority = "https://restaurantserverapi.azurewebsites.net";
#endif

                o.Audience = ApiConstants.ApiName;
                o.RequireHttpsMetadata = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            IConfiguration configuration,
            IUserBootstrapper userBootstrapper)
        {
            loggerFactory.AddConsole(configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors("ServerPolicy");
            app.UseDeveloperExceptionPage();
            app.UseIdentityServer();
            app.UseMvc();
            app.UseStaticFiles();

            AutoMapperConfiguration.Configure();
            userBootstrapper.CreateDefaultUsersAndRoles().Wait();
        }
    }
}