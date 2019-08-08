using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using Identity.API.Abstraction.Providers;
using Identity.API.Abstraction.ViewModelBuilders;
using Identity.API.Data;
using Identity.API.Model.Entities;
using Identity.API.Providers;
using Identity.API.Utils;
using Identity.API.ViewModelBuilders;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Steeltoe.Discovery.Client;

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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                    ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost;

                options.ForwardedHostHeaderName = "x-forwarded-host";
                options.ForwardedProtoHeaderName = "x-forwarded-proto";

                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            var connectionString = Configuration.GetConnectionString("IdentityConnectionString");
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

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

            services.AddIdentityServer(x => x.IssuerUri = "null" )
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
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "Restaurant - Identity HTTP API",
                    Version = "v1",
                    TermsOfService = "Terms Of Service"
                });
            });

            services.AddTransient<ILoginViewModelBuilder, LoginViewModelBuilder>();
            services.AddTransient<ILogOutViewModelBuilder, LogOutViewModelBuilder>();
            services.AddTransient<ILoggedOutViewModelBuilder, LoggedOutViewModelBuilder>();
            services.AddTransient<ILoginProvider, LoginProvider>();
            services.AddAutoMapper(typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseForwardedHeaders();
            app.Use(async (context, next) =>
            {
                var _logger = loggerFactory.CreateLogger("init");
                // Request method, scheme, and path
                _logger.LogDebug("Request Method: {METHOD}", context.Request.Method);
                _logger.LogDebug("Request Scheme: {SCHEME}", context.Request.Scheme);
                _logger.LogDebug("Request Path: {PATH}", context.Request.Path);
                _logger.LogDebug("Base Path: {PATHBASE}", context.Request.PathBase);
                _logger.LogDebug("Host: {HOST}", context.Request.Host.Value);

                // Headers
                foreach (var header in context.Request.Headers)
                {
                    _logger.LogDebug("Header: {KEY}: {VALUE}", header.Key, header.Value);
                }

                // Connection: RemoteIp
                _logger.LogDebug("Request RemoteIp: {REMOTE_IP_ADDRESS}", context.Connection.RemoteIpAddress);

                await next();
            });

            app.Use((context, next) =>
            {
                if (context.Request.Headers.TryGetValue("x-forwarded-prefix", out var prefix))
                {
                    context.Request.PathBase = new PathString(prefix);
                }
                return next();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("ServerPolicy");
            app.UseIdentityServer();


            var basePath = Configuration.GetBasePath();
            app.UseSwagger(c =>
            {
                if (basePath != string.Empty)
                {
                    c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                    {
                        swaggerDoc.Schemes = new List<string>() { httpReq.Scheme };
                        swaggerDoc.BasePath = basePath;
                    });
                }
            }).UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"{basePath}/swagger/v1/swagger.json", "Identity.API V1");
            });

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseMvcWithDefaultRoute();
        }
    }
}
