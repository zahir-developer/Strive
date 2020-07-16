using Strive.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessLogic.Collision
{
    public interface ICollisionBpl
    {
        Result GetAllCollison();
        Result SaveCollison(List<Strive.BusinessEntities.Collision.CollisionList> lstCollision);
        Result DeleteCollision(long id);
        Result GetCollisionById(long id);
    }
}
