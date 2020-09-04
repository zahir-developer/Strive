using Strive.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessLogic.Collision
{
    public interface ICollisionBpl
    {
        //Result GetAllCollission();
        //Result SaveCollison(List<Strive.BusinessEntities.Collision.CollisionListView> lstCollision);
       // Result DeleteCollision(long id);
        Result GetCollisionById(int id);
        //Result GetCollisionByEmpId(long id);
    }
}
