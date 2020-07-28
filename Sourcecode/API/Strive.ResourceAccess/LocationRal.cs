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
            return db.FetchRelation1<LocationDto, LocationAddress>(SPEnum.USPGETLOCATION.ToString(), _prm);
        }

        public List<LocationDto> GetLocationById(int id)
        {
            _prm.Add("@tblLocationId", id.toInt());
            return db.FetchRelation1<LocationDto, LocationAddress>(SPEnum.USPGETLOCATIONBYID.ToString(), _prm);
        }

        public bool AddLocation(LocationDto location)
        {
            return DbRepo.InsertPc(location, "LocationId", cs);
        }

        public bool SaveLocationDetails(LocationDto location)
        {
            return DbRepo.UpdatePc(location, "LocationId", cs);
        }

        public bool UpdateLocation(LocationDto location)
        {
            return DbRepo.UpdatePc(location, "LocationId", cs);
        }

        public bool DeleteLocationDetails(int id)
        {
            _prm.Add("@tblLocationId", id.toInt());
            db.Save(SPEnum.USPDELETELOCATION.ToString(), _prm);
            return true;
        }
    }
}
