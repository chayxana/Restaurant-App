using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using HealthChecks.UI.Client;
using Identity.API.Abstraction.Providers;
using Identity.API.Abstraction.ViewModelBuilders;
using Identity.API.Data;
using Identity.API.Middleware;
using Identity.API.Model.Entities;
using Identity.API.Providers;
using Identity.API.Utils;
using Identity.API.ViewModelBuilders;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Identity.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddControllersWithViews();
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                services.Configure<ForwardedHeadersOptions>(options =>
                {
                    options.ForwardedHeaders =
                        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                });
            });

            services.Configure<CookiePolicyOptions>(options => { options.MinimumSameSitePolicy = SameSiteMode.Lax; });

            var dbHost = Configuration["DB_HOST"];
            var dbName = Configuration["DB_NAME"];
            var dbUser = Configuration["DB_USER"];
            var dbPassword = Configuration["DB_PASSWORD"];
            var connectionString =
                $"Host={dbHost};Database={dbName};Username={dbUser};Password={dbPassword}";

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<ApplicationDbContext>(options => { options.UseNpgsql(connectionString); });

            services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddNpgSql(connectionString);

            services.AddCors(o => o.AddPolicy("ServerPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("WWW-Authenticate");
            }));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                })
                .AddDeveloperSigningCredential()
                .AddAspNetIdentity<ApplicationUser>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseNpgsql(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseNpgsql(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));

                    // options.EnableTokenCleanup = false;
                    // options.TokenCleanupInterval = 30; // interval in seconds
                });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Restaurant - Identity HTTP API",
                    Version = "v1",
                });
            });

            services.AddTransient<ILoginViewModelBuilder, LoginViewModelBuilder>();
            services.AddTransient<ILogOutViewModelBuilder, LogOutViewModelBuilder>();
            services.AddTransient<ILoggedOutViewModelBuilder, LoggedOutViewModelBuilder>();
            services.AddTransient<ILoginProvider, LoginProvider>();
            services.AddAutoMapper(typeof(Startup));
        }

        public bool IsK8S => Configuration.GetValue<string>("OrchestrationType").ToUpper().Equals("K8S");

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseForwardedHeaders();

            var basePath = Configuration.GetBasePath();
            if (!string.IsNullOrEmpty(basePath))
            {
                loggerFactory.CreateLogger<Startup>().LogDebug("Using PATH BASE '{pathBase}'", basePath);
                app.UsePathBase(basePath);
            }

            app.UseMiddleware<RequestLoggerMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    swaggerDoc.Servers = new List<OpenApiServer>
                    {
                        new OpenApiServer
                        {
                            Url = $"{httpReq.Scheme}://{httpReq.Host.Value}{basePath}"
                        }
                    };
                });
            }).UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"{basePath}/swagger/v1/swagger.json", "Identity.API V1");
                c.OAuthClientId("menu-api-swagger-ui");
                c.OAuthClientSecret("client-secret");
                c.OAuthAppName("Menu API Swagger UI");
            });

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();
            app.UseCors("ServerPolicy");
            app.UseIdentityServer();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
                {
                    Predicate = r => r.Name.Contains("self")
                });
            });
        }
    }
}