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

        public bool SaveClientDetails(List<ClientList> lstClient)
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<Client> lstclientlst = new List<Client>();
            var clientlst = lstClient.FirstOrDefault();
            lstclientlst.Add(new Client
            {
                ClientId = clientlst.ClientId,
                FirstName = clientlst.FirstName,
                MiddleName = clientlst.MiddleName,
                LastName = clientlst.LastName,
                Gender = clientlst.Gender,
                MaritalStatus= clientlst.MaritalStatus,
                BirthDate= clientlst.BirthDate,
                CreatedDate = clientlst.CreatedDate,
                IsActive = clientlst.IsActive,
                Notes = clientlst.Notes,
                RecNotes = clientlst.RecNotes,
                Score = clientlst.Score,
                NoEmail = clientlst.NoEmail,
                ClientType = clientlst.ClientType,

            });
            dynParams.Add("@tvpClient", lstclientlst.ToDataTable().AsTableValuedParameter("tvpClient"));
            CommandDefinition cmd = new CommandDefinition(SPEnum.USPSAVECLIENT.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }

    }
}
