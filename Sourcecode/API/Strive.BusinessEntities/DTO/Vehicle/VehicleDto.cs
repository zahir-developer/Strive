using Strive.BusinessEntities.Model;
using System.Collections.Generic;

namespace Strive.BusinessEntities.DTO.Vehicle
{
    public class VehicleDto
    {
        public Model.Client Client { get; set; }
        public List<ClientVehicle> ClientVehicle { get; set; }
    }
}
