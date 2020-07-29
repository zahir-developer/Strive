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

namespace Strive.ResourceAccess
{
    public class ClientRal
    {
        IDbConnection _dbconnection;
        public Db db;
        public ClientRal(IDbConnection dbconnection)
        {
            _dbconnection = dbconnection;
        }

        public ClientRal(ITenantHelper tenant)
        {
            _dbconnection = tenant.db();
             db = new Db(_dbconnection);
        }

        public bool SaveClientDetails(ClientView lstClient)
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<Client> lstclientlst = new List<Client>();
            //var clientlst = new ClientList();
            lstclientlst.Add(new Client
            {
                ClientId = lstClient.ClientId,
                FirstName = lstClient.FirstName,
                MiddleName = lstClient.MiddleName,
                LastName = lstClient.LastName,
                Gender = lstClient.Gender,
                MaritalStatus= lstClient.MaritalStatus,
                BirthDate= lstClient.BirthDate,
                CreatedDate = lstClient.CreatedDate,
                IsActive = lstClient.IsActive,
                Notes = lstClient.Notes,
                RecNotes = lstClient.RecNotes,
                Score = lstClient.Score,
                NoEmail = lstClient.NoEmail,
                ClientType = lstClient.ClientType,

            });
            dynParams.Add("@tvpClient", lstclientlst.ToDataTable().AsTableValuedParameter("tvpClient"));
            dynParams.Add("@tvpClientAddress", lstClient.ClientAddress.ToDataTable().AsTableValuedParameter("tvpClientAddress"));
            CommandDefinition cmd = new CommandDefinition(SPEnum.USPSAVECLIENT.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);

            return true;
        }
        public List<ClientView> GetAllClient()
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<ClientView> lstClientList = new List<ClientView>();
            lstClientList = db.FetchRelation1<ClientView, ClientAddress>(SPEnum.USPGETALLCLIENT.ToString(), dynParams);
            return lstClientList;
        }
        public bool DeleteClient(int clientId)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@ClientId", clientId);
            CommandDefinition cmd = new CommandDefinition(SPEnum.USPDELETECLIENT.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }

    }
}
