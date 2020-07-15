﻿using Dapper;
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
        public List<LocationList> GetLocationDetails()
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<LocationList> lstResource = new List<LocationList>();
            var res = _db.FetchRelation1<LocationList,LocationAddress>(SPEnum.USPGETLOCATION.ToString(), dynParams);
            return res;
        }

        public bool SaveLocationDetails(List<LocationList> lstLocation)
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<Location> lstLoca = new List<Location>();
            var locDet = lstLocation.FirstOrDefault();
            lstLoca.Add(new Location
            {
                LocationId = locDet.LocationId,
                LocationType = locDet.LocationType,
                LocationName = locDet.LocationName,
                LocationDescription = locDet.LocationDescription,
                IsFranchise = locDet.IsFranchise,
                IsActive = locDet.IsActive,
                TaxRate = locDet.TaxRate,
                SiteUrl = locDet.SiteUrl,
                Currency = locDet.Currency,
                Facebook = locDet.Facebook,
                Twitter = locDet.Twitter,
                Instagram = locDet.Instagram,
                WifiDetail = locDet.WifiDetail,
                WorkhourThreshold = locDet.WorkhourThreshold
            });
            dynParams.Add("@tvpLocation", lstLoca.ToDataTable().AsTableValuedParameter("tvpLocation"));
            dynParams.Add("@tvpLocationAddress", locDet.LocationAddress.ToDataTable().AsTableValuedParameter("tvpLocationAddress"));
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

        public List<LocationList> GetLocationById(int id)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@tblLocationId", id.toInt());
            List<LocationList> lstResource = new List<LocationList>();
            var res = _db.FetchRelation1<LocationList,LocationAddress>(SPEnum.USPGETLOCATIONBYID.ToString(), dynParams);
            return res;
        }

        public bool AddLocation(List<LocationList> lstLocation)
        {
            int successCount = 0;
            foreach (var location in lstLocation)
            {
                int locationId = Convert.ToInt32(_db.Insert<LocationList>(location));
                LocationAddress locAddress = location.LocationAddress.FirstOrDefault();
                locAddress.RelationshipId = locationId;
                long addressId = _db.Insert<LocationAddress>(locAddress);
                if (locationId > 0 && addressId > 0)
                    successCount++;
            }
            return successCount == lstLocation.Count;
        }

        public bool UpdateLocation(List<LocationList> lstLocation)
        {
            int successCount = 0;
            foreach (var location in lstLocation)
            {
                bool locResult = _db.Update<LocationList>(location);
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
