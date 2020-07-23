using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using Newtonsoft.Json.Linq;
using Strive.BusinessEntities;
using Strive.BusinessEntities.Auth;
using Strive.BusinessEntities.Employee;
using Strive.BusinessLogic.Common;
using Strive.Common;
using Strive.Crypto;
using Strive.ResourceAccess;

namespace Strive.BusinessLogic.Auth
{
    public class AuthManagerBpl : Strivebase, IAuthManagerBpl
    {
        private readonly ITenantHelper _tenant;
        private readonly IDistributedCache _cache;

        public AuthManagerBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(cache)
        {
            _tenant = tenantHelper;
            _cache = cache;
        }

        public TenantSchema GetTenantSchema(Guid userGuid)
        {
            TenantSchema tenantSchema = new AuthRal(_tenant).GetSchema(userGuid);
            SetTenantSchematoCache(tenantSchema);
            return tenantSchema;
        }


        public Result Login(Authentication authentication, string secretKey, string tenantConString)
        {
            Result result;
            JObject resultContent = new JObject();
            try
            {
                Token tkn = new Token();
                TenantSchema tenantSchema = null;
                var dbPassHash = new AuthRal(_tenant).GetPassword(authentication.Email);

                if (!Pass.Validate(authentication.PasswordHash, dbPassHash))
                {
                    throw new Exception("UnAuthorized");
                }

                tenantSchema = new AuthRal(_tenant).Login(authentication);
                SetTenantSchematoCache(tenantSchema);
                _tenant.SetConnection(GetTenantConnectionString(tenantSchema, tenantConString));
                EmployeeView employee = new EmployeeRal(_tenant).GetEmployeeByAuthId(tenantSchema.AuthId);
                (string token, string refreshToken) = GetTokens(tenantSchema, employee, secretKey);
                SaveRefreshToken(tenantSchema.UserGuid, refreshToken);
                resultContent.Add(token.WithName("Token"));
                resultContent.Add(refreshToken.WithName("RefreshToken"));
                resultContent.Add(employee.WithName("EmployeeDetails"));
                result = Helper.BindSuccessResult(resultContent);
            }
            catch (Exception ex)
            {
                result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return result;
        }

        public Result GenerateTokenByRefreshKey(string token, string refreshToken, string secretKey)
        {
            Token tkn = new Token();
            JObject resultContent = new JObject();
            //var claims = tkn.GetPrincipalFromExpiredToken(token, secretKey);
            var userGuid = tkn.GetUserGuidFromToken(token,secretKey);// claims.Find(a => a.Type.Contains("UserGuid")).Value;

            var savedRefreshToken = GetRefreshToken(userGuid); //retrieve the refresh token from a data store

            if (savedRefreshToken != refreshToken)
            {
                throw new SecurityTokenException("Invalid refresh token");
            }

            var newJwtToken = tkn.Generate(token, secretKey, _tenant.TokenExpiryMintues);//  GetTokenWithClaims(claims, secretKey);
            var newRefreshToken = tkn.GenerateRefreshToken();
            DeleteRefreshToken(userGuid, refreshToken);
            SaveRefreshToken(userGuid, newRefreshToken);

            resultContent.Add(newJwtToken.WithName("Token"));
            resultContent.Add(newRefreshToken.WithName("RefreshToken"));
            var result = Helper.BindSuccessResult(resultContent);
            return result;
        }

        private (string, string) GetTokens(TenantSchema tenant, EmployeeView employee, string secretKey)
        {
            Token tkn = new Token();
            var claims = new[]
            {
                new Claim("UserGuid", $"{tenant.UserGuid}"),
                new Claim("SchemaName", $"{tenant.Schemaname}"),
                 new Claim("TenantGuid", $"{tenant.TenantGuid}"),
                new Claim("AuthId", $"{employee.EmployeeDetail.Select(n=> n.AuthId)}"),
                new Claim("RoleId",
                    $"{string.Join(",", employee.EmployeeRole.Select(x => x.EmployeeRoleId.ToString()).ToList())}"),
                new Claim("RoleIdName", $"{string.Join(",", employee.EmployeeRole.Select(x => x.RoleId).ToList())}"),

            }.ToList();

            var token = tkn.Generate(claims, secretKey, "Strive", "Strive", _tenant.TokenExpiryMintues);
            var reToken = tkn.GenerateRefreshToken();
            return (token, reToken);
            //return GetTokenWithClaims(claims, secretKey);

        }

        public void Logout(string token, string secretKey)
        {
            Token tkn = new Token();
            var claims = tkn.GetPrincipalFromExpiredToken(token, secretKey);
            var userGuid = claims.Find(a => a.Type.Contains("UserGuid")).Value;
            DeleteRefreshToken(userGuid, null);
        }

        //public Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent, bool bypassTwoFactor)
        //{
        //    throw new NotImplementedException();
        //}

        AuthenticationProperties IAuthManagerBpl.ConfigureExternalAuthenticationProperties(string provider, string redirectUrl)
        {
            throw new NotImplementedException();
        }

        Task<ExternalLoginInfo> IAuthManagerBpl.GetExternalLoginInfoAsync()
        {
            throw new NotImplementedException();
        }

        Task<dynamic> IAuthManagerBpl.ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent, bool bypassTwoFactor)
        {
            throw new NotImplementedException();
        }

        public int CreateLogin(UserLogin userLogin)
        {
            return new CommonBpl(_cache, _tenant).CreateLogin(userLogin);
        }

        public bool ForgotPassword(string userId)
        {
            return new CommonBpl(_cache, _tenant).ForgotPassword(userId);
        }

        public Result ResetPassword(ResetPassword resetPassword)
        {
            return new CommonBpl(_cache, _tenant).ResetPassword(resetPassword);
        }

        public Result SendOTP(string emailId)
        {
            return new CommonBpl(_cache, _tenant).SendOTP(emailId);
        }

        public Result VerifyOTP(string emailId, string otp)
        {
            return new CommonBpl(_cache, _tenant).VerfiyOTP(emailId, otp);
        }

    }
}
