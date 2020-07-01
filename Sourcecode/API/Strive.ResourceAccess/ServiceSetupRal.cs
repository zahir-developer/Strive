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
        public List<tblService> GetServiceSetupDetails()
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<tblService> lstResource = new List<tblService>();
            var res = db.Fetch<tblService>(SPEnum.USPGETSERVICE.ToString(), dynParams);
            return res;
        }

        public List<tblCodeValue> GetAllServiceType()
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<tblCodeValue> lstResource = new List<tblCodeValue>();
            var res = db.Fetch<tblCodeValue>(SPEnum.USPGETALLSERVICETYPE.ToString(), dynParams);
            return res;
        }

        public bool SaveNewServiceDetails(List<tblService> lstServiceSetup)
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
        public List<tblService> GetServiceSetupById(int id)
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<tblService> lstResource = new List<tblService>();
            dynParams.Add("@tblServiceId", id.toInt());
            var res = db.Fetch<tblService>(SPEnum.USPGETSERVICEBYID.ToString(), dynParams);
            return res;
        }
    }
}

