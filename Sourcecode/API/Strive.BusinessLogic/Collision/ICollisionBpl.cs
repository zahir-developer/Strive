using Strive.BusinessEntities.DTO.Collision;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessLogic.Collision
{
    public interface ICollisionBpl
    {
        Result GetAllCollision();
        Result GetCollisionById(int id);
        Result GetCollisionByEmpId(int id);
        Result DeleteCollision(int id);
        Result AddCollision(CollisionDto collission);
        Result UpdateCollision(CollisionDto collission);
        Result GetVehicleListByClientId(int id);
    }
}
