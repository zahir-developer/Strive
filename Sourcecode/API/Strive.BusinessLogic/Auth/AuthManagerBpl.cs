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
using System.Linq;

namespace Strive.BusinessLogic
{
    public class AuthManagerBpl : Strivebase, IAuthManagerBpl
    {
        ITenantHelper tenant;
        string TenantConnectionStringTemplate;
        public AuthManagerBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(cache)
        {
            tenant = tenantHelper;
        }

        public TenantSchema GetTenantSchema(Guid UserGuid)
        {
            TenantSchema tenantSchema = new AuthRal(tenant).GetSchema(UserGuid);
            SetTenantSchematoCache(tenantSchema);
            return tenantSchema;
        }


        public Result Login(Authentication authentication, string secretKey, string tConStringtemplate)
        {
            Result result;
            JObject resultContent = new JObject();
            try
            {
                var userdetails = new AuthRal(tenant).Login(authentication);
                SetTenantSchematoCache(userdetails);
                tenant.SetConnection(GetTenantConnectionString(userdetails, tConStringtemplate));
                Employee employee = new EmployeeRal(tenant).GetEmployeeByAuthId(userdetails.AuthId);                
                var token = GetToken(userdetails, employee, secretKey);
                resultContent.Add(token.WithName("Token"));
                resultContent.Add(employee.WithName("EmployeeDetails"));
                result = Helper.BindSuccessResult(resultContent);
            }
            catch (Exception ex)
            {
                result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return result;
        }

        private string GetToken(TenantSchema tenant, Employee employee, string secretKey)
        {

            var credentials = Strive.Common.Utility.GetEncryptionStuff(secretKey);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                {
                    new Claim("UserGuid",$"{tenant.UserGuid}"),
                    new Claim("SchemaName",$"{tenant.Schemaname}"),
                    new Claim("AuthId",$"{employee.EmployeeDetail.AuthId}"),
                    new Claim("RoleId",$"{  string.Join(',',employee.EmployeeRole.Select(x=> x.EmployeeRoleId).ToList())}"),
                    new Claim("RoleIdName",$"{  string.Join(',',employee.EmployeeRole.Select(x=> x.RoleName).ToList())}"),

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
