using Dapper;
using Strive.BusinessEntities;
using Strive.BusinessEntities.Collision;
using Strive.Common;
using Strive.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Strive.ResourceAccess
{
    public class CollisionRal
    {
        IDbConnection _dbconnection;
        public Db db;
        public CollisionRal(IDbConnection dbconnection)
        {
            _dbconnection = dbconnection;
        }

        public CollisionRal(ITenantHelper tenant)
        {
            _dbconnection = tenant.db();
            db = new Db(_dbconnection);
        }
        public List<Collision> GetAllCollison()
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<Collision> lstCollision = new List<Collision>();
            lstCollision = db.FetchRelation1<Collision, LiabilityDetail>(SPEnum.USPGETCOLLISION.ToString(), dynParams);
            return lstCollision;
        }

        public List<Collision> GetCollisionById(long id)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@CollisionId", id);
            List<Collision> lstCollisionById = new List<Collision>();
            lstCollisionById = db.FetchRelation1<Collision, LiabilityDetail>(SPEnum.USPGETCOLLISIONBYID.ToString(), dynParams);
            return lstCollisionById;
        }

        public bool SaveCollison(List<CollisionList> lstCollision)
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<Collision> lstColli = new List<Collision>();
            var collisionReg = lstCollision.FirstOrDefault();
            lstColli.Add(new Collision
            {
                LiabilityId = collisionReg.LiabilityId,
                EmployeeId = collisionReg.EmployeeId,
                LiabilityType = collisionReg.LiabilityType,
                LiabilityDescription = collisionReg.LiabilityDescription,
                ProductId = collisionReg.ProductId,
                Status = collisionReg.Status,
                CreatedDate = collisionReg.CreatedDate,
                IsActive = collisionReg.IsActive

            });
            dynParams.Add("@tvpEmployeeLiability", lstColli.ToDataTable().AsTableValuedParameter("tvpEmployeeLiability"));
            dynParams.Add("@tvpEmployeeLiabilityDetail", collisionReg.LiabilityDetail.ToDataTable().AsTableValuedParameter("tvpEmployeeLiabilityDetail"));
            CommandDefinition cmd = new CommandDefinition(SPEnum.USPSAVECOLLISION.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }
        public bool DeleteCollisionDetails(long id)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@tvpEmployeeLiabilityId", id);
            CommandDefinition cmd = new CommandDefinition(SPEnum.USPDELETECOLLISION.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }
    }
}

