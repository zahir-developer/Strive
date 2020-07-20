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

namespace Admin.API.Controllers
{
    [Authorize]
    [Route("Admin/[Controller]")]
    public class ClientController
    {
        IClientBpl _clientBpl = null;

        public ClientController(IClientBpl clientBpl)
        {
            _clientBpl = clientBpl;
        }

        [HttpPost]
        [Route("Save")]
        public Result SaveClientDetails([FromBody] List<Strive.BusinessEntities.Client.ClientList> lstClient)
        {
            return _clientBpl.SaveClientDetails(lstClient);
        }

    }
}
