﻿using System;
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
using Admin.API.Helpers;
using Strive.BusinessEntities.DTO.Client;

namespace Admin.API.Controllers
{
    [Authorize]
    [Route("Admin/[Controller]")]
    public class ClientController : StriveControllerBase<IClientBpl>
    {
        public ClientController(IClientBpl clientBpl) : base(clientBpl) { }

        [HttpPost]
        [Route("Save")]
        public Result SaveClientDetails([FromBody] ClientDto client) => _bplManager.SaveClientDetails(client);

        [HttpGet]
        [Route("GetAllClient")]
        public Result GetAllClient()
        {
            return _bplManager.GetAllClient();

        }
        [HttpDelete]
        [Route("{clientId}")]
        public Result DeleteClient(int clientId)
        {
            return _bplManager.DeleteClient(clientId);
        }
        [HttpGet]
        [Route("GetClientById/{clientId}")]
        public Result GetClientById(int clientId)
        {
            return _bplManager.GetClientById(clientId);
        }

    }
}
