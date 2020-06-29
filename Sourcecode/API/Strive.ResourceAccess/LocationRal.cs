using Dapper;
using Strive.BusinessEntities;
using Strive.Common;
using Strive.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Strive.ResourceAccess
{
    public class LocationRal
    {
        IDbConnection _dbconnection;
        public Db db;
        public LocationRal(IDbConnection dbconnection)
        {
            _dbconnection = dbconnection;
        }

        public LocationRal(ITenantHelper tenant)
        {
            _dbconnection = tenant.db();
            db = new Db(_dbconnection);
        }
        public List<Location> GetLocationDetails()
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<Location> lstResource = new List<Location>();
            var res = db.Fetch<Location>(SPEnum.USPGETLOCATION.ToString(), dynParams);
            return res;
        }

        public bool SaveLocationDetails(List<Location> lstLocation)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@tvpLocation", lstLocation.ToDataTable().AsTableValuedParameter("tvpLocation"));
            CommandDefinition cmd = new CommandDefinition(SPEnum.USPSAVELOCATION.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }
        public bool DeleteLocationDetails(int id)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@tblLocationId", id.toInt());
            CommandDefinition cmd = new CommandDefinition(SPEnum.USPDELETELOCATION.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }
    }
}
