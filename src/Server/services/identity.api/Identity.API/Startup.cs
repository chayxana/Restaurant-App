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


            services.AddIdentityServer(s => s.IssuerUri = "http://demo.restaurant-identity")
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
            services.AddDiscoveryClient(Configuration);
            services.AddAutoMapper(typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("ServerPolicy");
            app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseMvcWithDefaultRoute();
            app.UseHttpsRedirection();
            app.UseDiscoveryClient();

            var (hasBasePath, basePath) =  Configuration.BasePath();
            if (hasBasePath)
            {
                loggerFactory.CreateLogger("init").LogDebug($"Using PATH BASE '{basePath}'");
                app.UsePathBase(basePath);
            }

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
        }
    }
}
