using System;
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
                return ResultWrap(new ClientRal(_tenant).SaveClientDetails, client, "Status");
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result SaveClientVehicle(VehicleDto vehicle)
        {
            try
            {
                return ResultWrap(new ClientRal(_tenant).SaveClientVehicle, vehicle, "Status");
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
