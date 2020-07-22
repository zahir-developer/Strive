using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Strive.BusinessLogic;
using Strive.Common;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using Microsoft.AspNetCore.Authorization;
using Strive.BusinessLogic.Client;
using Strive.BusinessEntities.Auth;
using Admin.Api.Controllers;
using Strive.BusinessLogic.Auth;
using Microsoft.Extensions.Configuration;
using Strive.BusinessEntities.Client;

namespace Admin.API.Controllers
{
    [Authorize]
    [Route("Admin/[Controller]")]
    public class ClientController
    {
        IClientBpl _clientBpl = null;
        readonly IAuthManagerBpl _authManager;
        readonly IConfiguration _configuration;

        public ClientController(IClientBpl clientBpl, IAuthManagerBpl authManager)
        {
            _clientBpl = clientBpl;
            _authManager = authManager;
        }

        [HttpPost]
        [Route("Save")]
        public Result SaveClientDetails([FromBody] ClientList client)
        {
            return _clientBpl.SaveClientDetails(client);

        }
    

    }
}
