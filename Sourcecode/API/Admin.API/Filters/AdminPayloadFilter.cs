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
            var ClientId = string.Empty;
            var tid = string.Empty;
            var requestPath = context.HttpContext.Request.Path.Value;

            if (!requestPath.Contains("/Auth/") && !requestPath.Contains("/Signup/") && !requestPath.Contains("/External/"))
            {
                isAuth = false;
                userGuid = context.HttpContext.User.Claims.ToList().Find(a => a.Type.Contains("UserGuid")).Value;
                TenantGuid = context.HttpContext.User.Claims.ToList().Find(a => a.Type.Contains("TenantGuid")).Value;
                schemaName = context.HttpContext.User.Claims.ToList().Find(a => a.Type.Contains("SchemaName")).Value;

                var emp = context.HttpContext.User.Claims.ToList().Find(a => a.Type.Contains("EmployeeId"));

                if (emp != null)
                    EmployeeId = emp.Value;

                var client = context.HttpContext.User.Claims.ToList().Find(a => a.Type.Contains("ClientId"));

                if (client != null)
                    ClientId = client.Value;

                tid = context.HttpContext.User.Claims.ToList().Find(a => a.Type.Contains("tid")).Value;
                _tenant.TenatId = tid;
            }
            SetDbConnection(userGuid, schemaName, isAuth, TenantGuid, EmployeeId, ClientId);
        }

        private void SetDbConnection(string userGuid, string schemaName, bool isAuth = false, string tenantGuid = null, string employeeId = null, string clientId = null)
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

            _tenant.SMTPClient = Pick("SMTP", "SMTPClient");
            _tenant.SMTPPassword = Pick("SMTP", "Password");
            _tenant.Port = Pick("SMTP", "Port");
            _tenant.FromMailAddress = Pick("SMTP", "FromAddress");
            _tenant.EmployeeId = employeeId;
            _tenant.ClientId = clientId;
            _tenant.SchemaName = schemaName;
            
            //Folder Path
            _tenant.ProductImageFolder = Pick("FolderPath", "ProductImage");
            _tenant.LogoImageFolder = Pick("FolderPath", "LogoImage");
            _tenant.DocumentUploadFolder = Pick("FolderPath", "EmployeeDocument");
            _tenant.GeneralDocumentFolder = Pick("FolderPath", "GeneralDocument");
            _tenant.VehicleImageFolder = Pick("FolderPath", "VehicleImage");
            
            //File Format
            _tenant.DocumentFormat = Pick("FileFormat", "EmployeeDocument");
            _tenant.ProductImageFormat = Pick("FileFormat", "ProductImage");
            _tenant.LogoImageFormat = Pick("FileFormat", "LogoImage");
            _tenant.EmployeeHandbook = Pick("FileFormat", "EmployeeHandbook");
            _tenant.TermsAndCondition = Pick("FileFormat", "TermsAndCondition");

            //Image Size
            _tenant.ImageThumbWidth = Convert.ToInt32(Pick("ImageThumb", "Width"));
            _tenant.ImageThumbHeight = Convert.ToInt32(Pick("ImageThumb", "Height"));
            _tenant.LogoThumbWidth = Convert.ToInt32(Pick("ImageThumb", "Width"));
            _tenant.LogoThumbHeight = Convert.ToInt32(Pick("ImageThumb", "Height"));

            //HTML Template Url
            _tenant.HtmlTemplates = Pick("FolderPath", "HtmlTemplate");

            //Card Connect
            _tenant.CCUrl = Pick("CardConnect", "Url");
            _tenant.CCUserName = Pick("CardConnect", "UserName");
            _tenant.CCPassword = Pick("CardConnect", "Password");
            _tenant.MID = Pick("CardConnect", "MID");

            //Applicationurl
            _tenant.ApplicationUrl = Pick("ApplicationUrl", "Url");
            _tenant.MobileUrl = Pick("ApplicationUrl", "MobileUrl");
        }
    }
}
