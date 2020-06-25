using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Strive.BusinessEntities;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Strive.BusinessLogic
{
    public class AuthManagerBpl : Strivebase, IAuthManagerBpl
    {
        ITenantHelper tenant;

        public AuthManagerBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(cache){
            tenant = tenantHelper;
        }

        public TenantSchema GetTenantSchema(Guid UserGuid)
        {
            TenantSchema tenantSchema = new AuthRal(tenant).GetSchema(UserGuid);
            SetTenantSchematoCache(tenantSchema);
            return tenantSchema;
        }


        public Result Login(Authentication authentication, string secretKey)
        {
            Result result;
            JObject resultContent = new JObject();
            try
            {
                var userdetails = new AuthRal(tenant).Login(authentication);
                SetTenantSchematoCache(userdetails);
                var token = GetToken(userdetails, secretKey);
                resultContent.Add(token.WithName("Token"));
                result = Helper.BindSuccessResult(resultContent);
            }
            catch (Exception ex)
            {
                result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return result;
        }

        private string GetToken(TenantSchema tenant, string secretKey)
        {

            var credentials = Common.Utility.GetEncryptionStuff(secretKey);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                {
                    new Claim("UserGuid",$"{tenant.UserGuid}"),
                    new Claim("SchemaName",$"{tenant.Schemaname}")
                }),
                Issuer = "Mammoth-Strive",
                Audience = "Mammoth-Customer",
                Expires = DateTime.UtcNow.AddDays(1),
                IssuedAt = DateTime.UtcNow,
                EncryptingCredentials = credentials.EnCredentials,
                SigningCredentials = credentials.SignCredentials
            };
            IdentityModelEventSource.ShowPII = true;
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
