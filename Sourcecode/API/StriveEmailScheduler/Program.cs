using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Strive.BusinessLogic;
using Strive.BusinessLogic.Client;

namespace StriveEmailScheduler
{
    class Program
    {
        public static void Main(string[] args)
        {
            var builder = new HostBuilder()
             .ConfigureServices((hostContext, services) =>
             {
                 services.AddTransient<IClientBpl, ClientBpl>();
                 services.AddTransient<IEmployeeBpl, EmployeeBpl>();
                 ServiceProvider serviceProvider = services.BuildServiceProvider();
                 var clientBpl = serviceProvider.GetService<IClientBpl>();
                 var employeeBpl = serviceProvider.GetService<IEmployeeBpl>();
                 new AppStartup(clientBpl, employeeBpl).Execute(args);
             });
            //  CreateHostBuilder(args).Build().Run();

        }

    }
}
