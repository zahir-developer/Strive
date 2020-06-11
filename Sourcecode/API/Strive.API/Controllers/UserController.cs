using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Strive.BusinessEntities;
using Strive.BusinessLogic;
using Strive.Common;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace aHEAdWebAPI.Controllers
{
    [Authorize]
    public class UserController : ControllerBase
    {
        readonly IUser _user;
        readonly IConfiguration _configuration;

        public UserController(IUser user, IConfiguration configuration)
        {
            _user = user;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("Users")]
        [AllowAnonymous]
        public Result GetAllUsers()
        {
            var result = _user.GetUsers();
            return result;
        }

    }
}