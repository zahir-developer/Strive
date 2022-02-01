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
using Strive.BusinessLogic.PaymentGateway;
using Strive.Common;

namespace Admin.API.Scheduler
{
    [DisallowConcurrentExecution]
    public class ThirdPaymentScheduler : IScheduledTask, IJob
    {
        public string Schedule => "*/1 * * * *";
        IDistributedCache _cache;
        IConfiguration _config;
        TenantHelper _tenant = new TenantHelper(null);
        public ThirdPaymentScheduler(IDistributedCache dcache, IConfiguration config)
        {
            _config = config;
            _cache = dcache;
            var strConnectionString = Pick("ConnectionStrings", "StriveConnection");
            var strAdminConnectionString = Pick("ConnectionStrings", "StriveAuthAdmin");
            string schemaName = "StriveCarSalon";
            _tenant.SetAuthDBConnection(strConnectionString);
            _tenant.SetAuthAdminDBConnection(strAdminConnectionString);

            var strTenantSchema = _cache.GetString(schemaName);
            var isSchemaAvailable = (!string.IsNullOrEmpty(strTenantSchema));

            if (!isSchemaAvailable) _tenant.SetConnection(strConnectionString);
            //string userGuid = "0be3990a-cf20-4c64-b3cd-40d35207dfe2";

            string connectionString = _config.GetSection("EmailScheduler")["StriveClientConnection"];
            /*if (!string.IsNullOrEmpty(userGuid))
            {

                var tenantSchema = (isSchemaAvailable) ? JsonConvert.DeserializeObject<TenantSchema>(strTenantSchema) :
                    new AuthManagerBpl(_cache, _tenant).GetTenantSchema(Guid.Parse(userGuid));

                strConnectionString = $"Server={Pick("Settings", "TenantDbServer")};Initial Catalog={Pick("Settings", "TenantDb")};MultipleActiveResultSets=true;User ID={tenantSchema.Username};Password={tenantSchema.Password}";
                _tenant.SetConnection(strConnectionString);

            }*/
            strConnectionString = connectionString;
            _tenant.SetConnection(strConnectionString);

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
            _tenant.SchemaName = schemaName;

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
                new PaymentGatewayBpl(_cache, _tenant).MakeRecurringPayment(3);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Task.CompletedTask;
        }
    }
}
