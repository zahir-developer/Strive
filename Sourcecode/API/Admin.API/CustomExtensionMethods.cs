using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Admin.API
{
    public static class CustomExtensionMethods
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", getApiInfo());

                // Set the comments path for the Swagger JSON and UI.
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //options.IncludeXmlComments(xmlPath);
            });

            return services;

            Info getApiInfo()
            {
                return new Info
                {
                    // values of Version and Title must be identical to the ones in API gateway otherwise 'No operations defined in spec'  occurs 
                    Title = "StriveAdminApi",
                    Version = "v1",
                    Description = "Strive Admin Api"
                    //,TermsOfService = "None",
                    //Contact = new Contact
                    //{
                    //    Name = "Development Team",
                    //    Email = string.Empty,
                    //    Url = "https://dev.example.com"
                    //},
                    //License = new License
                    //{
                    //    Name = "Use under Example",
                    //    Url = "https://www.example.com/license"
                    //}
                };
            }
        }
    }
}
