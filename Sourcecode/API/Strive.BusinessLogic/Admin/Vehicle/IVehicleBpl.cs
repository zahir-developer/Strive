using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.Vehicle;
using Strive.BusinessEntities.Model;
using Strive.BusinessEntities.ViewModel;
using Strive.Common;


namespace Strive.BusinessLogic.Vehicle
{
    public interface IVehicleBpl
    {
        Result GetAllVehicle(SearchDto searchDto);
        Result GetVehicleMembership();
        Result UpdateVehicleMembership(Membership Membership);
        Result AddVehicle(VehicleDto ClientVehicle);
        Result SaveClientVehicle(VehicleDto vehicle);
        Result DeleteVehicle(int vehicleId);
        Result GetVehicleByClientId(int clientId);
        Result GetVehicleId(int vehicleId);
        Result GetVehicleCodes();
        Result SaveClientVehicleMembership(ClientVehicleMembershipDetailModel clientmembership);
        Result GetVehicleMembershipDetailsByVehicleId(int id);
        Result GetMembershipDetailsByVehicleId(int id);
        Result GetPastDetails(int clientId);
        Result GetAllVehicleThumbnail(int vehicleId);
        Result GetVehicleImageById(int vehicleImageId);
        Result DeleteVehicleImage(int vehicleImageId);
        Result GetMembershipDiscountStatus(int clientId);

    }
}
