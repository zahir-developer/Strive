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
    public class CollissionRal : RalBase
    {
        public CollissionRal(ITenantHelper tenant) : base(tenant) { }
        public List<CollisionViewModel> GetAllCollission()
        {
           return db.Fetch<CollisionViewModel>(SPEnum.USPGETCOLLISIONBYEMPID.ToString(), null);
        }

        public List<CollisionViewModel> GetCollissionById(int id)
        {
           
            _prm.Add("@CollisionId", id);
            var result = db.Fetch<CollisionViewModel>(SPEnum.USPGETCOLLISIONBYEMPID.ToString(), _prm);
            return result;
        }
        public List<CollisionViewModel> GetCollissionByEmpId(int id)
        {

            _prm.Add("@EmployeeId", id);
            var result = db.Fetch<CollisionViewModel>(SPEnum.USPGETCOLLISIONBYEMPID.ToString(), _prm);
            return result;
        }
        public bool DeleteCollission(int id)
        {
            _prm.Add("@tvpEmployeeLiabilityId", id);
            db.Save(SPEnum.USPDELETECOLLISION.ToString(), _prm);
            return true;
        }
        public bool AddCollission(CollissionDto lstCollision)
        {
            return dbRepo.SavePc(lstCollision, "LiabilityId");
        }
        public bool UpdateCollission(CollissionDto lstCollision)
        {
            return dbRepo.SavePc(lstCollision, "LiabilityId");
        }
    }
}

