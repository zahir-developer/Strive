using Microsoft.Extensions.Caching.Distributed;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.Vehicle;
using Strive.BusinessEntities.Model;
using Strive.BusinessEntities.ViewModel;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Net;

namespace Strive.BusinessLogic.Vehicle
{
    public class VehicleBpl : Strivebase, IVehicleBpl
    {
        public VehicleBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper) { }

        public Result GetAllVehicle()
        {
            return ResultWrap(new VehicleRal(_tenant).GetAllVehicle, "Vehicle");
        }
        public Result GetVehicleMembership()
        {
            return ResultWrap(new VehicleRal(_tenant).GetVehicleMembership, "VehicleMembership");
        }
        public Result UpdateVehicleMembership(Membership Membership)
        {
            return ResultWrap(new VehicleRal(_tenant).UpdateVehicleMembership, Membership, "Status");
        }
        public Result UpdateClientVehicle(ClientVehicle ClientVehicle)
        {
            return ResultWrap(new VehicleRal(_tenant).UpdateClientVehicle, ClientVehicle, "Status");
        }

        public Result SaveClientVehicle(VehicleDto vehicle)
        {
            try
            {
                return ResultWrap(new VehicleRal(_tenant).SaveVehicle, vehicle, "Status");
            }
            catch(Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        public Result DeleteVehicle(int vehicleId)
        {
            return ResultWrap(new VehicleRal(_tenant).DeleteVehicleById, vehicleId, "Status");
        }
        public Result GetVehicleByClientId(int clientId)
        {
            return ResultWrap(new VehicleRal(_tenant).GetVehicleByClientId, clientId, "Status");
        }
        public Result GetVehicleId(int vehicleId)
        {
            return ResultWrap(new VehicleRal(_tenant).GetVehicleId, vehicleId, "Status");
        }
        public Result GetVehicleCodes()
        {
            return ResultWrap(new VehicleRal(_tenant).GetVehicleCodes, "VehicleDetails");
        }
       
    }
}
