using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strive.BusinessEntities.Client;
using Strive.BusinessEntities.DTO.Client;
using Strive.BusinessEntities.DTO.Vehicle;
using Strive.Common;

namespace Strive.BusinessLogic.Client
{
    public interface IClientBpl
    {
        //Result SaveClientDetails(ClientView lstClient);
        Result SaveClientDetails(ClientDto client);
        Result GetAllClient();
        Result DeleteClient(int clientId);
        Result GetClientById(int clientId);
        Result GetClientVehicleById(int clientId);
        Result UpdateClientVehicle(ClientDto vehicle);
        Result GetClientSearch(ClientSearchDto search);
        Result GetClientCodes();
        Result GetStatementByClientId(int id);
        Result GetHistoryByClientId(int id);
    }
}
