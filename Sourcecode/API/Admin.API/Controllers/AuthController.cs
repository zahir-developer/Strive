using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Strive.BusinessEntities;
using Strive.BusinessEntities.Auth;
using Strive.BusinessLogic.Auth;
using Strive.Common;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Admin.Api.Controllers
{
    [AllowAnonymous]
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
        public Result Login([FromBody] Authentication authentication)
        {
            string secretKey = Pick("Jwt", "SecretKey");
            string tenantConnectionStringTemplate = $"Server={Pick("Settings", "TenantDbServer")};Initial Catalog={Pick("Settings", "TenantDb")};MultipleActiveResultSets=true;User ID=[UserName];Password=[Password]";

            var result = _authManager.Login(authentication, secretKey, tenantConnectionStringTemplate);
            return result;
        }

        [HttpPost, Route("/Admin/Refresh"), AllowAnonymous]
        public Result Refresh([FromBody]RegenerateToken regToken)
        {
            string secretKey = Pick("Jwt", "SecretKey");
            var result = _authManager.GenerateTokenByRefreshKey(regToken.Token, regToken.RefreshToken, secretKey);
            return result;
        }

        [HttpPost, Route("/Admin/CreateLogin"), AllowAnonymous]
        public Result CreateLogin([FromBody]UserLogin userLogin)
        {
            Newtonsoft.Json.Linq.JObject _resultContent = new Newtonsoft.Json.Linq.JObject();
            Result _result;

            var result = _authManager.CreateLogin(userLogin);

            _resultContent.Add((result > 0).WithName("Status"));
            _result = Helper.BindSuccessResult(_resultContent);
            return _result;

        }
       

        [HttpPut, Route("/Admin/SendOTP/{emailId}"), AllowAnonymous]
        public Result SendOTP(string emailId)
        {
            return _authManager.SendOTP(emailId);
        }

        [HttpGet, Route("/Admin/VerfiyOTP/{emailId}/{otp}"), AllowAnonymous]
        public Result VerfiyOTP(string emailId, string otp)
        {
            return _authManager.VerifyOTP(emailId, otp);
        }

        [HttpPost, Route("/Admin/ResetPassword"), AllowAnonymous]
        public Result ResetPassword([FromBody]ResetPassword resetPassword)
        {
            return _authManager.ResetPassword(resetPassword);
        }

        public void Logout(string token)
        {
            string secretKey = Pick("Jwt", "SecretKey");
            _authManager.Logout(token, secretKey);
        }

        private string Pick(string section, string name)
        {
            return _configuration.GetSection("StriveAdminSettings:" + section)[name] ?? string.Empty;
        }
    }
}