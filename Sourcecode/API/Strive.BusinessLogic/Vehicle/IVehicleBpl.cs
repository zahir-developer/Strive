using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.Vehicle;
using Strive.BusinessEntities.Model;
using Strive.BusinessEntities.ViewModel;
using Strive.Common;


namespace Strive.BusinessLogic.Vehicle
{
    public interface IVehicleBpl
    {
        Result GetAllVehicle();
        Result GetVehicleMembership();
        Result UpdateVehicleMembership(Membership Membership);
        Result UpdateClientVehicle(ClientVehicle ClientVehicle);
        Result SaveClientVehicle(VehicleDto vehicle);
        Result DeleteVehicle(int vehicleId);
        Result GetVehicleByClientId(int clientId);
        Result GetVehicleId(int vehicleId);
        Result GetVehicleCodes();
    }
}
