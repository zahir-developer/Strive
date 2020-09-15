using System.Collections.Generic;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.Location;
using Strive.Common;

namespace Strive.BusinessLogic.Location
{
    public interface ILocationBpl
    {
        Result AddLocation(LocationDto location);
        Result UpdateLocation(LocationDto lstLocation);
        Result DeleteLocation(int id);
        Result GetSearchResult(LocationSearchDto search);
        Result GetAllLocation();
        Result GetLocationById(int id);
    }
}
