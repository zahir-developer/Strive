using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Strive.BusinessEntities.Location;
using Strive.Common;
using Strive.ResourceAccess;
using System.Linq;
using Strive.BusinessLogic.Common;

namespace Strive.BusinessLogic.Location
{
    public class LocationBpl : Strivebase, ILocationBpl
    {
        public LocationBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache) { }

        public Result GetLocationDetails()
        {
            return ResultWrap(new LocationRal(_tenant).GetLocationDetails, "Location");
        }

        public Result GetLocationById(int id)
        {
            return ResultWrap(new LocationRal(_tenant).GetLocationById, id, "Location");
        }

        public Result DeleteLocationDetails(int id)
        {
            return ResultWrap(new LocationRal(_tenant).DeleteLocationDetails, id, "Location");
        }

        public Result AddLocation(LocationDto location)
        {
            return ResultWrap(new LocationRal(_tenant).AddLocation, location, "Status");
        }

        public Result UpdateLocation(LocationDto location)
        {
            return ResultWrap(new LocationRal(_tenant).UpdateLocation, location, "Status");
        }

        public Result SaveLocationDetails(LocationDto location)
        {
            try
            {
                CommonBpl commonBpl = new CommonBpl(_cache, _tenant);
                var lstGeocode = commonBpl.GetGeocode(location.LocationAddress);

                bool blnStatus = new LocationRal(_tenant).SaveLocationDetails(location);
                _resultContent.Add(blnStatus.WithName("Status"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

    }
}
