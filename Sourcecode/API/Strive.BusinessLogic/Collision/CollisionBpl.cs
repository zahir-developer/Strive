using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
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
        readonly ITenantHelper _tenant;
        readonly JObject _resultContent = new JObject();
        Result _result;
        public CollisionBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(cache)
        {
            _tenant = tenantHelper;
        }
        public Result GetAllCollison()
        {
            try
            {
                var lstCollision = new CollisionRal(_tenant).GetAllCollison();
                _resultContent.Add(lstCollision.WithName("Collision"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result GetCollisionById(long id)
        {
            try
            {
                var lstCollisionById = new CollisionRal(_tenant).GetCollisionById(id);
                _resultContent.Add(lstCollisionById.WithName("CollisionDetail"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result SaveCollison(List<Strive.BusinessEntities.Collision.Collision> lstCollision)
        {
            try
            {
                bool blnStatus = new CollisionRal(_tenant).SaveCollison(lstCollision);
                _resultContent.Add(blnStatus.WithName("Status"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result DeleteCollision(long collisionId)
        {
            try
            {
                var lstCollision = new CollisionRal(_tenant).DeleteCollisionDetails(collisionId);
                _resultContent.Add(lstCollision.WithName("Collision"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

    }
}

