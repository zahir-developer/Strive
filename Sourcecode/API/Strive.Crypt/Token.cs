using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Strive.Crypto
{
    public class Token
    {
        public string Generate(List<Claim> claims, string secretKey, string iss, string aud, int expiryMin)
        {
            Crypt crypto = new Crypt();
            var credentials = crypto.GetEncryptionStuff(secretKey);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = iss,
                Audience = aud,
                Expires = DateTime.UtcNow.AddMinutes(expiryMin),
                IssuedAt = DateTime.UtcNow,
                EncryptingCredentials = credentials.EnCredentials,
                SigningCredentials = credentials.SignCredentials
            };
            IdentityModelEventSource.ShowPII = true;
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public List<Claim> GetPrincipalFromExpiredToken(string token, string secretKey)
        {
            Crypt crypt = new Crypt();
            var credentials = crypt.GetDecryptionStuff(secretKey);

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

            if(token == null)
                throw new SecurityTokenException("Invalid token");

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null) //|| !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal.Claims.ToList();
        }

        public string Generate(string token, string secretKey,int expiryMin)
        {
            var claims = GetPrincipalFromExpiredToken(token, secretKey);
            return Generate(claims,secretKey,"","",expiryMin);
        }

        public string GetUserGuidFromToken(string token, string secretKey)
        {
            var claims = GetPrincipalFromExpiredToken(token, secretKey);
            return claims.Find(a => a.Type.Contains("UserGuid")).Value;
        }

    }
}
