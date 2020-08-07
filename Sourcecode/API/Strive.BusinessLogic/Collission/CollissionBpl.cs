using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Strive.BusinessEntities.DTO.Collision;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Strive.BusinessLogic.Collission
{
    public class CollissionBpl : Strivebase, ICollissionBpl
    {
        public CollissionBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper,cache)
        {
        }
        public Result GetAllCollission()
        {
           return ResultWrap(new CollissionRal(_tenant).GetAllCollission, "Collision");
        }

        public Result GetCollissionById(int id)
        {
            return ResultWrap(new CollissionRal(_tenant).GetCollissionById, id, "Collision");
        }
        public Result GetCollissionByEmpId(int id)
        {
            return ResultWrap(new CollissionRal(_tenant).GetCollissionByEmpId, id, "Collision");
        }
        public Result DeleteCollission(int id)
        {
            return ResultWrap(new CollissionRal(_tenant).DeleteCollission, id, "Status");
        }
        public Result AddCollission(CollissionDto collission)
        {
            return ResultWrap(new CollissionRal(_tenant).AddCollission, collission, "Status");
        }

        public Result UpdateCollission(CollissionDto collission)
        {
            return ResultWrap(new CollissionRal(_tenant).UpdateCollission, collission, "Status");
        }
        
    }
}

