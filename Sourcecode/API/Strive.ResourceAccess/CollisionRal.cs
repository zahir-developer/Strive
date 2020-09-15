using Dapper;
using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO.Collision;
using Strive.BusinessEntities.ViewModel;
using Strive.Common;
using Strive.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Strive.ResourceAccess
{
    public class CollisionRal : RalBase
    {
        public CollisionRal(ITenantHelper tenant) : base(tenant) { }
        public List<CollisionViewModel> GetAllCollision()
        {
            return db.Fetch<CollisionViewModel>(SPEnum.USPGETCOLLISIONBYEMPID.ToString(), null);
        }

        public CollisionViewModel GetCollisionById(int id)
        {

            _prm.Add("@CollisionId", id);
            var result = db.FetchMultiResult<CollisionViewModel>(SPEnum.USPGETCOLLISIONBYID.ToString(), _prm);
            return result;
        }
        public List<CollisionViewModel> GetCollisionByEmpId(int id)
        {
            _prm.Add("@EmployeeId", id);
            var result = db.Fetch<CollisionViewModel>(SPEnum.USPGETCOLLISIONBYEMPID.ToString(), _prm);
            return result;
        }
        public bool DeleteCollision(int id)
        {
            _prm.Add("@tvpEmployeeLiabilityId", id);
            db.Save(SPEnum.USPDELETECOLLISION.ToString(), _prm);
            return true;
        }
        public bool AddCollision(CollisionDto lstCollision)
        {
            return dbRepo.SavePc(lstCollision, "LiabilityId");
        }
        public bool UpdateCollision(CollisionDto lstCollision)
        {
            return dbRepo.SavePc(lstCollision, "LiabilityId");
        }
        public List<VehicleListViewModel> GetVehicleListByClientId(int id)
        {
            _prm.Add("@ClientId", id);
            var result = db.Fetch<VehicleListViewModel>(SPEnum.USPGETVEHICLELISTBYCLIENTID.ToString(), _prm);
            return result;
        }
    }
}