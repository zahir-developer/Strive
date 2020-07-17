using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Strive.BusinessEntities.Location;
using Strive.Common;
using Strive.ResourceAccess;

namespace Strive.BusinessLogic.Location
{
    public class LocationBpl : Strivebase, ILocationBpl
    {
        readonly ITenantHelper _tenant;
        readonly JObject _resultContent = new JObject();
        Result _result;
        public LocationBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(cache)
        {
            _tenant = tenantHelper;
        }
        public Result GetLocationDetails()
        {
            try
            {
                var lstLocation = new LocationRal(_tenant).GetLocationDetails();
                _resultContent.Add(lstLocation.WithName("Location"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        public Result SaveLocationDetails(List<LocationView> lstLocation)
        {
            try
            {
                bool blnStatus = new LocationRal(_tenant).SaveLocationDetails(lstLocation);
                _resultContent.Add(blnStatus.WithName("Status"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result DeleteLocationDetails(int id)
        {
            try
            {
                var lstLocation = new LocationRal(_tenant).DeleteLocationDetails(id);
                _resultContent.Add(lstLocation.WithName("Location"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        public Result GetLocationById(int id)
        {
            try
            {
                var lstLocation = new LocationRal(_tenant).GetLocationById(id);
                _resultContent.Add(lstLocation.WithName("Location"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        public Result AddLocation(List<LocationView> lstLocation)
        {
            try
            {
                bool blnStatus = new LocationRal(_tenant).AddLocation(lstLocation);
                _resultContent.Add(blnStatus.WithName("Status"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        public Result UpdateLocation(List<LocationView> lstLocation)
        {
            try
            {
                bool blnStatus = new LocationRal(_tenant).UpdateLocation(lstLocation);
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
