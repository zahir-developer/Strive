using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;

using Strive.API.Filters;
using Strive.Common;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Strive.BusinessLogic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;

namespace Strive.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IAuthManagerBpl, AuthManagerBpl>();
            services.AddTransient<IEmployeeBpl, EmployeeBpl>();
            services.AddScoped<ITenantHelper, TenantHelper>();

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
                option.Filters.Add(typeof(StrivepayloadFilter));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            #endregion


            #region Add Swagger
            services.AddSwaggerGen(swag =>
            {
                swag.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "StriveAPI", Version = "V1" });
                swag.AddSecurityDefinition("Bearer", new Swashbuckle.AspNetCore.Swagger.ApiKeyScheme { In = "header", Name = "Authorization", Type = "apiKey" });
                swag.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> { { "Bearer", Enumerable.Empty<string>() } });
            });
            #endregion


            #region Add Authentication


            var credentials = Common.Utility.GetDecryptionStuff(Configuration.GetSection("StriveSettings:Jwt")["SecretKey"]);

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

            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IDistributedCache cache, IHostingEnvironment env, ILoggerFactory loggerfactory,
              IServiceProvider serviceProvider, IApplicationLifetime appLifetime)
        {
            loggerfactory.AddSerilog();
            app.UseDeveloperExceptionPage().UseCors("CorsPolicy");
            app.UseSwagger();
            app.UseSwaggerUI(options => { options.SwaggerEndpoint(Configuration["StriveSettings:VirtualDirectory"] + "swagger.json", "StriveAPI V1"); });
            app.UseExceptionHandler("/error");
            app.UseAuthentication();
            app.UseStatusCodePages();
            app.UseSerilogRequestLogging();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowCredentials().AllowAnyHeader());
            app.UseMvc(routes => { routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"); });
            //app.UseMultitenancy<AppTenant>
        }
    }
}
