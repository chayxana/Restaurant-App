using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Restaurant.Server.Abstractions.Facades;
using Restaurant.Server.Abstractions.Repositories;
using Restaurant.Server.Constants;
using Restaurant.Server.Facades;
using Restaurant.Server.Mappers;
using Restaurant.Server.Models;
using Restaurant.Server.Repositories;

namespace Restaurant.Server
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
            services.AddScoped<IMapperFacade, MapperFacade>();
            services.AddLogging();

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
