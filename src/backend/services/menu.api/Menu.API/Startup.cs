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
using HealthChecks.UI.Client;
using IdentityModel;
using Menu.API.Abstraction.Facades;
using Menu.API.Abstraction.Managers;
using Menu.API.Abstraction.Repositories;
using Menu.API.Abstraction.Services;
using Menu.API.Data;
using Menu.API.Facades;
using Menu.API.Managers;
using Menu.API.Models;
using Menu.API.Providers;
using Menu.API.Repositories;
using Menu.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Menu.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public bool IsK8S => Configuration.GetValue<string>("ORCHESTRATION_TYPE").ToUpper().Equals("K8S");
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                if (IsK8S)
                {
                    options.ForwardedHeaders =
                        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                }
                else
                {
                    options.ForwardedHeaders =
                        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost;

                    options.ForwardedHostHeaderName = "x-forwarded-host";
                    options.ForwardedProtoHeaderName = "x-forwarded-proto";
                    options.KnownNetworks.Clear();
                    options.KnownProxies.Clear();
                }
            });
            services.AddAuthorization();

            var authAuthority = Configuration["AUTH_AUTHORITY"];
            var authUrl = Configuration["AUTH_URL"];

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = authAuthority;
                options.RequireHttpsMetadata = false;
                options.Audience = "menu-api";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = JwtClaimTypes.Name,
                    RoleClaimType = JwtClaimTypes.Role,
                    ValidateIssuer = false
                };
            });
            var dbHost = Configuration["DB_HOST"];
            var dbName = Configuration["DB_NAME"];
            var dbUser = Configuration["DB_USER"];
            var dbPassword = Configuration["DB_PASSWORD"];
            var connectionString = 
                $"Host={dbHost};Database={dbName};Username={dbUser};Password={dbPassword}";

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
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Restaurant - Menu HTTP API",
                    Version = "v1",
                });

                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows()
                    {
                        Implicit = new OpenApiOAuthFlow()
                        {
                            AuthorizationUrl = new Uri($"{authUrl}/connect/authorize"),
                            TokenUrl = new Uri($"{authUrl}/connect/token"),
                            Scopes = new Dictionary<string, string>()
                            {
                                { "menu-api", "Restaurant Menu Api" }
                            }
                        }
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
            services.AddScoped<ICurrencyProvider, CurrencyProvider>();
            services.AddAutoMapper(typeof(Startup).GetTypeInfo().Assembly);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseForwardedHeaders();
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

            if (IsK8S)
            {
                logger.LogDebug($"Using PATH BASE '{pathBase}'");
                app.UsePathBase(pathBase);
            }
            else
            {
                app.Use((context, next) =>
                {
                    if (context.Request.Headers.TryGetValue("x-forwarded-prefix", out var prefix))
                    {
                        loggerFactory.CreateLogger<Startup>().LogDebug("Using x-forwarded-prefix as a PREFIX '{pathBase}'", prefix);
                        context.Request.PathBase = new PathString(prefix);
                    }
                    return next();
                });
            }
            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    swaggerDoc.Servers = new List<OpenApiServer>
                    {
                        new OpenApiServer
                        {
                            Url = $"{httpReq.Scheme}://{httpReq.Host.Value}{routePrefix}"
                        }
                    };
                });
            }).UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"{routePrefix}/swagger/v1/swagger.json", "Menu.API V1");
                c.OAuthClientId("menu-api-swagger-ui");
                c.OAuthAppName("Menu API Swagger UI");
            });

            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors("ServerPolicy");
            app.UseAuthentication();
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
