using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Strive.BusinessEntities;
using Strive.Common;
using System;
using System.Linq;

namespace Messenger.Api.Filters
{
    public class AdminPayloadFilter : IActionFilter
    {
        private readonly IConfiguration _config;
        private readonly IDistributedCache _cache;
        readonly ITenantHelper _tenant;
        readonly GData gdata = new GData();

        public AdminPayloadFilter(IConfiguration conf, IDistributedCache dcache, ITenantHelper tenantHelper)
        {
            _config = conf;
            _cache = dcache;
            _tenant = tenantHelper;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                string actionName = context.ActionDescriptor.RouteValues["action"];
                string controllerName = context.ActionDescriptor.RouteValues["controller"];
                context.HttpContext.Items.Add("gdata", gdata);
                GetDetailsFromToken(context);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            string actionName = context.ActionDescriptor.RouteValues["action"];
            string controllerName = context.ActionDescriptor.RouteValues["controller"];

            // our code after action executes
            if (context.HttpContext.Request.Path.Value.Contains("/Login"))
            {
                GData gdata = new GData();
                gdata = (GData)context.HttpContext.Items["gdata"];
                context.HttpContext.Response.Headers.Add("Token", gdata.gToken);
            }
        }

        private string Pick(string section, string name)
        {
            string configValue = string.Empty;

            try
            {
                configValue = _config.GetSection("StriveMessengerSettings:" + section)[name];
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
            bool isAuth = true;
            string UserGuid = string.Empty;
            if (!context.HttpContext.Request.Path.Value.Contains("/Login"))
            {
                isAuth = false;
                UserGuid = context.HttpContext.User.Claims.ToList().Find(a => a.Type.Contains("UserGuid")).Value;
            }
            SetDBConnection(UserGuid, isAuth);
        }

        private void SetDBConnection(string userGuid, bool isAuth = false)
        {
            string strConnectionString = Pick("ConnectionStrings", "StriveConnection");

            if (isAuth)
            {
                _tenant.SetConnection(strConnectionString);
            }
            else
            {
                string strTenantSchema = _cache.GetString(userGuid);
                if (!string.IsNullOrEmpty(strTenantSchema))
                {
                    var tenantSchema = JsonConvert.DeserializeObject<TenantSchema>(strTenantSchema);
                    strConnectionString = $"Server={Pick("Settings", "TenantDbServer")};Initial Catalog={Pick("Settings", "TenantDb")};MultipleActiveResultSets=true;User ID={tenantSchema.Username};Password={tenantSchema.Password}";
                    _tenant.SetConnection(strConnectionString);
                }
            }
        }
    }
}
