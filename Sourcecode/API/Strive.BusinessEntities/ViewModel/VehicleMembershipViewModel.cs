using Strive.BusinessEntities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class VehicleMembershipViewModel
    {
        public ClientVehicle ClientVehicle { get; set; }

        public ClientVehicleMembershipDetails ClientVehicleMembership { get; set; }

        public List<ClientVehicleMembershipService> ClientVehicleMembershipService { get; set; }

        public InactiveMembershipDetail InactiveMembership { get; set; }
    }
}
