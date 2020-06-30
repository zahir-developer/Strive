using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Strive.BusinessEntities;
using Strive.BusinessLogic;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Strive.BusinessLogic.Auth;

namespace Admin.API.Filters
{
    public class AdminPayloadFilter : IActionFilter
    {
        private readonly IConfiguration config;
        private readonly IDistributedCache cache;
        ITenantHelper tenant;
        GData gdata = new GData();

        public AdminPayloadFilter(IConfiguration conf, IDistributedCache dcache, ITenantHelper tenantHelper)
        {
            config = conf;
            cache = dcache;
            tenant = tenantHelper;
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
                configValue = config.GetSection("StriveAdminSettings:" + section)[name];
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
            string userGuid = string.Empty;
            string schemaName = string.Empty;
            if (!context.HttpContext.Request.Path.Value.Contains("Admin/Login") &&
                 !context.HttpContext.Request.Path.Value.Contains("Admin/Refresh"))
            {
                isAuth = false;
                userGuid = context.HttpContext.User.Claims.ToList().Find(a => a.Type.Contains("UserGuid")).Value;
                schemaName = context.HttpContext.User.Claims.ToList().Find(a => a.Type.Contains("SchemaName")).Value;
            }
            SetDBConnection(userGuid, schemaName, isAuth);
        }

        private void SetDBConnection(string UserGuid, string SchemaName, bool isAuth = false)
        {
            string strConnectionString = Pick("ConnectionStrings", "StriveConnection");

            if (isAuth)
            {
                tenant.SetConnection(strConnectionString);
            }
            else
            {
                string strTenantSchema = cache.GetString(SchemaName);
                bool IsSchemaAvailable = (!string.IsNullOrEmpty(strTenantSchema));

                if (!IsSchemaAvailable) tenant.SetConnection(strConnectionString);

                var tenantSchema = (IsSchemaAvailable) ? JsonConvert.DeserializeObject<TenantSchema>(strTenantSchema) :
                    new AuthManagerBpl(cache, tenant).GetTenantSchema(Guid.Parse(UserGuid));

                if (tenantSchema is null)
                {
                    throw new Exception("Invalid Login");
                }

                strConnectionString = $"Server={Pick("Settings", "TenantDbServer")};Initial Catalog={Pick("Settings", "TenantDb")};MultipleActiveResultSets=true;User ID={tenantSchema.Username};Password={tenantSchema.Password}";
                tenant.SetConnection(strConnectionString);
            }
        }
    }
}
