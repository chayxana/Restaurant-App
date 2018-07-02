using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Restaurant.Server.Api.Abstraction.Facades;
using Restaurant.Server.Api.Abstraction.Providers;
using Restaurant.Server.Api.Abstraction.Repositories;
using Restaurant.Server.Api.Data;
using Restaurant.Server.Api.Facades;
using Restaurant.Server.Api.IdentityServer;
using Restaurant.Server.Api.Mappers;
using Restaurant.Server.Api.Models;
using Restaurant.Server.Api.Providers;
using Restaurant.Server.Api.Repositories;
using Swashbuckle.AspNetCore.Swagger;

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

            services.AddDbContext<RestaurantDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IRepository<DailyEating>, DailyEatingRepository>();
            services.AddScoped<IRepository<Food>, FoodRepository>();
            services.AddScoped<IRepository<Category>, CategoryRepository>();
            services.AddScoped<IRepository<Order>, OrderRepository>();
            services.AddScoped<IMapperFacade, MapperFacade>();
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

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<RestaurantDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddTestUsers(DefaultUsers.Get())
                .AddAspNetIdentity<User>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Restaurant HTTP API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app,
            ILoggerFactory loggerFactory,
            IConfiguration configuration,
            IHostingEnvironment env)
        {
            loggerFactory.AddConsole(configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurant API");
                c.RoutePrefix = string.Empty;
            });

            app.UseCors("ServerPolicy");
            app.UseIdentityServer();
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseStaticFiles();

            new AutoMapperConfiguration().Configure();
        }
    }
}