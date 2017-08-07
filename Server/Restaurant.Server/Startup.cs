using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Restaurant.Server.Api.Abstractions.Facades;
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
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();

            services.AddScoped<IRepository<DailyEating>, DailyEatingRepository>();
            services.AddScoped<IRepository<Food>, FoodRepository>();
            services.AddScoped<IRepository<Category>, CategoryRepository>();
			services.AddScoped<IMapperFacade, MapperFacade>();
	        services.AddSingleton<IFileUploadProvider, FileUploadProvider>();
            services.AddLogging();


			services.AddCors(o => o.AddPolicy("ServerPolicy", builder =>
			{
				builder.AllowAnyOrigin()
					   .AllowAnyMethod()
					   .AllowAnyHeader();
			}));

			services.AddMvc();

            services.AddIdentityServer()
                .AddTemporarySigningCredential()
                .AddInMemoryPersistedGrants()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddAspNetIdentity<User>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
			app.UseCors("ServerPolicy");

			app.UseIdentity()
               .UseIdentityServer()
               .UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
               {
                   Authority = "http://localhost:62798",
                   RequireHttpsMetadata = false,
                   ApiName = "api1",
                   RoleClaimType = "role",
                   NameClaimType = "name",
               });

			app.UseMvc();
			app.UseStaticFiles();
			

            AutoMapperConfiguration.Configure();
            await CreateRoles(app.ApplicationServices.GetService<IServiceProvider>());
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            string[] roleNames = { "Admin", "Member" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var admin = new User
            {
                UserName = Configuration["AppSettings:AdminUserName"],
                Email = Configuration["AppSettings:AdminEmail"],
            };

            //Ensure you have these values in your appsettings.json file
            string password = Configuration["AppSettings:AdminPassword"];
            var user = await userManager.FindByEmailAsync(Configuration["AppSettings:AdminEmail"]);

            if (user == null)
            {
                var createPowerUser = await userManager.CreateAsync(admin, password);
                if (createPowerUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
