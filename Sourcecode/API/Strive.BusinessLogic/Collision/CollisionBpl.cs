using Microsoft.Extensions.Caching.Distributed;
using Strive.BusinessEntities.DTO.Collision;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Strive.BusinessLogic.Collision
{
    public class CollisionBpl : Strivebase, ICollisionBpl
    {
        public CollisionBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache)
        {
        }
        public Result GetAllCollision()
        {
            return ResultWrap(new CollisionRal(_tenant).GetAllCollision, "Collision");
        }

        public Result GetCollisionById(int id)
        {
            return ResultWrap(new CollisionRal(_tenant).GetCollisionById, id, "Collision");
        }
        public Result GetCollisionByEmpId(int id)
        {
            return ResultWrap(new CollisionRal(_tenant).GetCollisionByEmpId, id, "Collision");
        }
        public Result DeleteCollision(int id)
        {
            return ResultWrap(new CollisionRal(_tenant).DeleteCollision, id, "Status");
        }
        public Result AddCollision(CollisionDto collission)
        {
            return ResultWrap(new CollisionRal(_tenant).AddCollision, collission, "Status");
        }

        public Result UpdateCollision(CollisionDto collission)
        {
            return ResultWrap(new CollisionRal(_tenant).UpdateCollision, collission, "Status");
        }

    }
}
