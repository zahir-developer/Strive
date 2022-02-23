using Strive.BusinessEntities.Model;
using System.Collections.Generic;

namespace Strive.BusinessEntities.DTO.Vehicle
{
    public class VehicleDto
    {
        public Model.ClientVehicle ClientVehicle { get; set; }

        public List<VehicleIssueImage> VehicleImage { get; set; }
    }
}
 