using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strive.API.Filters
{
    public class StrivepayloadFilter : IActionFilter
    {
        private readonly IConfiguration config;
        private readonly IDistributedCache cache;
        GData gdata = new GData();

        public StrivepayloadFilter(IConfiguration conf, IDistributedCache dcache)
        {
            config = conf;
            cache = dcache;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                string actionName = context.ActionDescriptor.RouteValues["action"];
                string controllerName = context.ActionDescriptor.RouteValues["controller"];
                GetDetailsFromToken(context);
                context.HttpContext.Items.Add("gdata", gdata);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private string Pick(string section, string name)
        {
            string configValue = string.Empty;

            try
            {
                configValue = config.GetSection("StriveSettings:" + section)[name];
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
            if (!context.HttpContext.Request.Path.Value.Contains("/Login"))
            {
                ///... Connection string should be retrieved from token.
                gdata.gTenantConnectionString = context.HttpContext.User.Claims.ToList().Find(a => a.Type.Contains("connection")).Value;
                gdata.gDbName = gdata.gTenantConnectionString.Split(';').ToList().Find(a => a.Contains("Initial Catalog")).ToString().Replace("Initial Catalog=", "");                gdata.LoginId = context.HttpContext.User.Claims.ToList().Find(a => a.Type.Contains("LoginId")).Value;
                gdata.gDbName = context.HttpContext.User.Claims.ToList().Find(a => a.Type.Contains("DbName")).Value;


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


    }
}
