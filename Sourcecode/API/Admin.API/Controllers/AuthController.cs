using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Strive.BusinessEntities;
using Strive.BusinessLogic;
using Strive.Common;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Admin.Api.Controllers
{
    public class AuthController : ControllerBase
    {
        readonly IAuthManagerBpl _authManager;
        readonly IConfiguration _configuration;

        public AuthController(IAuthManagerBpl authManager, IConfiguration configuration)
        {
            _authManager = authManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("/Admin/Login")]
        [AllowAnonymous]
        public Result Login([FromBody] Authentication authentication)
        {
            string SecretKey = Pick("Jwt", "SecretKey");
            string TenantConnectionStringTemplate = $"Server={Pick("Settings", "TenantDbServer")};Initial Catalog={Pick("Settings", "TenantDb")};MultipleActiveResultSets=true;User ID=[UserName];Password=[Password]";

            var result = _authManager.Login(authentication, SecretKey, TenantConnectionStringTemplate);
            return result;
        }

        [HttpPost, Route("/Admin/Refresh"), AllowAnonymous]
        public Result Refresh(string token, string refreshToken)
        {
            string secretKey = Pick("Jwt", "SecretKey");
            var result = _authManager.GenerateTokenByRefreshKey(token, refreshToken, secretKey);
            return result;
        }

        public void Logout(string token)
        {
            string secretKey = Pick("Jwt", "SecretKey");
            _authManager.Logout(token, secretKey);
        }

        private string Pick(string section, string name)
        {
            string configValue = string.Empty;


            configValue = _configuration.GetSection("StriveAdminSettings:" + section)[name];
            if (configValue is null)
            {
                configValue = string.Empty;
            }

            return configValue;
        }


    }
}