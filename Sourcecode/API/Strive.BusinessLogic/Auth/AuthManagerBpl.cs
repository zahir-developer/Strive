using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Strive.BusinessEntities;
using Strive.BusinessEntities.Employee;
using Strive.Common;
using Strive.ResourceAccess;

namespace Strive.BusinessLogic.Auth
{
    public class AuthManagerBpl : Strivebase, IAuthManagerBpl
    {
        private readonly ITenantHelper _tenant;

        public AuthManagerBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(cache)
        {
            _tenant = tenantHelper;
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
                var userDetails = new AuthRal(_tenant).Login(authentication);
                SetTenantSchematoCache(userDetails);
                _tenant.SetConnection(GetTenantConnectionString(userDetails, tenantConString));
                Employee employee = new EmployeeRal(_tenant).GetEmployeeByAuthId(userDetails.AuthId);
                var token = GetToken(userDetails, employee, secretKey);
                string refreshToken = GenerateRefreshToken();
                SaveRefreshToken(userDetails.UserGuid, refreshToken);
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
            JObject resultContent = new JObject();
            var claims = GetPrincipalFromExpiredToken(token, secretKey);
            var userGuid = claims.Find(a => a.Type.Contains("UserGuid")).Value;

            var savedRefreshToken = GetRefreshToken(userGuid); //retrieve the refresh token from a data store

            if (savedRefreshToken != refreshToken)
            {
                throw new SecurityTokenException("Invalid refresh token");
            }

            var newJwtToken = GetTokenWithClaims(claims, secretKey);
            var newRefreshToken = GenerateRefreshToken();
            DeleteRefreshToken(userGuid, refreshToken);
            SaveRefreshToken(userGuid, newRefreshToken);

            resultContent.Add(newJwtToken.WithName("Token"));
            resultContent.Add(newRefreshToken.WithName("RefreshToken"));
            var result = Helper.BindSuccessResult(resultContent);
            return result;
        }

        private string GetToken(TenantSchema tenant, Employee employee, string secretKey)
        {
            var claims = new[]
            {
                new Claim("UserGuid", $"{tenant.UserGuid}"),
                new Claim("SchemaName", $"{tenant.Schemaname}"),
                 new Claim("TenantGuid", $"{tenant.TenantGuid}"),
                new Claim("AuthId", $"{employee.EmployeeDetail.AuthId}"),
                new Claim("RoleId",
                    $"{string.Join(',', employee.EmployeeRole.Select(x => x.EmployeeRoleId).ToList())}"),
                new Claim("RoleIdName", $"{string.Join(',', employee.EmployeeRole.Select(x => x.RoleName).ToList())}"),

            }.ToList();

            return GetTokenWithClaims(claims, secretKey);

        }

        private string GetTokenWithClaims(List<Claim> claims, string secretKey)
        {

            var credentials = Strive.Common.Utility.GetEncryptionStuff(secretKey);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = "Mammoth-Strive",
                Audience = "Mammoth-Customer",
                Expires = DateTime.UtcNow.AddMinutes(15),
                IssuedAt = DateTime.UtcNow,
                EncryptingCredentials = credentials.EnCredentials,
                SigningCredentials = credentials.SignCredentials
            };
            IdentityModelEventSource.ShowPII = true;
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private List<Claim> GetPrincipalFromExpiredToken(string token, string secretKey)
        {
            var credentials = Strive.Common.Utility.GetDecryptionStuff(secretKey);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                TokenDecryptionKey = credentials.DecryptKey,
                IssuerSigningKey = credentials.SignKey,

                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null) //|| !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal.Claims.ToList();
        }

        public void Logout(string token, string secretKey)
        {
            var claims = GetPrincipalFromExpiredToken(token, secretKey);
            var userGuid = claims.Find(a => a.Type.Contains("UserGuid")).Value;
            DeleteRefreshToken(userGuid, null);
        }
    }
}
