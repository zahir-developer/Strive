using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.Vehicle
{
    public class VehicleBpl : Strivebase, IVehicleBpl
    {
        readonly ITenantHelper _tenant;
        readonly JObject _resultContent = new JObject();
        Result _result;
        public VehicleBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(cache)
        {
            _tenant = tenantHelper;
        }

        public Result GetAllVehicle()
        {
            try
            {
                var list = new VehicleRal(_tenant).GetVehicleDetails();
                _resultContent.Add(list.WithName("Vehicle"));
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
