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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Admin.API.Filters;
using Strive.BusinessLogic;
using Strive.Common;

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


            var credentials = Strive.Common.Utility.GetDecryptionStuff(Configuration.GetSection("StriveAdminSettings:Jwt")["SecretKey"]);

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


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            //app.UseSwaggerUI(options => { options.SwaggerEndpoint(Configuration["StriveAdminSettings:VirtualDirectory"] + "swagger.json", "Strive-Admin - v1"); });
            app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "StriveAdminApi"); });
            app.UseExceptionHandler("/error");
            app.UseAuthentication();
            app.UseStatusCodePages();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowCredentials().AllowAnyHeader());
            app.UseMvc(routes => { routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"); });

        }
    }
}
