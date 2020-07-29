using Dapper;
using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO.Location;
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

        public List<LocationViewModel> GetAllLocation()
        {
            return db.Fetch<LocationViewModel>(SPEnum.USPGETALLLOCATION.ToString(), _prm);
        }

        public LocationAddress GetLocationDetailById(int locationId)
        {
            return db.GetSingleByFkId<LocationAddress>(locationId, "LocationId");
        }

        public bool AddLocation(LocationDto location)
        {
            return DbRepo.InsertPc(location, "LocationId", cs, _tenant.SchemaName);
        }

        public bool SaveLocationDetails(LocationDto location)
        {
            return DbRepo.UpdatePc(location, "LocationId", cs, _tenant.SchemaName);
        }

        public bool UpdateLocation(LocationDto location)
        {
            return DbRepo.UpdatePc(location, "LocationId", cs, _tenant.SchemaName);
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
    }
}
