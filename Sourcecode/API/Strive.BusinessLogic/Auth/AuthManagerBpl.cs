using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Strive.BusinessEntities;
using Strive.BusinessEntities.Auth;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Strive.BusinessLogic.Auth
{
    public class AuthManagerBpl : IAuthManager
    {
        public Result Login(Authentication authentication, string secretKey)
        {
            Result result;
            JObject resultContent = new JObject();
            try
            {
                //var userdetails = new AuthRal().Login(authentication);

                var userdetails = new User() { FirstName = "Mamooth", LastName = "Strive", LoginId = "Mamooth", Role = "Admin" };
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

        private string GetToken(User user, string secretKey)
        {

            var credentials = Common.Utility.GetEncryptionStuff(secretKey);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                {
                    new Claim("UserName",$"{user.FirstName}{user.LastName}"),
                    new Claim("LoginId",user.LoginId),
                    new Claim(ClaimTypes.Role, user.Role),
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
