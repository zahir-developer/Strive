using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Quartz;
using Strive.BusinessEntities;
using Strive.BusinessLogic;
using Strive.BusinessLogic.Auth;
using Strive.Common;

namespace Admin.API.Scheduler
{
    [DisallowConcurrentExecution]
    public class EmailScheduler : IScheduledTask, IJob
    {
        public string Schedule => "*/1 * * * *";
        IDistributedCache _cache;
        IConfiguration _config;
        TenantHelper _tenant = new TenantHelper(null);
        public EmailScheduler(IDistributedCache dcache, IConfiguration config)
        {
            _config = config;
            _cache = dcache;
            var strConnectionString = Pick("ConnectionStrings", "StriveConnection");
            var strAdminConnectionString = Pick("ConnectionStrings", "StriveAuthAdmin");
            Authentication authentication = new Authentication();
            authentication.Email = _config.GetSection("EmailScheduler")["UserName"];
            authentication.PasswordHash = _config.GetSection("EmailScheduler")["Password"];

            _tenant.SetAuthDBConnection(strConnectionString);
            _tenant.SetConnection(strConnectionString);
            
            new AuthManagerBpl(_cache,_tenant).Login(authentication, GetTenantConnection());

            //Applicationurl
            _tenant.ApplicationUrl = Pick("ApplicationUrl", "Url");
            _tenant.MobileUrl = Pick("ApplicationUrl", "MobileUrl");

            //Azure Web App
            _tenant.AzureStorageConn = Pick("Azure", "ConnctionString");
            _tenant.AzureStorageContainer = Pick("Azure", "Container");
            _tenant.AzureBlobHtmlTemplate = Pick("Azure", "BlobHtmlTemplate");
            _tenant.TenantFolder = Pick("Azure", "TenantFolder");

            _tenant.SMTPClient = Pick("SMTP", "SMTPClient");
            _tenant.SMTPPassword = Pick("SMTP", "Password");
            _tenant.Port = Pick("SMTP", "Port");
            _tenant.FromMailAddress = Pick("SMTP", "FromAddress");

        }
        protected string GetTenantConnection()
        {
            string tenantConnectionStringTemplate = $"Server={Pick("Settings", "TenantDbServer")};Initial Catalog={Pick("Settings", "TenantDb")};MultipleActiveResultSets=true;User ID=[UserName];Password=[Password]";
            return tenantConnectionStringTemplate;
        }
        private string Pick(string section, string name)
        {
            string configValue = string.Empty;

            try
            {
                configValue = _config.GetSection("StriveAdminSettings:" + section)[name];
                if (configValue is null)
                {
                    configValue = string.Empty;
                }
            }
            catch (Exception ex)
            {

            }
            return configValue;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {

        }

        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                new ClientBpl(_cache, _tenant).SendClientEmail();
                new EmployeeBpl(_cache, _tenant).SendEmployeeEmail();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Task.CompletedTask;
        }
    }
}
