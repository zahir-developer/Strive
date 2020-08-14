using Dapper;
using Strive.BusinessEntities;
using Strive.Common;
using Strive.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using Strive.BusinessEntities.Client;
using Strive.BusinessEntities.ViewModel;
using Strive.BusinessEntities.DTO.Client;
using Strive.BusinessEntities.DTO.Vehicle;

namespace Strive.ResourceAccess
{
    public class ClientRal : RalBase
    {
        public ClientRal(ITenantHelper tenant) : base(tenant) { }

        public bool InsertClientDetails(ClientDto client)
        {
            return dbRepo.InsertPc(client, "ClientId");
        }
        public bool UpdateClientVehicle(ClientDto client)
        {
            return dbRepo.UpdatePc(client);
        }

        public List<ClientViewModel> GetAllClient()
        {
            return db.Fetch<ClientViewModel>(SPEnum.USPGETALLCLIENT.ToString(), null);
        }
        public List<ClientDetailViewModel> GetClientById(int clientId)
        {
            _prm.Add("@ClientId", clientId);
            return db.Fetch<ClientDetailViewModel>(SPEnum.USPGETCLIENT.ToString(), _prm);
            
        }
        public bool DeleteClient(int clientId)
        {
            _prm.Add("ClientId", clientId);
            db.Save(SPEnum.USPDELETECLIENT.ToString(), _prm);
            return true;
        }
        public List<ClientSearchViewModel> GetClientSearch(ClientSearchDto search)
        {
            _prm.Add("@FirstName", search.FirstName);
            return db.Fetch<ClientSearchViewModel>(SPEnum.uspGetClientName.ToString(), _prm);
        }
    }
}
