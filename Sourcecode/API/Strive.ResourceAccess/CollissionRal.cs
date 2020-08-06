using Dapper;
using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO.Collision;
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
        //IDbConnection _dbconnection;
        //    public Db db;
        //    public CollisionRal(IDbConnection dbconnection)
        //    {
        //        _dbconnection = dbconnection;
        //    }

        //    public CollisionRal(ITenantHelper tenant)
        //    {
        //        _dbconnection = tenant.db();
        //        db = new Db(_dbconnection);
        //    }
        //public List<LocationViewModel> GetAllCollison()
        //{
        //    return db.Fetch<LocationViewModel>(SPEnum.USPGETALLLOCATION.ToString(), _prm);
        //}

        //public LocationAddress GetLocationDetailById(int locationId)
        //{
        //    return db.GetSingleByFkId<LocationAddress>(locationId, "LocationId");
        //}
        //public CollissionDto GetAllCollission()
        //{
        //    var result = db.FetchMultiResult<CollissionDto>(SPEnum.USPGETCOLLISION.ToString(), _prm);
        //    return result;
        //}
        public CollissionDto GetCollissionById(int id)
        {
            _prm.Add("@LiabilityId", id);
            var result = db.FetchMultiResult<CollissionDto>(SPEnum.USPGETCOLLISIONBYID.ToString(), _prm);
            return result;
        }
        //public List<CollisionListView> GetCollisionById(long id)
        //{
        //    DynamicParameters dynParams = new DynamicParameters();
        //    dynParams.Add("@CollisionId", id);
        //    List<CollisionListView> lstCollisionById = new List<CollisionListView>();
        //    lstCollisionById = db.FetchRelation1<CollisionListView, LiabilityDetail>(SPEnum.USPGETCOLLISIONBYID.ToString(), dynParams);
        //    return lstCollisionById;
        //}
        //public List<CollisionListView> GetCollisionByEmpId(long id)
        //{
        //    DynamicParameters dynParams = new DynamicParameters();
        //    dynParams.Add("@EmployeeId", id);
        //    List<CollisionListView> lstCollisionById = new List<CollisionListView>();
        //    lstCollisionById = db.FetchRelation1<CollisionListView, LiabilityDetail>(SPEnum.USPGETCOLLISIONBYEMPID.ToString(), dynParams);
        //    return lstCollisionById;
        //}

        //public bool SaveCollison(List<CollisionListView> lstCollision)
        //{
        //    DynamicParameters dynParams = new DynamicParameters();
        //    List<Collision> lstColli = new List<Collision>();
        //    var collisionReg = lstCollision.FirstOrDefault();
        //    lstColli.Add(new Collision
        //    {
        //        LiabilityId = collisionReg.LiabilityId,
        //        EmployeeId = collisionReg.EmployeeId,
        //        LiabilityType = collisionReg.LiabilityType,
        //        LiabilityDescription = collisionReg.LiabilityDescription,
        //        ProductId = collisionReg.ProductId,
        //        Status = collisionReg.Status,
        //        CreatedDate = collisionReg.CreatedDate,
        //        IsActive = collisionReg.IsActive

        //    });
        //    dynParams.Add("@tvpEmployeeLiability", lstColli.ToDataTable().AsTableValuedParameter("tvpEmployeeLiability"));
        //    dynParams.Add("@tvpEmployeeLiabilityDetail", collisionReg.LiabilityDetail.ToDataTable().AsTableValuedParameter("tvpEmployeeLiabilityDetail"));
        //    CommandDefinition cmd = new CommandDefinition(SPEnum.USPSAVECOLLISION.ToString(), dynParams, commandType: CommandType.StoredProcedure);
        //    db.Save(cmd);
        //    return true;
        //}
        //public bool DeleteCollisionDetails(long id)
        //{
        //    DynamicParameters dynParams = new DynamicParameters();
        //    dynParams.Add("@tvpEmployeeLiabilityId", id);
        //    CommandDefinition cmd = new CommandDefinition(SPEnum.USPDELETECOLLISION.ToString(), dynParams, commandType: CommandType.StoredProcedure);
        //    db.Save(cmd);
        //    return true;
        //}
    }
}

