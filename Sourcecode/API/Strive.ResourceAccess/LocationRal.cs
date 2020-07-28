using Dapper;
using Strive.BusinessEntities;
using Strive.BusinessEntities.Location;
using Strive.BusinessEntities.Model;
using Strive.Common;
using Strive.Repository;
using Strive.RepositoryCqrs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Strive.ResourceAccess
{
    public class LocationRal : RalBase
    {
        public LocationRal(ITenantHelper tenant) : base(tenant) { }

        public List<LocationDto> GetLocationDetails()
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<LocationDto> lstResource = new List<LocationDto>();
            var res = db.FetchRelation1<LocationDto, LocationAddress>(SPEnum.USPGETLOCATION.ToString(), dynParams);
            return res;
        }

        public bool SaveLocationDetails(LocationDto location)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@tvpLocation", location.TableName("tvpLocation"));
            dynParams.Add("@tvpLocationAddress", location.LocationAddress.TableName("tvpLocationAddress"));
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

        public List<LocationDto> GetLocationById(int id)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@tblLocationId", id.toInt());
            List<LocationDto> lstResource = new List<LocationDto>();
            var res = db.FetchRelation1<LocationDto, LocationAddress>(SPEnum.USPGETLOCATIONBYID.ToString(), dynParams);
            return res;
        }

        public bool AddLocation(LocationDto location)
        {
            bool addResult = DbRepo.InsertPc<LocationDto>(location,"LocationId",cs);
            return true;
        }

        public bool AddLocation(List<LocationDto> lstLocation)
        {
            int successCount = 0;
            foreach (var location in lstLocation)
            {
                int locationId = Convert.ToInt32(db.Insert<LocationDto>(location));
                LocationAddress locAddress = location.LocationAddress;
                locAddress.LocationId = locationId;
                long addressId = db.Insert<LocationAddress>(locAddress);
                if (locationId > 0 && addressId > 0)
                    successCount++;
            }
            return successCount == lstLocation.Count;
        }

        public bool UpdateLocation(LocationDto location)
        {
            int successCount = 0;
                bool locResult = db.Update<LocationDto>(location);
                LocationAddress locAddress = location.LocationAddress;
                locAddress.LocationId = location.Location.LocationId;
                var addResult = db.Update<LocationAddress>(locAddress);
                if (locResult && addResult)
                    successCount++;
            return true;
        }
    }
}
