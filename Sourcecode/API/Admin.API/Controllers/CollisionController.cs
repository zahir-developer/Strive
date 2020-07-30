using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class CollisionController: ControllerBase
    {
        ICollisionBpl _collisionBpl = null;

        public CollisionController(ICollisionBpl collisionBpl)
        {
            _collisionBpl = collisionBpl;
        }

        [HttpGet]
        [Route("GetAll")]
        public Result GetAllCollison()
        {
            return _collisionBpl.GetAllCollison();
        }

        [HttpGet]
        [Route("GetCollisionById/{id}")]
        public Result GetCollisionById(long id)
        {
            return _collisionBpl.GetCollisionById(id);
        }
        [HttpGet]
        [Route("GetCollisionByEmpId/{id}")]
        public Result GetCollisionByEmpId(long id)
        {
            return _collisionBpl.GetCollisionByEmpId(id);
        }

        [HttpPost]
        [Route("Save")]
        public Result SaveCollison([FromBody] List<Strive.BusinessEntities.Collision.CollisionListView> lstCollision)
        {
            return _collisionBpl.SaveCollison(lstCollision);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public Result DeleteCollision(long id)
        {
            return _collisionBpl.DeleteCollision(id);
        }

    }
}
