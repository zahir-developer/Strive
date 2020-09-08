using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.DTO.Collision;
using Strive.BusinessLogic.Collision;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.API.Controllers
{
    [Authorize]
    [Route("Admin/[Controller]")]
    public class CollisionController : StriveControllerBase<ICollisionBpl>
    {
        public CollisionController(ICollisionBpl colBpl) : base(colBpl) { }
        #region GET
        [HttpGet]
        [Route("GetAll")]
        public Result GetAllCollission() => _bplManager.GetAllCollision();
        #endregion
        #region
        [HttpGet]
        [Route("GetCollisionById/{id}")]
        public Result GetCollisionById(int id) => _bplManager.GetCollisionById(id);
        #endregion
        #region
        [HttpGet]
        [Route("GetCollisionByEmpId/{id}")]
        public Result GetCollisionByEmpId(int id) => _bplManager.GetCollisionByEmpId(id);
        #endregion
        #region
        [HttpDelete]
        [Route("Delete")]
        public Result DeleteCollission(int id) => _bplManager.DeleteCollision(id);
        #endregion
        [HttpPost]
        [Route("Add")]
        public Result Add([FromBody] CollisionDto collission) => _bplManager.AddCollision(collission);
        #region
        [HttpPost]
        [Route("Update")]
        public Result Update([FromBody] CollisionDto collission) => _bplManager.UpdateCollision(collission);
        #endregion
        #region
        [HttpGet]
        [Route("GetVehicleListByClientId/{id}")]
        public Result GetVehicleListByClientId(int id) => _bplManager.GetVehicleListByClientId(id);
        #endregion

    }
}
