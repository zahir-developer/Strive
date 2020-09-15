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

        public bool AddLocation(LocationDto location)
        {
            return dbRepo.InsertPc(location, "LocationId");
        }

        public bool UpdateLocation(LocationDto location)
        {
            return dbRepo.UpdatePc(location);
        }

        public bool DeleteLocation(int id)
        {    
            _prm.Add("LocationId", id.toInt());
            _prm.Add("UserId", _tenant.EmployeeId);
            _prm.Add("Date", DateTime.UtcNow);
            db.Save(SPEnum.USPDELETELOCATION_TEMP.ToString(), _prm);
            return true;
        }

        public List<LocationViewModel> GetSearchResult(LocationSearchDto search)
        {
            _prm.Add("@LocationSearch", search.LocationSearch);
            return db.Fetch<LocationViewModel>(SPEnum.USPGETALLLOCATION.ToString(), _prm);
        }

        public List<LocationViewModel> GetAllLocation()
        {
            return db.Fetch<LocationViewModel>(SPEnum.USPGETALLLOCATION.ToString(), _prm);
        }

        public LocationDto GetLocationDetailById(int id)
        {
            _prm.Add("@tblLocationId", id);
            var result = db.FetchMultiResult<LocationDto>(SPEnum.USPGETLOCATIONBYID.ToString(), _prm);
            return result;
        }
    }
}
