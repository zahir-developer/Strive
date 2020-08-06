using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Strive.BusinessEntities.Client;
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
        public VehicleBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper) { }

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
        public Result SaveClientVehicle(List<ClientVehicle> vehicle)
        {
            try
            {
                var res = new VehicleRal(_tenant).SaveVehicle(vehicle);
                _resultContent.Add(res.WithName("SaveVehicle"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch(Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result DeleteVehicle(int id)
        {
            try
            {
                var res = new VehicleRal(_tenant).DeleteVehicleById(id);
                _resultContent.Add(res.WithName("DeleteVehicle"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch(Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result GetClientVehicleById(int id)
        {
            try
            {
                var res = new VehicleRal(_tenant).GetVehicleById(id);
                _resultContent.Add(res.WithName("GetVehicle"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch(Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        
    }
}
