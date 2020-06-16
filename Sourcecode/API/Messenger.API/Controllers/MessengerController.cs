using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Strive.BusinessEntities;
using Strive.BusinessLogic;
using Strive.Common;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Messenger.Api.Controllers
{
    public class MessengerController : ControllerBase
    {
        public MessengerController()
        {
        }

        [HttpPost]
        [Route("/[Controller]/Check")]
        [AllowAnonymous]
        public Result Check()
        {
            return null;
        }

    }
}