using Strive.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessLogic.Collission
{
    public interface ICollissionBpl
    {
        //Result GetAllCollission();
        //Result SaveCollison(List<Strive.BusinessEntities.Collision.CollisionListView> lstCollision);
       // Result DeleteCollision(long id);
        Result GetCollissionById(int id);
        //Result GetCollisionByEmpId(long id);
    }
}
