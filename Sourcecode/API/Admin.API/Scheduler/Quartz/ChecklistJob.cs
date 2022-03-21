using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Strive.Common;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Strive.BusinessLogic.Checklist;
using FirebaseAdmin.Messaging;
using Strive.BusinessEntities;
using Newtonsoft.Json;
using Strive.BusinessLogic.Auth;

namespace Admin.API.Scheduler.Quartz
{
    [DisallowConcurrentExecution]
    public class ChecklistJob : IScheduledTask, IJob
    {
        public string Schedule => "*/1 * * * *";
        IDistributedCache _cache;
        IConfiguration _config;
        TenantHelper _tenant = new TenantHelper(null);
        public ChecklistJob(IDistributedCache dcache, IConfiguration config)
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
            
            _tenant.ErrorLog = Pick("Logs", "Error");
            _tenant.TimeZone = Pick("TimeZone", "EST");

        }

        protected string GetTenantConnection()
        {
            string tenantConnectionStringTemplate = $"Server={Pick("Settings", "TenantDbServer")};Initial Catalog={Pick("Settings", "TenantDb")};MultipleActiveResultSets=true;User ID=[UserName];Password=[Password]";
            return tenantConnectionStringTemplate;
        }
       
        public void SetTenantSchematoCache(TenantSchema tenantSchema)
        {
            string strTenantSchema = _cache.GetString(tenantSchema.UserGuid);
            if (string.IsNullOrEmpty(strTenantSchema))
            {
                _cache.SetString(tenantSchema.Schemaname, JsonConvert.SerializeObject(tenantSchema));
                _tenant.SchemaName = tenantSchema.Schemaname;
            }
        }
        protected string GetTenantConnectionString(TenantSchema tenantSchema, string conString)
        {
            conString = conString.Replace("[UserName]", tenantSchema.Username);
            conString = conString.Replace("[Password]", tenantSchema.Password);

            return conString;
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
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                TimeZoneInfo oZone = TimeZoneInfo.FindSystemTimeZoneById(_tenant.TimeZone);
               
                DateTime oTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, oZone);

               
                var oList = new ChecklistBpl(_cache, _tenant).GetChecklistNotificationByDate(oTime);

                if (oList.ChecklistNotification !=null)
                {
                    foreach (var oObj in oList.ChecklistNotification)
                    {
                        var msg = new Message()
                        {
                            Token = oObj.Token,
                            Data = new Dictionary<string, string>()
                            {
                                 {"NotificationDate", oObj.NotificationDate.ToShortDateString()},
                                 {"NotificationTime", oObj.NotificationTime.ToString()},
                                 {"CheckListEmployeeId", oObj.CheckListEmployeeId.ToString()},
                                 {"RoleId", oObj.RoleId.ToString()},
                                 {"Name", oObj.Name},
                            },

                            Notification = new Notification()
                            {
                                Title = "Strive Notification",
                                Body = oObj.Name
                            }
                        };

                        var response = await FirebaseMessaging.DefaultInstance.SendAsync(msg);
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

    }
}
