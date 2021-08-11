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
            return db.Fetch<LocationViewModel>(EnumSP.Location.USPGETALLLOCATION.ToString(), _prm);
        }

        public List<LocationViewModel> GetLocationSearch(LocationSearchDto search)
        {
            _prm.Add("@LocationSearch", search.LocationSearch);
            return db.Fetch<LocationViewModel>(EnumSP.Location.USPGETALLLOCATION.ToString(), _prm);
        }

        public LocationDescriptionViewModel GetLocationDetailById(int id)
        {
            _prm.Add("@tblLocationId", id);
            var result = db.FetchMultiResult<LocationDescriptionViewModel>(EnumSP.Location.USPGETLOCATIONBYID.ToString(), _prm);
            return result;
        }
        //public List<LocationAddressModel> GetAllLocationAddress()
        //{
        //    List<LocationAddressModel> lam = new List<LocationAddressModel>();
        //    var allAddress = _db.GetAll<LocationAddressModel>();
        //    lam.AddRange(allAddress);
        //    return lam;
        //}

        public int AddLocation(LocationDto location)
        {
            return dbRepo.InsertPK(location, "LocationId");
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
            db.Save(EnumSP.Location.USPDELETELOCATION.ToString(), _prm);
            return true;
        }
        public LocationAddress GetLocationAddressDetails(int locationId)
        {
            _prm.Add("locationId", locationId);
            return db.FetchFirstResult<LocationAddress>("uspGetLocationAddress", _prm);
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
            return db.Fetch<LocationOffsetViewModel>(EnumSP.Location.USPGETALLLOCATIONOFFSET.ToString(), _prm);
        }
        public bool DeleteLocationOffset(int id)
        {    
            _prm.Add("LocationOffsetId", id.toInt());
            db.Save(EnumSP.Location.USPDELETELOCATIONOFFSET.ToString(), _prm);
            return true;
        }
        public bool AddBaySolt(int id)
        {
            _prm.Add("LocationId", id.toInt());
            db.Save(EnumSP.Location.USPADDBAYSLOT.ToString(), _prm);
            return true;
        }

        public List<LocationNameViewModel> GetAllLocationName()
        {
            return db.Fetch<LocationNameViewModel>(EnumSP.Location.USPGETALLLOCATIONNAME.ToString(), _prm);
        }
        public List<MerchantDetailViewModel> GetMerchantDetails(MerchantSearch search)
        {
            _prm.Add("@tblLocationId", search.LocationId);
            _prm.Add("@userName", search.UserName);
            _prm.Add("@password", search.Password);
            var result = db.Fetch<MerchantDetailViewModel>(EnumSP.Location.USPGETMERCHANTDETAIL.ToString(), _prm);
            return result;
        }

    }
}
