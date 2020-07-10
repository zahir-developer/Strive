using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Strive.BusinessEntities;
using Strive.Common;
using System;
using System.Linq;
using Strive.BusinessLogic.Auth;

namespace Admin.API.Filters
{
    public class AdminPayloadFilter : IActionFilter
    {
        private readonly IConfiguration _config;
        private readonly IDistributedCache _cache;
        readonly ITenantHelper _tenant;
        readonly GData _gdata = new GData();

        public AdminPayloadFilter(IConfiguration conf, IDistributedCache cache, ITenantHelper tenantHelper)
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
            //string actionName = context.ActionDescriptor.RouteValues["action"];
            //string controllerName = context.ActionDescriptor.RouteValues["controller"];

            //// our code after action executes
            //if (context.HttpContext.Request.Path.Value.Contains("/Login"))
            //{
            //    GData gdata = new GData();
            //    gdata = (GData)context.HttpContext.Items["gdata"];
            //    context.HttpContext.Response.Headers.Add("Token", gdata.gToken);
            //}
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
            if (!context.HttpContext.Request.Path.Value.Contains("Admin/Login") &&
                 !context.HttpContext.Request.Path.Value.Contains("Admin/Refresh") &&
                 !context.HttpContext.Request.Path.Value.Contains("Admin/Weather"))
            {
                isAuth = false;
                userGuid = context.HttpContext.User.Claims.ToList().Find(a => a.Type.Contains("UserGuid")).Value;
                TenantGuid = context.HttpContext.User.Claims.ToList().Find(a => a.Type.Contains("TenantGuid")).Value;
                schemaName = context.HttpContext.User.Claims.ToList().Find(a => a.Type.Contains("SchemaName")).Value;
            }
            SetDbConnection(userGuid, schemaName, isAuth, TenantGuid);
        }

        private void SetDbConnection(string userGuid, string schemaName, bool isAuth = false, string tenantGuid =null)
        {
            var strConnectionString = Pick("ConnectionStrings", "StriveConnection");

            if (isAuth)
            {
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

                if(isSchemaAvailable)
                {
                    _tenant.SetTenantGuid(tenantGuid);
                }
                
                strConnectionString = $"Server={Pick("Settings", "TenantDbServer")};Initial Catalog={Pick("Settings", "TenantDb")};MultipleActiveResultSets=true;User ID={tenantSchema.Username};Password={tenantSchema.Password}";
                _tenant.SetConnection(strConnectionString);
            }
        }
    }
}
