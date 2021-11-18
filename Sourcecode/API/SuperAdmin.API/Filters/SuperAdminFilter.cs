using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Strive.BusinessEntities;
using Strive.Common;
using System;
using System.Linq;
using Strive.BusinessLogic.Auth;

namespace SuperAdmin.API.Filters
{
    public class SuperAdminFilter : IActionFilter
    {
        private readonly IConfiguration _config;
        private readonly IDistributedCache _cache;
        readonly ITenantHelper _tenant;
        readonly GData _gdata = new GData();

        public SuperAdminFilter(IConfiguration conf, IDistributedCache cache, ITenantHelper tenantHelper)
        {
            _config = conf;
            _cache = cache;
            _tenant = tenantHelper;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Items.Add("gdata", _gdata);
            GetDetailsFromToken(context);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
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

        private void GetDetailsFromToken(ActionExecutingContext context)
        {
            var isAuth = true;
            var userGuid = string.Empty;
            var TenantGuid = string.Empty;
            var schemaName = string.Empty;
            var EmployeeId = string.Empty;
            var tid = string.Empty;
            var requestPath = context.HttpContext.Request.Path.Value;

            if (!requestPath.Contains("/Auth/") && !requestPath.Contains("/Signup/"))
            {
                isAuth = false;
                userGuid = context.HttpContext.User.Claims?.ToList()?.Find(a => a.Type.Contains("UserGuid"))?.Value;
                TenantGuid = context.HttpContext.User.Claims?.ToList()?.Find(a => a.Type.Contains("TenantGuid"))?.Value;
                schemaName = context.HttpContext.User.Claims?.ToList()?.Find(a => a.Type.Contains("SchemaName"))?.Value;
                EmployeeId = context.HttpContext.User.Claims?.ToList()?.Find(a => a.Type.Contains("EmployeeId"))?.Value;
                tid = context.HttpContext.User.Claims?.ToList()?.Find(a => a.Type.Contains("tid"))?.Value;
                _tenant.TenantId = tid;
            }
            SetDbConnection(userGuid, schemaName, isAuth, TenantGuid, EmployeeId);
        }

        private void SetDbConnection(string userGuid, string schemaName, bool isAuth = false, string tenantGuid = null, string employeeId = null)
        {
            var strConnectionString = Pick("ConnectionStrings", "StriveConnection");

            if (isAuth)
            {
                _tenant.SetAuthDBConnection(strConnectionString);
                _tenant.SetConnection(strConnectionString);
            }
            else
            {
                _tenant.SetAuthDBConnection(strConnectionString);

                var strTenantSchema = _cache.GetString(schemaName);
                var isSchemaAvailable = (!string.IsNullOrEmpty(strTenantSchema));

                if (!isSchemaAvailable) _tenant.SetConnection(strConnectionString);

                var tenantSchema = (isSchemaAvailable) ? JsonConvert.DeserializeObject<TenantSchema>(strTenantSchema) :
                    new AuthManagerBpl(_cache, _tenant).GetTenantSchema(Guid.Parse(userGuid));

                if (tenantSchema is null)
                {
                    throw new Exception("Invalid Login");
                }

                if (isSchemaAvailable)
                {
                    _tenant.SetTenantGuid(tenantGuid);
                }

                strConnectionString = $"Server={Pick("Settings", "TenantDbServer")};Initial Catalog={Pick("Settings", "TenantDb")};MultipleActiveResultSets=true;User ID={tenantSchema.Username};Password={tenantSchema.Password}";
                _tenant.SetConnection(strConnectionString);

            }
            _tenant.TokenExpiryMintues = Pick("Jwt", "TokenExpiryMinutes").toInt();

            _tenant.SMTPClient = Pick("SMTP", "SMTPClient").ToString();
            _tenant.SMTPPassword = Pick("SMTP", "Password").ToString();
            _tenant.Port = Pick("SMTP", "Port").ToString();
            _tenant.FromMailAddress = Pick("SMTP", "FromAddress").ToString();
            _tenant.EmployeeId = employeeId;
            _tenant.SchemaName = schemaName;
        }
    }
}
