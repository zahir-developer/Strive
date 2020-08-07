using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.DTO.Collision;
using Strive.BusinessLogic.Collission;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.API.Controllers
{
    [Authorize]
    [Route("Admin/[Controller]")]
    public class CollissionController: StriveControllerBase<ICollissionBpl>
    {
        public CollissionController(ICollissionBpl colBpl) : base(colBpl) { }
        #region GET
        [HttpGet]
        [Route("GetAll")]
        public Result GetAllCollission() => _bplManager.GetAllCollission();
        #endregion
        #region
        [HttpGet]
        [Route("GetCollisionById/{id}")]
        public Result GetCollisionById(int id) => _bplManager.GetCollissionById(id);
        #endregion
        #region
        [HttpGet]
        [Route("GetCollisionByEmpId/{id}")]
        public Result GetCollisionByEmpId(int id) => _bplManager.GetCollissionByEmpId(id);
        #endregion
        #region
        [HttpDelete]
        [Route("Delete")]
        public Result DeleteCollission(int id) => _bplManager.DeleteCollission(id);
        #endregion
        [HttpPost]
        [Route("Add")]
        public Result Add([FromBody] CollissionDto collission) => _bplManager.AddCollission(collission);
        #region
        [HttpPost]
        [Route("Update")]
        public Result Update([FromBody] CollissionDto collission) => _bplManager.UpdateCollission(collission);
        #endregion
    }
}
