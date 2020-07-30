using Dapper;
using Strive.BusinessEntities;
using Strive.BusinessEntities.ServiceSetup;
using Strive.Common;
using Strive.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Strive.ResourceAccess
{
    public class ServiceSetupRal
    {
        IDbConnection _dbconnection;
        public Db db;
        public ServiceSetupRal(IDbConnection dbconnection)
        {
            _dbconnection = dbconnection;
        }

        public ServiceSetupRal(ITenantHelper tenant)
        {
            _dbconnection = tenant.db();
            db = new Db(_dbconnection);
        }
        public List<ServiceView> GetServiceSetupDetails()
        {
            DynamicParameters dynParams = new DynamicParameters();
            var res = db.Fetch<ServiceView>(SPEnum.USPGETSERVICE.ToString(), dynParams);
            return res;
        }

        public bool SaveNewServiceDetails(List<Service> lstServiceSetup)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@tvpService", lstServiceSetup.ToDataTable().AsTableValuedParameter("tvpService"));
            CommandDefinition cmd = new CommandDefinition(SPEnum.USPSAVESERVICE.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }
        public bool DeleteServiceById(int id)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@tblServiceId", id.toInt());
            CommandDefinition cmd = new CommandDefinition(SPEnum.USPDELETESERVICEBYID.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }
        public List<Service> GetServiceSetupById(int id)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@tblServiceId", id.toInt());
            var res = db.Fetch<Service>(SPEnum.USPGETSERVICEBYID.ToString(), dynParams);
            return res;
        }
    }
}

