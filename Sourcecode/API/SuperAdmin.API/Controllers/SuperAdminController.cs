using SuperAdmin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Strive.BusinessEntities;
using Strive.BusinessEntities.Auth;
using Strive.BusinessEntities.Model;
using Strive.BusinessLogic.Auth;
using Strive.Common;
using Strive.BusinessEntities.ViewModel;
using Strive.BusinessLogic.SuperAdmin.Tenant;

namespace SuperAdmin.Api.Controllers
{
    [Authorize]
    [Route("/SuperAuth/")]
    public class SuperAdminController : StriveControllerBase<TenantBpl>
    {
        public SuperAdminController(TenantBpl tenantManager, IConfiguration config) : base(tenantManager, config) { }

        #region POST

        /// <summary>
        /// Login for Employee and Client.
        /// </summary>
        [HttpPost, Route("CreateTenant")]
        public Result CreateTenant([FromBody] TenantViewModel tenant)
        {
            return new Result();
        }

        //[HttpPost, Route("Refresh")]
        //public Result Refresh([FromBody] RegenerateToken regToken) => _bplManager.GenerateTokenByRefreshKey(regToken.Token, regToken.RefreshToken, GetSecretKey());

        //[HttpPost, Route("CreateLogin")]
        //public Result CreateLogin([FromBody] AuthMaster authMaster)
        //{
        //    Newtonsoft.Json.Linq.JObject _resultContent = new Newtonsoft.Json.Linq.JObject();
        //    Result _result;

        //    var result = _bplManager.CreateLogin(authMaster.EmailId, authMaster.MobileNumber);

        //    _resultContent.Add((result > 0).WithName("Status"));
        //    _result = Helper.BindSuccessResult(_resultContent);
        //    return _result;

        //}

        //[HttpPost, Route("ResetPassword")]
        //public Result ResetPassword([FromBody] ResetPassword resetPassword) => _bplManager.ResetPassword(resetPassword);

        //[HttpPut, Route("SendOtp")]
        //public Result SendOTP(string emailId) => _bplManager.SendOTP(emailId);

        #endregion

        #region GET
        [HttpGet, Route("Tenant")]
        public Result GetTenant()
        {
            return null;
        }

        #endregion

    }
}