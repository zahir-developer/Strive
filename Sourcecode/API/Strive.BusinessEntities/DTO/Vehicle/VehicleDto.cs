using Strive.BusinessEntities.Model;

namespace Strive.BusinessEntities.DTO.Vehicle
{
    public class VehicleDto
    {
        public Model.Client Client { get; set; }
        public ClientVehicle ClientVehicle { get; set; }
    }
}
