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
using MMLib.SwaggerForOcelot;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Eureka;
using Serilog;

namespace StriveGateway.API
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

            ////services.AddOcelot().AddEureka();
            //services.AddSwaggerForOcelot(Configuration);

            ////services.AddOcelot(Configuration);

            services.AddSwaggerForOcelot(Configuration);
            //services.AddOcelot(Configuration);


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            


            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();
            }));

          

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer("ApiSecurity", x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    //TokenDecryptionKey = credentials.DecryptKey,
                    //IssuerSigningKey = credentials.SignKey,
                    ClockSkew = TimeSpan.Zero
                };

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerfactory)
        {
            loggerfactory.AddSerilog();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseSwagger();
            //app.UseSwaggerUI(options => { options.SwaggerEndpoint(Configuration["StriveSettings:VirtualDirectory"] + "swagger.json", "StriveAPI V1"); });


            //app.UseSwaggerForOcelotUI(Configuration);//, opt => { opt.PathToSwaggerGenerator = "/swagger/docs"; });

            app.UseSwaggerForOcelotUI(Configuration, opt => { opt.PathToSwaggerGenerator = "/swagger/docs"; });

            //app.UseSwaggerForOcelotUI(opt => { opts.DownstreamSwaggerHeaders = new[] { new KeyValuePair<string, string>("Auth-Key", "AuthValue"), }; })

            app.UseMvc();
           


            app.UseOcelot().GetAwaiter().GetResult();
        }
    }
}
