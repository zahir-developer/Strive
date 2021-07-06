using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
//using Microsoft.AspNet.Identity.Owin;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
//using Microsoft.Owin.Security;
using Newtonsoft.Json.Linq;
using Strive.BusinessEntities;
using Strive.BusinessEntities.Auth;
using Strive.BusinessEntities.DTO.Client;
using Strive.BusinessEntities.DTO.Employee;
using Strive.BusinessEntities.Employee;
using Strive.BusinessEntities.Model;
using Strive.BusinessLogic.Common;
using Strive.BusinessLogic.DTO.Client;
using Strive.Common;
using Strive.Crypto;
using Strive.ResourceAccess;

namespace Strive.BusinessLogic.Auth
{
    public class AuthManagerBpl : Strivebase, IAuthManagerBpl
    {
        public AuthManagerBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache) { }

        public Result Login(Authentication authentication, string secretKey, string tcon)
        {
            try
            {
                string token = string.Empty;
                string refreshToken = string.Empty;

                ValidateLogin(authentication);
                TenantSchema tSchema = new AuthRal(_tenant).Login(authentication);
                CacheLogin(tSchema, tcon);

                if (tSchema.UserType != (int)UserType.Client)
                {

                    EmployeeLoginViewModel employee = new EmployeeRal(_tenant).GetEmployeeByAuthId(tSchema.AuthId);

                    (token, refreshToken) = GetTokens(tSchema, employee, secretKey);
                    _resultContent.Add(employee.WithName("EmployeeDetails"));
                }
                else
                {
                    ClientLoginViewModel client = new ClientRal(_tenant).GetClientByAuthId(tSchema.AuthId);

                    (token, refreshToken) = GetTokens(tSchema, client, secretKey);
                    _resultContent.Add(client.WithName("ClientDetails"));
                }

                SaveRefreshToken(tSchema.UserGuid, refreshToken);
                _resultContent.Add(token.WithName("Token"));
                _resultContent.Add(refreshToken.WithName("RefreshToken"));


                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        private void CacheLogin(TenantSchema tSchema, string tcon)
        {
            SetTenantSchematoCache(tSchema);
            _tenant.SetConnection(GetTenantConnectionString(tSchema, tcon));
            _tenant.SetTenantGuid(tSchema.TenantGuid.ToString());
        }

        private void ValidateLogin(Authentication authentication)
        {
            if (!Pass.Validate(authentication.PasswordHash, new AuthRal(_tenant).GetPassword(authentication.Email)))
            {
                throw new Exception("UnAuthorized");
            }
        }

        public Result GenerateTokenByRefreshKey(string token, string refreshToken, string secretKey)
        {
            Token tkn = new Token();
            JObject resultContent = new JObject();
            var userGuid = tkn.GetUserGuidFromToken(token, secretKey);// claims.Find(a => a.Type.Contains("UserGuid")).Value;

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

        private (string, string) GetTokens(TenantSchema tenant, EmployeeLoginViewModel employee, string secretKey)
        {
            Token tkn = new Token();

            var roleId = new List<string>();
            roleId.Add("0");

            var roleName = new List<string>();
            roleName.Add(string.Empty);

            var claims = new[]
            {
                new Claim("UserGuid", $"{tenant.UserGuid}"),
                new Claim("EmployeeId", $"{employee.EmployeeLogin?.EmployeeId}"),
                new Claim("SchemaName", $"{tenant.Schemaname}"),
                new Claim("TenantGuid", $"{tenant.TenantGuid}"),
                new Claim("tid", $"{tenant.TenantId}"),
                new Claim("AuthId", $"{tenant.AuthId}")
            }.ToList();

            var roleIds = new Claim("RoleId", $"{ string.Join(",", employee.EmployeeRoles != null ? employee.EmployeeRoles.Select(x => x.RoleId.ToString()).ToList() : roleId.ToList())}");
            var roleNames = new Claim("RoleIdName", $"{string.Join(",", employee.EmployeeRoles != null ? employee.EmployeeRoles.Select(x => x.RoleName).ToList() : roleName.ToList())}");

            claims.Add(roleIds);
            claims.Add(roleNames);

            var token = tkn.Generate(claims, secretKey, "Strive", "Strive", _tenant.TokenExpiryMintues);
            var reToken = tkn.GenerateRefreshToken();
            return (token, reToken);
        }

        private (string, string) GetTokens(TenantSchema tenant, ClientLoginViewModel client, string secretKey)
        {
            Token tkn = new Token();
            var claims = new[]
            {
                new Claim("UserGuid", $"{tenant.UserGuid}"),
                new Claim("ClientId", $"{client.ClientDetail.ClientId}"),
                new Claim("SchemaName", $"{tenant.Schemaname}"),
                new Claim("TenantGuid", $"{tenant.TenantGuid}"),
                new Claim("tid", $"{tenant.TenantId}"),
                new Claim("AuthId", $"{tenant.AuthId}"),
                new Claim("RoleId", $"{string.Join(",", client.RolePermissionViewModel.Select(x => x.RoleId.ToString()).ToList())}"),
                new Claim("RoleIdName", $"{string.Join(",", client.RolePermissionViewModel.Select(x => x.RoleName).ToList())}"),
            }.ToList();

            var token = tkn.Generate(claims, secretKey, "Strive", "Strive", _tenant.TokenExpiryMintues);
            var reToken = tkn.GenerateRefreshToken();
            return (token, reToken);
        }

        public TenantSchema GetTenantSchema(Guid userGuid)
        {
            TenantSchema tenantSchema = new AuthRal(_tenant).GetSchema(userGuid);
            SetTenantSchematoCache(tenantSchema);
            return tenantSchema;
        }

        public void Logout(string token, string secretKey)
        {
            Token tkn = new Token();
            var claims = tkn.GetPrincipalFromExpiredToken(token, secretKey);
            var userGuid = claims.Find(a => a.Type.Contains("UserGuid")).Value;
            DeleteRefreshToken(userGuid, null);
        }

        //AuthenticationProperties IAuthManagerBpl.ConfigureExternalAuthenticationProperties(string provider, string redirectUrl)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<ExternalLoginInfo> IAuthManagerBpl.GetExternalLoginInfoAsync()
        //{
        //    throw new NotImplementedException();
        //}

        Task<dynamic> IAuthManagerBpl.ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent, bool bypassTwoFactor)
        {
            throw new NotImplementedException();
        }

        public Result CreateCustomer(ClientDto client, string conn)
        {
            if (client.Token != null)
            {
                TenantSchema tSchema = new TenantRal(_tenant, true).TenantAdminLogin(client.Token.GetValueOrDefault());

                if (tSchema == null)
                    return Helper.BindValidationErrorResult("Invalid Authentication Token");

                //Create Account - Auth db
                var comBpl = new CommonBpl(_cache, _tenant);

                var accountDetail = client.ClientAddress.FirstOrDefault();

                _tenant.TenantGuid = client.Token.GetValueOrDefault().ToString();

                var clientLogin = comBpl.CreateLogin(UserType.Client, accountDetail.Email, accountDetail.PhoneNumber, client.Password);
                client.Client.AuthId = clientLogin.authId;

                //Create Customer - Tenant db
                CacheLogin(tSchema, conn);

                var clientBpl = new ClientBpl(_cache, _tenant);

                var createClient = clientBpl.SaveClientDetails(client);

                return createClient;
            }
            else
                return Helper.BindValidationErrorResult("Invalid Authentication Token");
        }

        public Result EmailIdExists(string emailId)
        {
            return ResultWrap(new AuthRal(_tenant).EmailIdExists, emailId, "EmailIdExist");
        }

        public int CreateLogin(UserType userType, HtmlTemplate htmlTemplate, string emailId, string mobileNumber)
        {
            var commonBpl = new CommonBpl(_cache, _tenant);

            var createLogin = commonBpl.CreateLogin(userType, emailId, mobileNumber);

            commonBpl.SendLoginCreationEmail(htmlTemplate, emailId, createLogin.Item2);

            return createLogin.Item1;
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

        public Result GetModelByMakeId(int makeId)
        {
            return ResultWrap(new AuthRal(_tenant).GetModelByMakeId, makeId, "Model");

        }

        public Result GetAllMake()
        {
            return ResultWrap(new AuthRal(_tenant).GetAllMake, "Make");
        }

        public Result GetAllColor()
        {
            return ResultWrap(new AuthRal(_tenant).GetAllColor, "Color");
        }

        //public Microsoft.Owin.Security.AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Microsoft.AspNet.Identity.Owin.ExternalLoginInfo> GetExternalLoginInfoAsync()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
