using Dapper;
using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.Location;
using Strive.BusinessEntities.Location;
using Strive.BusinessEntities.ViewModel;
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
        private readonly Db _db;

        public LocationRal(ITenantHelper tenant) : base(tenant) {
            var dbConnection = tenant.db();
            _db = new Db(dbConnection);
        }

        public List<LocationViewModel> GetAllLocation()
        {
            return db.Fetch<LocationViewModel>(SPEnum.USPGETALLLOCATION.ToString(), _prm);
        }

        public List<LocationViewModel> GetLocationSearch(LocationSearchDto search)
        {
            _prm.Add("@LocationSearch", search.LocationSearch);
            return db.Fetch<LocationViewModel>(SPEnum.USPGETALLLOCATION.ToString(), _prm);
        }

        public LocationDescriptionViewModel GetLocationDetailById(int id)
        {
            _prm.Add("@tblLocationId", id);
            var result = db.FetchMultiResult<LocationDescriptionViewModel>(SPEnum.USPGETLOCATIONBYID.ToString(), _prm);
            return result;
        }
        //public List<LocationAddressModel> GetAllLocationAddress()
        //{
        //    List<LocationAddressModel> lam = new List<LocationAddressModel>();
        //    var allAddress = _db.GetAll<LocationAddressModel>();
        //    lam.AddRange(allAddress);
        //    return lam;
        //}

        public bool AddLocation(LocationDto location)
        {
            return dbRepo.InsertPc(location, "LocationId");
        }

        //public bool SaveLocationDetails(LocationDto location)
        //{
        //    return dbRepo.UpdatePc(location);
        //}

        public bool UpdateLocation(LocationDto location)
        {
            return dbRepo.UpdatePc(location);
        }

        public bool DeleteLocation(int id)
        {

            //DbRepo.Delete<Location>(AddAudit<Location>(id, cs, _tenant.SchemaName));
            //DbRepo.Delete<Location>("LocationId",id, cs, _tenant.SchemaName);

            //var location = AddAudit<Location>(id);
            //location.LocationId = id;     
            _prm.Add("LocationId", id.toInt());
            _prm.Add("UserId", _tenant.EmployeeId);
            _prm.Add("Date", DateTime.UtcNow);
            db.Save(SPEnum.USPDELETELOCATION.ToString(), _prm);
            return true;
        }
        public LocationAddress GetLocationAddressDetails(int locationId)
        {

            var result = new LocationAddress();
            var allAddress = _db.GetAll<LocationAddress>();

            var locationAddress = allAddress.Where(s => s.LocationId == locationId).FirstOrDefault();

            result.Latitude = locationAddress.Latitude;
            result.Longitude = locationAddress.Longitude;
            result.WeatherLocationId = locationAddress.WeatherLocationId;

            return result;
        }
        public bool AddLocationOffset(LocationOffsetDto locationOffset)
        {
            return dbRepo.InsertPc(locationOffset, "LocationId");
        }

        public bool UpdateLocationOffset(LocationOffsetDto locationOffset)
        {
            return dbRepo.UpdatePc(locationOffset);
        }
        public List<LocationOffsetViewModel> GetAllLocationOffset()
        {
            return db.Fetch<LocationOffsetViewModel>(SPEnum.USPGETALLLOCATIONOFFSET.ToString(), _prm);
        }
        public bool DeleteLocationOffset(int id)
        {    
            _prm.Add("LocationOffsetId", id.toInt());
            db.Save(SPEnum.USPDELETELOCATIONOFFSET.ToString(), _prm);
            return true;
        }
        public bool AddBaySolt(int id)
        {
            _prm.Add("LocationId", id.toInt());
            db.Save(SPEnum.USPADDBAYSLOT.ToString(), _prm);
            return true;
        }

    }
}
