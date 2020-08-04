using System.Collections.Generic;
using Strive.BusinessEntities.DTO;
using Strive.Common;

namespace Strive.BusinessLogic.Location
{
    public interface ILocationBpl
    {
        Result AddLocation(LocationDto location);
        Result UpdateLocation(LocationDto lstLocation);
        Result GetAllLocation();
        Result GetLocationById(int id);
        Result DeleteLocation(int id);
        Result SaveLocation(LocationDto lstLocation);
    }
}
