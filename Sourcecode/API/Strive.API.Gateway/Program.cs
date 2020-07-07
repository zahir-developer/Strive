using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Serilog;
using Serilog.Events;

namespace StriveGateway.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config
                .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile("ocelot.json", false, false)

                .AddEnvironmentVariables();
            })
            .ConfigureLogging((hostingContext, logging) =>
            {
                //add your logging
                logging.AddSerilog();
            })
            .ConfigureServices(s =>
            {
                //s.AddOcelot().AddEureka().AddCacheManager(x => x.WithDictionaryHandle());
                s.AddOcelot().AddCacheManager(x => x.WithDictionaryHandle());
            })
            .Configure(a =>
            {
                a.UseOcelot().Wait();
            })
            .UseStartup<Startup>()
            .UseSerilog((_, config) =>
            {
                config
                    .MinimumLevel.Information()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .Enrich.FromLogContext()
                    .WriteTo.File(@"Logs\log.txt", rollingInterval: RollingInterval.Day);
            });
                    //.Configure(app =>
                    //{
                    //    app.UseMiddleware<RequestResponseLoggingMiddleware>();
                    //    app.UseOcelot().Wait();
                    //});


    }
}
