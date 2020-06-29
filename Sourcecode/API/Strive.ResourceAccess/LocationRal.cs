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
        IDbConnection _dbConnection;
        private Db db;

        public LocationRal(ITenantHelper tenent)
        {
            _dbConnection = tenent.db();
            db = new Db(_dbConnection);
        }

        public List<Location> GetAllLocation()
        {
            return db.FetchRelation1<Location, LocationAddress>(SPEnum.USPGETAllLOCATION.ToString(), new DynamicParameters());
        }

        public bool SaveLocation(List<Location> locations)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@tvpLocation", locations.ToDataTable().AsTableValuedParameter("tvpEmployee"));

            CommandDefinition cmd = new CommandDefinition(SPEnum.USPSaveLOCATION.ToString(), parameters, commandType: CommandType.StoredProcedure);

            db.Save(cmd);

            return true;
        }
    }
}
