using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.Model
{
    public class ClientVehicleMembershipDetailModel
    {
        public ClientVehicleModel ClientVehicle { get; set; }

        public ClientVehicleMembershipModel ClientVehicleMembershipModel { get; set; }

        public int? DeletedClientMembershipId { get; set; }
    }
}
