using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Strive.BusinessLogic;
using Strive.BusinessLogic.Client;
using Strive.Common;
using Microsoft.Extensions.Caching.Distributed;

namespace StriveEmailScheduler
{
    class Program
    {
        public static void Main(string[] args)
        {
             //  IDistributedCache _cache;
              var builder = new HostBuilder()
             .ConfigureServices((hostContext, services) =>
             {
                 services.AddScoped<ITenantHelper>(_ => new TenantHelper(null));
                 ServiceProvider serviceProvider = services.BuildServiceProvider();
                 var tenant = serviceProvider.GetService<ITenantHelper>();
                 // services.AddSingleton(typeof(IDistributedCache), typeof(DistributedCache));
                
                 services.AddTransient<IClientBpl>(_ => new ClientBpl(null, tenant));
                 //services.AddTransient<IClientBpl, ClientBpl>(_ => _cache);
                 services.AddTransient<IEmployeeBpl>(_ => new EmployeeBpl(null, tenant));
                                 var clientBpl = serviceProvider.GetService<IClientBpl>();
                 var employeeBpl = serviceProvider.GetService<IEmployeeBpl>();
                 new AppStartup(clientBpl, employeeBpl).Execute(args);
             });
            //  CreateHostBuilder(args).Build().Run();
            //  await builder.RunConsoleAsync();
            builder.Build().Run();

        }

    }
}
