﻿using Strive.BusinessEntities.DTO.Vehicle;
using Strive.BusinessEntities.MembershipSetup;
using Strive.Common;


namespace Strive.BusinessLogic.Vehicle
{
    public interface IVehicleBpl
    {
        Result GetAllVehicle();
        Result GetVehicleMembership();
        Result UpdateVehicleMembership(Membership Membership);
        Result SaveClientVehicle(VehicleDto vehicle);
        Result DeleteVehicle(int vehicleId);
        Result GetClientVehicleById(int clientId);
        Result GetAllCodeType();
    }
}
