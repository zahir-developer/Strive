using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
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
        //public Result GetAllCollission()
        //{
        //    return ResultWrap(new CollissionRal(_tenant).GetAllCollission, "Collision");
        //}

        public Result GetCollissionById(int id)
        {
            return ResultWrap(new CollissionRal(_tenant).GetCollissionById, id, "Collision");
        }
        //public Result GetAllCollison()
        //{
        //    try
        //    {
        //        var lstCollision = new CollisionRal(_tenant).GetAllCollison();
        //        _resultContent.Add(lstCollision.WithName("Collision"));
        //        _result = Helper.BindSuccessResult(_resultContent);
        //    }
        //    catch (Exception ex)
        //    {
        //        _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
        //    }
        //    return _result;
        //}
        //public Result GetCollisionById(long id)
        //{
        //    try
        //    {
        //        var lstCollisionById = new CollisionRal(_tenant).GetCollisionById(id);
        //        _resultContent.Add(lstCollisionById.WithName("CollisionDetail"));
        //        _result = Helper.BindSuccessResult(_resultContent);
        //    }
        //    catch (Exception ex)
        //    {
        //        _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
        //    }
        //    return _result;
        //}
        //public Result GetCollisionByEmpId(long id)
        //{
        //    try
        //    {
        //        var lstCollisionByEmpId = new CollisionRal(_tenant).GetCollisionByEmpId(id);
        //        _resultContent.Add(lstCollisionByEmpId.WithName("CollisionDetailOfEmployee"));
        //        _result = Helper.BindSuccessResult(_resultContent);
        //    }
        //    catch (Exception ex)
        //    {
        //        _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
        //    }
        //    return _result;
        //}
        //public Result SaveCollison(List<Strive.BusinessEntities.Collision.CollisionListView> lstCollision)
        //{
        //    try
        //    {
        //        bool blnStatus = new CollisionRal(_tenant).SaveCollison(lstCollision);
        //        _resultContent.Add(blnStatus.WithName("Status"));
        //        _result = Helper.BindSuccessResult(_resultContent);
        //    }
        //    catch (Exception ex)
        //    {
        //        _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
        //    }
        //    return _result;
        //}
        //public Result DeleteCollision(long id)
        //{
        //    try
        //    {
        //        var lstCollision = new CollisionRal(_tenant).DeleteCollisionDetails(id);
        //        _resultContent.Add(lstCollision.WithName("Collision"));
        //        _result = Helper.BindSuccessResult(_resultContent);
        //    }
        //    catch (Exception ex)
        //    {
        //        _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
        //    }
        //    return _result;
        //}

    }
}

