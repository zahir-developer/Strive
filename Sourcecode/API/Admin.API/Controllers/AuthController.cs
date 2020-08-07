using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        public AuthController(IAuthManagerBpl authManager, IConfiguration config) : base(authManager, config) { }

        #region POST

        [HttpPost, Route("Login")]
        public Result Login([FromBody] Authentication authentication) => _bplManager.Login(authentication, GetSecretKey(), GetTenantConnection());

        [HttpPost, Route("Refresh")]
        public Result Refresh([FromBody] RegenerateToken regToken) => _bplManager.GenerateTokenByRefreshKey(regToken.Token, regToken.RefreshToken, GetSecretKey());

        [HttpPost, Route("CreateLogin")]
        public Result CreateLogin([FromBody]AuthMaster authMaster)
        {
            Newtonsoft.Json.Linq.JObject _resultContent = new Newtonsoft.Json.Linq.JObject();
            Result _result;

            var result = _bplManager.CreateLogin(authMaster.EmailId, authMaster.MobileNumber);

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

    }
}