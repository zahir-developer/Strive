using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.API.Helpers
{
    public class StriveControllerBase<T> : ControllerBase
    {
        protected readonly T _bplManager;
        protected readonly IConfiguration _config;

        public StriveControllerBase(T bpl, IConfiguration config = null)
        {
            _bplManager = bpl;
            _config = config;
        }

        protected string GetSecretKey()
        {
            string secretKey = Pick("Jwt", "SecretKey");
            return secretKey;
        }

        protected string GetTenantConnection()
        {
            string tenantConnectionStringTemplate = $"Server={Pick("Settings", "TenantDbServer")};Initial Catalog={Pick("Settings", "TenantDb")};MultipleActiveResultSets=true;User ID=[UserName];Password=[Password]";
            return tenantConnectionStringTemplate;
        }


        private string Pick(string section, string name)
        {
            return _config.GetSection("StriveAdminSettings:" + section)[name] ?? string.Empty;
        }

        protected string GetUserName()
        {
            return _config.GetSection("EmailScheduler")["UserName"] ?? string.Empty;
        }
        protected string GetPassword()
        {
           return _config.GetSection("EmailScheduler")["Password"] ?? string.Empty;
        }
    }
}
