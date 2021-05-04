using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OwaspHeaders.Core.Extensions;
using OwaspHeaders.Core.Models;
using Strive.BusinessLogic.SuperAdmin.Tenant;
using Strive.Common;
using Strive.Crypto;
using SuperAdmin.API.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SuperAdmin.API
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
            services.AddScoped<ITenantHelper, TenantHelper>();           
            services.AddTransient<TenantBpl, TenantBpl>();

            #region Add CORS
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();
            }));
            #endregion

            #region Add MVC
            services.AddMvc(option =>
            {
                option.EnableEndpointRouting = false;
                option.Filters.Add(typeof(SuperAdminFilter));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            #endregion

            #region Add Swagger
            services.AddSwaggerGen(swag =>
            {
                swag.AddSecurityDefinition("Bearer", new Swashbuckle.AspNetCore.Swagger.ApiKeyScheme { In = "header", Name = "Authorization", Type = "apiKey" });
                swag.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> { { "Bearer", Enumerable.Empty<string>() } });
                swag.ResolveConflictingActions(apiDesc => apiDesc.First());

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                swag.IncludeXmlComments(xmlPath);

            });
            #endregion

            #region Add Authentication

            var credentials = new Crypt().GetDecryptionStuff(Configuration.GetSection("StriveAdminSettings:Jwt")["SecretKey"]);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    TokenDecryptionKey = credentials.DecryptKey,
                    IssuerSigningKey = credentials.SignKey,
                    ClockSkew = TimeSpan.Zero
                };
                x.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }

                        return Task.CompletedTask;
                    }
                };

            });
            #endregion

            #region Anti Forgery Token
            services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");
            #endregion

            services.AddOptions();
            services.Configure<SecureHeadersMiddlewareConfiguration>(Configuration.GetSection("SecureHeadersMiddlewareConfiguration"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwagger();


        }

        public static SecureHeadersMiddlewareConfiguration CustomConfiguration()
        {
            return SecureHeadersMiddlewareBuilder
                .CreateBuilder()
                .UseHsts(1200, false)
                .UseXSSProtection(OwaspHeaders.Core.Enums.XssMode.oneReport, "https://reporturi.com/some-report-url")
                .UseContentDefaultSecurityPolicy()
                .UsePermittedCrossDomainPolicies(OwaspHeaders.Core.Enums.XPermittedCrossDomainOptionValue.masterOnly)
                .UseReferrerPolicy(OwaspHeaders.Core.Enums.ReferrerPolicyOptions.sameOrigin)
                .Build();
        }


        ///... This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
    IOptions<SecureHeadersMiddlewareConfiguration> secureHeaderSettings, IAntiforgery antiforgery)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "StriveAdminApi"); });
            app.UseExceptionHandler("/error");
            app.UseAuthentication();
            app.UseStatusCodePages();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowCredentials().AllowAnyHeader());
            app.UseSecureHeadersMiddleware(Middleware.SecureHeaders.CustomConfiguration());
            app.UseMvc(routes => { routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"); });

            app.Use(next => context =>
            {
                if (context.Request.Path == "/")
                {
                    ///... send the request token as a JavaScript-readable cookie, and Angular will use it by default
                    var tokens = antiforgery.GetAndStoreTokens(context);
                    context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken, new CookieOptions { HttpOnly = false, Secure = false });
                }
                return next(context);
            });
        }
    }
}
