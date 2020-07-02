using Dapper;
using Strive.BusinessEntities;
using Strive.Common;
using Strive.Repository;
using System.Collections.Generic;
using System.Data;

namespace Strive.ResourceAccess
{
    public class LocationRal
    {
        private readonly Db _db;

        public LocationRal(ITenantHelper tenant)
        {
            var dbConnection = tenant.db();
            _db = new Db(dbConnection);
        }
        public List<Location> GetLocationDetails()
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<Location> lstResource = new List<Location>();
            var res = _db.Fetch<Location>(SPEnum.USPGETLOCATION.ToString(), dynParams);
            return res;
        }

        public bool SaveLocationDetails(List<Location> lstLocation)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@tvpLocation", lstLocation.ToDataTable().AsTableValuedParameter("tvpLocation"));
            CommandDefinition cmd = new CommandDefinition(SPEnum.USPSAVELOCATION.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            _db.Save(cmd);
            return true;
        }
        public bool DeleteLocationDetails(int id)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@tblLocationId", id.toInt());
            CommandDefinition cmd = new CommandDefinition(SPEnum.USPDELETELOCATION.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            _db.Save(cmd);
            return true;
        }
    }
}
