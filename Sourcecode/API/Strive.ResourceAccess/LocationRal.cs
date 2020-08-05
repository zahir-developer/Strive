using Dapper;
using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.Location;
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

        public List<LocationViewModel> GetAllLocation()
        {
            return db.Fetch<LocationViewModel>(SPEnum.USPGETALLLOCATION.ToString(), _prm);
        }

        public LocationAddress GetLocationDetailById(int locationId)
        {
            return db.GetSingleByFkId<LocationAddress>(locationId, "LocationId");
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
            location.Drawer.DrawerName = $"Drawer-{location.Location.LocationName}-1";
            return dbRepo.InsertPc(location, "LocationId");
        }

        public bool SaveLocationDetails(LocationDto location)
        {
            return dbRepo.UpdatePc(location);
        }

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
        //public LocationAddressModel GetLocationAddressDetails(int locationId)
        //{

        //    var result = new LocationAddressModel();
        //    var allAddress = _db.GetAll<LocationAddressModel>();

        //    var locationAddress = allAddress.Where(s => s.RelationshipId == locationId).FirstOrDefault();

        //    result.Latitude = locationAddress.Latitude;
        //    result.Longitude = locationAddress.Longitude;
        //    result.WeatherLocationId = locationAddress.WeatherLocationId;

        //    return result;
        //}
    }
}
