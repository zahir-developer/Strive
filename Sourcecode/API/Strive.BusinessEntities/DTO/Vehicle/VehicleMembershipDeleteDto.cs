using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.Vehicle
{
    public class VehicleMembershipDeleteDto
    {
        public int ClientMembershipId { get; set; }

        public int ClientId { get; set; }

        public int VehicleId { get; set; }
    }
}
