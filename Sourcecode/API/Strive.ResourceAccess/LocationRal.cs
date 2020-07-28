using Dapper;
using Strive.BusinessEntities;
using Strive.BusinessEntities.Location;
using Strive.BusinessEntities.Model;
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
        public List<LocationDto> GetLocationDetails()
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<LocationDto> lstResource = new List<LocationDto>();
            var res = _db.FetchRelation1<LocationDto, LocationAddress>(SPEnum.USPGETLOCATION.ToString(), dynParams);
            return res;
        }

        public bool SaveLocationDetails(LocationDto location)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@tvpLocation", location.TableName("tvpLocation"));
            dynParams.Add("@tvpLocationAddress", location.LocationAddress.TableName("tvpLocationAddress"));
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

        public List<LocationDto> GetLocationById(int id)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@tblLocationId", id.toInt());
            List<LocationDto> lstResource = new List<LocationDto>();
            var res = _db.FetchRelation1<LocationDto, LocationAddress>(SPEnum.USPGETLOCATIONBYID.ToString(), dynParams);
            return res;
        }

        public bool AddLocation(LocationDto location)
        {
            bool addResult = DbRepo.InsertPc<LocationDto>(location,"LocationId");
            return true;
        }

        public bool AddLocation(List<LocationDto> lstLocation)
        {
            int successCount = 0;
            foreach (var location in lstLocation)
            {
                int locationId = Convert.ToInt32(_db.Insert<LocationDto>(location));
                LocationAddress locAddress = location.LocationAddress;
                locAddress.LocationId = locationId;
                long addressId = _db.Insert<LocationAddress>(locAddress);
                if (locationId > 0 && addressId > 0)
                    successCount++;
            }
            return successCount == lstLocation.Count;
        }

        public bool UpdateLocation(LocationDto location)
        {
            int successCount = 0;
                bool locResult = _db.Update<LocationDto>(location);
                LocationAddress locAddress = location.LocationAddress;
                locAddress.LocationId = location.Location.LocationId;
                var addResult = _db.Update<LocationAddress>(locAddress);
                if (locResult && addResult)
                    successCount++;
            return true;
        }
    }
}
