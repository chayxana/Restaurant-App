using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using AutoMapper;
using IdentityModel;
using Menu.API.Abstraction.Facades;
using Menu.API.Abstraction.Managers;
using Menu.API.Abstraction.Repositories;
using Menu.API.Abstraction.Services;
using Menu.API.Data;
using Menu.API.Facades;
using Menu.API.Managers;
using Menu.API.Models;
using Menu.API.Repositories;
using Menu.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            services.AddAuthorization();

            var identityUrl = Configuration["IDENTITY_URL"];
            var publicIdentityUrl = Configuration["IDENTITY_URL_PUB"];

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = identityUrl;
                options.RequireHttpsMetadata = false;
                options.Audience = "menu-api";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = JwtClaimTypes.Name,
                    RoleClaimType = JwtClaimTypes.Role,
                    ValidateIssuer = false
                };
            });

            var connectionString = Configuration.GetConnectionString("MenuDatabaseConnectionString");
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            services.AddHealthChecks()
                .AddNpgSql(connectionString);

            services.AddCors(o => o.AddPolicy("ServerPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("WWW-Authenticate");
            }));

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
                    AuthorizationUrl = $"{publicIdentityUrl}/connect/authorize",
                    TokenUrl = $"{publicIdentityUrl}/connect/token",
                    Scopes = new Dictionary<string, string>()
                    {
                        { "menu-api", "Restaurant Menu Api" }
                    }
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            services.AddScoped<IAmazonS3>(provider =>
            {
                var configuration = provider.GetService<IConfiguration>();
                var accessKeyId = "";
                var secretKey = "";
                var amazonInstance = new AmazonS3Client(accessKeyId, secretKey, RegionEndpoint.EUCentral1);
                return amazonInstance;
            });

            services.AddScoped<IFileUploadManager, LocalFileUploadManager>();
            services.AddScoped<IFileInfoFacade, FileInfoFacade>();
            services.AddScoped<IRepository<Category>, CategoryRepository>();
            services.AddScoped<IRepository<Food>, FoodRepository>();
            services.AddScoped<IRepository<FoodPicture>, PictureRepository>();
            services.AddScoped<IFoodPictureService, FoodPictureService>();
            services.AddAutoMapper(typeof(Startup).GetTypeInfo().Assembly);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseForwardedHeaders();

            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

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
            app.UseHealthChecks("/hc");
            app.UseHealthChecks("/liveness", new HealthCheckOptions
            {
                Predicate = r => r.Name.Contains("self")
            });
            app.UseCors("ServerPolicy");
            app.UseMvcWithDefaultRoute();
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
