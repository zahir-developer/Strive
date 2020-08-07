using Strive.BusinessEntities.DTO.Collision;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessLogic.Collission
{
    public interface ICollissionBpl
    {
        Result GetAllCollission();
        Result GetCollissionById(int id);
        Result GetCollissionByEmpId(int id);
        Result DeleteCollission(int id);
        Result AddCollission(CollissionDto collission);
        Result UpdateCollission(CollissionDto collission);
    }
}
