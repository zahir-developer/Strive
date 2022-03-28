using Strive.BusinessEntities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.Model
{
    public class ClientVehicleModel
    {
        public ClientVehicle ClientVehicle { get; set; }
        public List<VehicleIssueImage> VehicleImage { get; set; }
    }
}
