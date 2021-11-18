using System.Collections.Generic;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.Location;
using Strive.BusinessEntities.DTO.MembershipSetup;
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
        //Result SaveLocation(LocationDto lstLocation);
        Result GetLocationSearch(LocationSearchDto search);

        Result AddLocationOffset(LocationOffsetDto locationOffset);

        Result UpdateLocationOffset(LocationOffsetDto locationOffset);
        Result GetAllLocationOffset();

        Result DeleteLocationOffset(int id);

        Result GetAllLocationName();
        Result GetMerchantDetails(MerchantSearch search);
    }
}
