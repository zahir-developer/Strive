using Dapper;
using Strive.BusinessEntities;
using Strive.Common;
using Strive.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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

        public List<Location> GetLocationById(int id)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@tblLocationId", id.toInt());
            List<Location> lstResource = new List<Location>();
            var res = _db.Fetch<Location>(SPEnum.USPGETLOCATIONBYID.ToString(), dynParams);
            return res;
        }

        public bool AddLocation(List<Location> lstLocation)
        {
            int successCount = 0;
            foreach (var location in lstLocation)
            {
                int locationId = Convert.ToInt32(_db.Insert<Location>(location));
                LocationAddress locAddress = location.LocationAddress.FirstOrDefault();
                locAddress.RelationshipId = locationId;
                long addressId = _db.Insert<LocationAddress>(locAddress);
                if (locationId > 0 && addressId > 0)
                    successCount++;
            }
            return successCount == lstLocation.Count;
        }

        public bool UpdateLocation(List<Location> lstLocation)
        {
            int successCount = 0;
            foreach (var location in lstLocation)
            {
                bool locResult = _db.Update<Location>(location);
                LocationAddress locAddress = location.LocationAddress.FirstOrDefault();
                locAddress.RelationshipId = location.LocationId;
                var addResult = _db.Update<LocationAddress>(locAddress);
                if (locResult && addResult)
                    successCount++;
            }
            return successCount == lstLocation.Count;
        }
    }
}
