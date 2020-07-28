using System.Collections.Generic;
using Strive.BusinessEntities.Location;
using Strive.Common;

namespace Strive.BusinessLogic.Location
{
    public interface ILocationBpl
    {
        Result GetLocationDetails();
        Result SaveLocationDetails(LocationDto lstLocation);
        Result DeleteLocationDetails(int id);
        Result GetLocationById(int id);
        Result AddLocation(LocationDto location);
        Result UpdateLocation(LocationDto lstLocation);

    }
}
