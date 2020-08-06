using Strive.BusinessEntities.Client;
using Strive.BusinessEntities.Model;

namespace Strive.BusinessEntities.DTO
{
    public class VehicleDto
    {
        public Model.Client Client { get; set; }
        public ClientVehicle ClientVehicle { get; set; }
    }
}
