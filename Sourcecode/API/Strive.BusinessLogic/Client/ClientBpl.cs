﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strive.Common;
using Strive.ResourceAccess;
using System.Net;
using Strive.BusinessEntities.Client;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Strive.BusinessLogic.Client;
using Strive.BusinessLogic.Common;
using Strive.BusinessEntities.Auth;
using Strive.BusinessEntities.DTO.Client;
using Strive.BusinessEntities.Model;
using Strive.BusinessEntities.DTO.Client;
using Strive.BusinessEntities.DTO.Vehicle;

namespace Strive.BusinessLogic
{
    public class ClientBpl : Strivebase, IClientBpl
    {
        public ClientBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache)
        {
        }

        public Result SaveClientDetails(ClientDto client)
        {
            try
            {

                var newitem = new CommonBpl(_cache, _tenant).CreateLogin(lstClient.ClientAddress.FirstOrDefault().Email, lstClient.ClientAddress.FirstOrDefault().PhoneNumber);
                bool blnStatus = new ClientRal(_tenant).SaveClientDetails(lstClient);

                _resultContent.Add(blnStatus.WithName("Status"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result UpdateClientVehicle(ClientDto vehicle)
        {
            try
            {
                return ResultWrap(new ClientRal(_tenant).UpdateClientVehicle, vehicle, "Status");
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result GetAllClient()
        {
            return ResultWrap(new ClientRal(_tenant).GetAllClient, "Client");
        }
        public Result GetClientById(int clientId)
        {
            return ResultWrap(new ClientRal(_tenant).GetClientById, clientId, "Status");
        }
        public Result DeleteClient(int clientId)
        {
            return ResultWrap(new ClientRal(_tenant).DeleteClient, clientId, "Status");
        }
    }
}
