using Strive.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessLogic.Collision
{
    public interface ICollisionBpl
    {
        Result GetAllCollison();
        Result SaveCollison(List<Strive.BusinessEntities.Collision.Collision> lstCollision);
        Result DeleteCollision(long collisionId);
        Result GetCollisionById(long id);
    }
}
