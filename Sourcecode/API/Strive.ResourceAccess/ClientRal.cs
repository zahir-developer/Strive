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

namespace Strive.ResourceAccess
{
    public class ClientRal : RalBase
    {
        private Db _db;

        public ClientRal(ITenantHelper tenant) : base(tenant) { }

        public bool SaveClientDetails(ClientDto client)
        {
            return dbRepo.InsertPc(client, "ClientId");
        }

        public List<ClientViewModel> GetAllClient()
        {
            return db.Fetch<ClientViewModel>(SPEnum.USPGETALLCLIENT.ToString(), null);
        }
        public ClientViewModel GetClientById(int clientId)
        {
            _prm.Add("ClientId", clientId);
            return db.FetchSingle<ClientViewModel>(SPEnum.USPGETALLCLIENT.ToString(), _prm);
        }
        public bool DeleteClient(int clientId)
        {
            _prm.Add("ClientId", clientId);
            db.Save(SPEnum.USPDELETECLIENT.ToString(), _prm);
            return true;
        }
    }
}
