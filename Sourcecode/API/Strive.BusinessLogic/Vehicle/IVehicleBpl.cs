using Strive.BusinessEntities.Client;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.MembershipSetup;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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

    }
}
