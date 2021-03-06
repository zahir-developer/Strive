using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Strive.BusinessEntities;
using Strive.BusinessLogic;
using Strive.Common;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Strive.API.Controllers
{
    public class AuthController : ControllerBase
    {
        readonly IAuthManagerBpl _authManager;
        readonly IConfiguration _configuration;

        public AuthController(IAuthManagerBpl authManager, IConfiguration configuration)
        {
            _authManager = authManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public Result Login([FromBody] Authentication authentication)
        {
            //var pwd = CustomPasswordHasher.HashPassword(authentication.PasswordHash);
            //var pwd = authentication.PasswordHash;
            //authentication.PasswordHash = pwd;
            var result = _authManager.Login(authentication, _configuration.GetSection("StriveSettings:Jwt")["SecretKey"]);
            return result;
        }

    }
}