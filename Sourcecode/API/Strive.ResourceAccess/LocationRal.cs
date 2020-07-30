using Dapper;
using Strive.BusinessEntities;
using Strive.BusinessEntities.Location;
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
        public List<LocationView> GetLocationDetails()
        {
            DynamicParameters dynParams = new DynamicParameters();
            var res = _db.FetchRelation1<LocationView,LocationAddress>(SPEnum.USPGETLOCATION.ToString(), dynParams);
            return res;
        }
        public List<LocationAddressModel> GetAllLocationAddress()
        {
            List<LocationAddressModel> lam = new List<LocationAddressModel>();
            var allAddress = _db.GetAll<LocationAddressModel>();
            lam.AddRange(allAddress);
            return lam;
        }

        public bool SaveLocationDetails(LocationView location)
        {
            DynamicParameters dynParams = new DynamicParameters();
            Location loc = location; //(TVP. Issue)Workaround - Save Error.
            dynParams.Add("@tvpLocation", loc.TableName("tvpLocation"));
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

        public List<LocationView> GetLocationById(int id)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@tblLocationId", id.toInt());
            var res = _db.FetchRelation1<LocationView,LocationAddress>(SPEnum.USPGETLOCATIONBYID.ToString(), dynParams);
            return res;
        }

        public bool AddLocation(List<LocationView> lstLocation)
        {
            int successCount = 0;
            foreach (var location in lstLocation)
            {
                int locationId = Convert.ToInt32(_db.Insert<LocationView>(location));
                LocationAddress locAddress = location.LocationAddress;
                locAddress.RelationshipId = locationId;
                long addressId = _db.Insert<LocationAddress>(locAddress);
                if (locationId > 0 && addressId > 0)
                    successCount++;
            }
            return successCount == lstLocation.Count;
        }

        public bool UpdateLocation(List<LocationView> lstLocation)
        {
            int successCount = 0;
            foreach (var location in lstLocation)
            {
                bool locResult = _db.Update<LocationView>(location);
                LocationAddress locAddress = location.LocationAddress;
                locAddress.RelationshipId = location.LocationId;
                var addResult = _db.Update<LocationAddress>(locAddress);
                if (locResult && addResult)
                    successCount++;
            }
            return successCount == lstLocation.Count;
        }
        public LocationAddressModel GetLocationAddressDetails(int locationId)
        {

            var result = new LocationAddressModel();
            var allAddress = _db.GetAll<LocationAddressModel>();

            var locationAddress = allAddress.Where(s => s.RelationshipId == locationId).FirstOrDefault();

            result.Latitude = locationAddress.Latitude;
            result.Longitude = locationAddress.Longitude;
            result.WeatherLocationId = locationAddress.WeatherLocationId;

            return result;
        }
    }
}
