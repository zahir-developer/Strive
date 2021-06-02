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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Antiforgery;
using Strive.BusinessLogic.Vehicle;
using Strive.BusinessLogic.TimeClock;
using Strive.BusinessLogic.GiftCard;
using System.Reflection;
using System.IO;
using Strive.BusinessLogic.MembershipSetup;
using Strive.BusinessEntities.Model;
using Strive.BusinessLogic.Schedule;
using Strive.BusinessLogic.Washes;
using Strive.BusinessLogic.Details;
using Strive.BusinessLogic.Sales;
using Strive.BusinessLogic.Admin.ExternalApi;
using Strive.BusinessLogic.PayRoll;
using Strive.BusinessLogic.Messenger;
using Strive.BusinessLogic.WhiteLabelling;
using Strive.BusinessLogic.Checkout;
using Strive.BusinessLogic.MonthlySalesReport;
using Strive.BusinessLogic.DashboardStatistics;
using Strive.BusinessLogic.Checklist;
using Strive.BusinessLogic.BonusSetup;
using Strive.BusinessLogic.AdSetup;
using Strive.BusinessLogic.DealSetup;
using Strive.BusinessLogic.PaymentGateway;
using Strive.BusinessLogic.SuperAdmin.Tenant;
using Microsoft.Extensions.Logging;
using Strive.BusinessLogic.Logger;
using Microsoft.AspNetCore.Mvc.Formatters;
using Serilog;
using Serilog.AspNetCore;

namespace Admin.API
{
    public class Startup
    {
        private readonly Microsoft.Extensions.Logging.ILogger _logger;
        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            _logger = logger;
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
            services.AddTransient<IMembershipBpl, MembershipBpl>();
            services.AddTransient<ICollisionBpl, CollisionBpl>();
            services.AddTransient<IWashesBpl, WashesBpl>();
            services.AddTransient<ICashRegisterBpl, CashRegisterBpl>();
            services.AddTransient<IVendorBpl, VendorBpl>();
            services.AddTransient<IServiceSetupBpl, ServiceSetupBpl>();
            services.AddTransient<ICashRegisterBpl, CashRegisterBpl>();
            services.AddTransient<IClientBpl, ClientBpl>();
            services.AddTransient<IGiftCardBpl, GiftCardBpl>();
            services.AddTransient<IWeatherBpl, WeatherBpl>();
            services.AddTransient<IVendorBpl,VendorBpl>();
            services.AddTransient<IVehicleBpl,VehicleBpl>();
            services.AddTransient<ITimeClockBpl, TimeClockBpl>();
            services.AddTransient<IDetailsBpl, DetailsBpl>();
            services.AddTransient<IScheduleBpl, ScheduleBpl>();
            services.AddTransient<ISalesBpl, SalesBpl>();
            services.AddTransient<IExternalApiBpl, ExternalApiBpl>();
            services.AddTransient<IPayRollBpl,PayRollBpl>();
            services.AddTransient<IMessengerBpl, MessengerBpl>();
            services.AddTransient<IWhiteLabelBpl, WhiteLabelBpl>();
            services.AddTransient<ICheckoutBpl, CheckoutBpl>();
            services.AddTransient<IReportBpl, ReportBpl>();
            services.AddTransient<IDashboardBpl, DashboardBpl>();
            services.AddTransient<IChecklistBpl, ChecklistBpl>();
            services.AddTransient<IBonusSetupBpl, BonusSetupBpl>();
            services.AddTransient<IAdSetupBpl, AdSetupBpl>();
            services.AddTransient<IdealSetupBpl, DealSetupBpl>();
            services.AddTransient<IPaymentGatewayBpl, PaymentGatewayBpl>();
            services.AddTransient<ITenantBpl, TenantBpl>();
            services.AddTransient<ILogBpl, LogBpl>();

            Serilog.Log.Logger = new LoggerConfiguration()
           .MinimumLevel.Information()
           //.MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Verbose)
           .MinimumLevel.Override("Microsoft",(Serilog.Events.LogEventLevel)Convert.ToInt32(Configuration.GetSection("StriveAdminSettings:LogEvent")["Level"]))
           .Enrich.FromLogContext()
           .WriteTo.File(Configuration.GetSection("StriveAdminSettings:Logs")["Error"] + "Errorlog.txt")
           .CreateLogger();

            Serilog.Log.Information("Strive Starting host");

            services.AddSingleton<ILoggerFactory>(new SerilogLoggerFactory(Serilog.Log.Logger, false));

            services.AddMvcCore(
            opt =>  // or AddMvc()
            {
                    // remove formatter that turns nulls into 204 - No Content responses
                    // this formatter breaks Angular's Http response JSON parsing
                    opt.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
            });

            _logger.LogInformation("Test log Strive");

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
                swag.ResolveConflictingActions(apiDesc => apiDesc.First());
                //swag.OperationFilter<ApplySwaggerOperationSummaryFilterAttributes>();

                // Set the comments path for the Swagger JSON and UI.
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

            services.AddSignalR();
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
    IOptions<SecureHeadersMiddlewareConfiguration> secureHeaderSettings, IAntiforgery antiforgery, ILoggerFactory logger)
        {
            app.UseSwagger();
            //app.UseSwaggerUI(options => { options.SwaggerEndpoint(Configuration["StriveAdminSettings:VirtualDirectory"] + "swagger.json", "Strive-Admin - v1"); });
            app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "StriveAdminApi"); });

            var fileExists = Directory.Exists(Configuration["StriveAdminSettings:Logs:Error"]);

            logger.AddFile(Configuration["StriveAdminSettings:Logs:Error"] + "mylog-{Date}.txt");

           
            app.UseExceptionHandler("/error");
            app.UseAuthentication();
            app.UseStatusCodePages();
            app.UseCors(builder => builder.WithOrigins("http://14.141.185.75:5000","http://14.141.185.75:5003","http://localhost:4200", "http://localhost:4300", "http://40.114.79.101:5003", "http://40.114.79.101:5000").AllowAnyMethod().AllowCredentials().AllowAnyHeader());
            //app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            // global cors policy
            //app.UseCors(x => x
            //    .AllowAnyMethod()
            //    .AllowAnyHeader()
            //    .SetIsOriginAllowed(origin => true) // allow any origin
            //    .AllowCredentials()); // allow credentials

            //app.UseSecureHeadersMiddleware(CustomSecureHeaderExtensions.CustomConfiguration());
            //app.UseSecureHeadersMiddleware(secureHeaderSettings.Value); 
            //app.UseSecureHeadersMiddleware(SecureHeadersMiddlewareExtensions.BuildDefaultConfiguration());
            app.UseSecureHeadersMiddleware(Middleware.SecureHeaders.CustomConfiguration());

            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatMessageHub>("/chatMessageHub");
            });

            app.UseMvc(routes => { routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"); });

            app.Use(next => context =>
            {
                if (context.Request.Path == "/")
                {
                    //send the request token as a JavaScript-readable cookie, and Angular will use it by default
                    var tokens = antiforgery.GetAndStoreTokens(context);
                    context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken, new CookieOptions { HttpOnly = false, Secure = false });
                }
                return next(context);
            });

        }
    }
}
