using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Strive.BusinessEntities;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Strive.BusinessLogic
{
    public class LocationBpl : Strivebase, ILocationBpl
    {
        ITenantHelper tenant;
        JObject resultContent = new JObject();
        Result result;
        public LocationBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(cache)
        {
            tenant = tenantHelper;
        }
        public Result GetLocationDetails()
        {
            try
            {
                var lstLocation = new LocationRal(tenant).GetLocationDetails();
                resultContent.Add(lstLocation.WithName("Location"));
                result = Helper.BindSuccessResult(resultContent);
            }
            catch (Exception ex)
            {
                result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return result;
        }

        public Result SaveLocationDetails(List<Location> lstLocation)
        {
            try
            {
                bool blnStatus = new LocationRal(tenant).SaveLocationDetails(lstLocation);
                resultContent.Add(blnStatus.WithName("Status"));
                result = Helper.BindSuccessResult(resultContent);
            }
            catch (Exception ex)
            {
                result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return result;
        }
        public Result DeleteLocationDetails(int id)
        {
            try
            {
                var lstLocation = new LocationRal(tenant).DeleteLocationDetails(id);
                resultContent.Add(lstLocation.WithName("Location"));
                result = Helper.BindSuccessResult(resultContent);
            }
            catch (Exception ex)
            {
                result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return result;
        }
    }
}
