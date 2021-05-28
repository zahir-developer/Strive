using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Strive.BusinessEntities;
using Strive.BusinessEntities.Auth;
using Strive.BusinessEntities.Model;
using Strive.BusinessLogic.Auth;
using Strive.Common;

namespace Admin.Api.Controllers
{
    [AllowAnonymous]
    [Route("/Auth/")]
    public class AuthController : StriveControllerBase<IAuthManagerBpl>
    {
        private readonly ILogger _logger;

        public AuthController(IAuthManagerBpl authManager, IConfiguration config, ILogger<AuthController> logger) : base(authManager, config)
        {
            _logger = logger;
        }

        #region POST

        /// <summary>
        /// Login for Employee and Client.
        /// </summary>
        [HttpPost, Route("Login")]
        public Result Login([FromBody] Authentication authentication)
        {
            try
            {
                _logger.LogInformation("Test strive message....!!!");
            }
            catch (System.Exception)
            {

                throw;
            }
            
            return _bplManager.Login(authentication, GetSecretKey(), GetTenantConnection());
        }

        [HttpPost, Route("Refresh")]
        public Result Refresh([FromBody] RegenerateToken regToken) => _bplManager.GenerateTokenByRefreshKey(regToken.Token, regToken.RefreshToken, GetSecretKey());

        [HttpPost, Route("CreateEmployeeLogin")]
        public Result CreateLogin([FromBody]AuthMaster authMaster)
        {
            Newtonsoft.Json.Linq.JObject _resultContent = new Newtonsoft.Json.Linq.JObject();
            Result _result;

            var result = _bplManager.CreateLogin(UserType.Employee, HtmlTemplate.EmployeeSignUp, authMaster.EmailId, authMaster.MobileNumber);

            _resultContent.Add((result > 0).WithName("Status"));
            _result = Helper.BindSuccessResult(_resultContent);
            return _result;

        }

        [HttpPost, Route("CustomerSignup")]
        public Result CustomerSignup([FromBody]AuthMaster authMaster)
        {
            Newtonsoft.Json.Linq.JObject _resultContent = new Newtonsoft.Json.Linq.JObject();
            Result _result;

            var result = _bplManager.CreateLogin(UserType.Employee, HtmlTemplate.ClientSignUp, authMaster.EmailId, authMaster.MobileNumber);


            _resultContent.Add((result > 0).WithName("Status"));
            _result = Helper.BindSuccessResult(_resultContent);
            return _result;

        }

        [HttpPost, Route("ResetPassword")]
        public Result ResetPassword([FromBody]ResetPassword resetPassword) => _bplManager.ResetPassword(resetPassword);

        [HttpPut, Route("SendOtp")]
        public Result SendOTP(string emailId) => _bplManager.SendOTP(emailId);

        #endregion

        #region GET
        [HttpGet, Route("VerfiyOtp")]
        public Result VerfiyOTP(string emailId, string otp) => _bplManager.VerifyOTP(emailId, otp);

        [HttpGet, Route("Logout")]
        public void Logout(string token) => _bplManager.Logout(token, GetSecretKey());

        #endregion

        [HttpGet, Route("LogoutAllApp")]
        public void LogoutAllApp (string tokenkey) => _bplManager.Logout(tokenkey, GetSecretKey());

        [HttpPost, Route("Log")]
        public void Log(string message)
        {
            try
            {
                _logger.LogInformation(message);
                throw new System.Exception(message);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}