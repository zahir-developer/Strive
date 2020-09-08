using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.Model
{
    public class ClientVehicleMembershipModel
    {
        public ClientVehicleMembershipDetails ClientVehicleMembershipDetails { get; set; }

        public List<ClientVehicleMembershipService> ClientVehicleMembershipService { get; set; }
    }
}
