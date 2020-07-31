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
            Client cli = lstClient;
            dynParams.Add("@tvpClient", cli.TableName("tvpClient"));
            dynParams.Add("@tvpClientAddress", lstClient.ClientAddress.TableName("tvpClientAddress"));
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
        public List<ClientView> GetClientById(int id)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@ClientId", id);
            var lstClientInfo = db.FetchRelation1<ClientView, ClientAddress>(SPEnum.USPGETCLIENTBYID.ToString(), dynParams);
            return lstClientInfo;
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
