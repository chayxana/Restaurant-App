using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Menu.API.Abstraction.Repositories;
using Menu.API.Data;
using Menu.API.Models;
using Menu.API.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Steeltoe.Common.Discovery;
using Steeltoe.Discovery.Client;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;

namespace Menu.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddAuthorization();

            var identityUrl = Configuration["IdentityUrl"];

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = identityUrl;
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "menu-api";
                });

            var connectionString = Configuration.GetConnectionString("MenuDatabaseConnectionString");
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "Restaurant - Menu HTTP API",
                    Version = "v1",
                    TermsOfService = "Terms Of Service"
                });

                options.AddSecurityDefinition("oauth2", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "implicit",
                    AuthorizationUrl = $"{identityUrl}/connect/authorize",
                    TokenUrl = $"{identityUrl}/connect/token",
                    Scopes = new Dictionary<string, string>()
                    {
                        { "menu-api", "Restaurant Menu Api" }
                    }
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            services.AddScoped<IRepository<Category>, CategoryRepository>();
            services.AddScoped<IRepository<Food>, FoodRepository>();
            services.AddAutoMapper(typeof(Startup).GetTypeInfo().Assembly);
            services.AddDiscoveryClient(Configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseMvcWithDefaultRoute();
            app.UseMvc();
            app.UseDiscoveryClient();

            var logger = loggerFactory.CreateLogger("init");
            var pathBase = Configuration["PATH_BASE"];
            var routePrefix = (!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty);

            if (!string.IsNullOrEmpty(pathBase))
            {
                logger.LogDebug($"Using PATH BASE '{pathBase}'");
                app.UsePathBase(pathBase);
            }
            app.UseSwagger(c =>
            {
                if (routePrefix != string.Empty)
                {
                    c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                    {
                        swaggerDoc.Schemes = new List<string>() { httpReq.Scheme };
                        swaggerDoc.BasePath = routePrefix;
                    });
                }
            }).UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"{routePrefix}/swagger/v1/swagger.json", "Menu.API V1");
                c.OAuthClientId("menu-api-swagger-ui");
                c.OAuthAppName("Menu API Swagger UI");
            });
        }
    }
}
