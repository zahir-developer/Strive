using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Admin.API.Filters;
using Strive.BusinessLogic;
using Strive.BusinessLogic.Auth;
using Strive.Common;
using Strive.BusinessLogic.ServiceSetup;
using Strive.BusinessLogic.CashRegister;
using Strive.BusinessLogic.Common;
using Strive.BusinessLogic.Location;
using Strive.BusinessLogic.Collision;
using Strive.BusinessLogic.Client;
using OwaspHeaders.Core.Extensions;
using OwaspHeaders.Core.Models;
using Microsoft.Extensions.Options;
using Strive.Crypto;
using Strive.BusinessLogic.Document;
using Strive.BusinessLogic.Weather;
using Strive.BusinessLogic.MembershipSetup;
using Strive.BusinessLogic.Vehicle;
using Strive.BusinessLogic.TimeClock;

namespace Admin.API
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
            services.AddTransient<IAuthManagerBpl, AuthManagerBpl>();
            services.AddTransient<ILocationBpl, LocationBpl>();
            services.AddTransient<IEmployeeBpl, EmployeeBpl>();
            services.AddTransient<IProductBpl, ProductBpl>();
            services.AddTransient<ILocationBpl, LocationBpl>();
            services.AddTransient<ICommonBpl, CommonBpl>();
            services.AddTransient<IDocumentBpl, DocumentBpl>();
            //services.AddTransient<IMembershipBpl, MembershipBpl>();
            services.AddTransient<ICollisionBpl, CollisionBpl>();
            services.AddTransient<ICashRegisterBpl, CashRegisterBpl>();
            services.AddTransient<IVendorBpl, VendorBpl>();
            services.AddTransient<IServiceSetupBpl, ServiceSetupBpl>();
            services.AddTransient<ICashRegisterBpl, CashRegisterBpl>();
            services.AddTransient<IClientBpl, ClientBpl>();
            services.AddTransient<IWeatherBpl, WeatherBpl>();
            services.AddTransient<IVendorBpl,VendorBpl>();
            services.AddTransient<IVehicleBpl,VehicleBpl>();
            services.AddTransient<ITimeClockBpl, TimeClockBpl>();

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
                option.Filters.Add(typeof(AdminPayloadFilter));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            #endregion


            #region Add Swagger
            services.AddSwaggerGen(swag =>
            {
                // swag.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "StriveAdminApi", Version = "v1" });
                swag.AddSecurityDefinition("Bearer", new Swashbuckle.AspNetCore.Swagger.ApiKeyScheme { In = "header", Name = "Authorization", Type = "apiKey" });
                swag.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> { { "Bearer", Enumerable.Empty<string>() } });
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


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
    IOptions<SecureHeadersMiddlewareConfiguration> secureHeaderSettings)
        {
            app.UseSwagger();
            //app.UseSwaggerUI(options => { options.SwaggerEndpoint(Configuration["StriveAdminSettings:VirtualDirectory"] + "swagger.json", "Strive-Admin - v1"); });
            app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "StriveAdminApi"); });
            app.UseExceptionHandler("/error");
            app.UseAuthentication();
            app.UseStatusCodePages();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowCredentials().AllowAnyHeader());
            //app.UseSecureHeadersMiddleware(CustomSecureHeaderExtensions.CustomConfiguration());
            //app.UseSecureHeadersMiddleware(secureHeaderSettings.Value); 
            //app.UseSecureHeadersMiddleware(SecureHeadersMiddlewareExtensions.BuildDefaultConfiguration());
            app.UseSecureHeadersMiddleware(Middleware.SecureHeaders.CustomConfiguration());
            app.UseMvc(routes => { routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"); });

        }
    }
}
