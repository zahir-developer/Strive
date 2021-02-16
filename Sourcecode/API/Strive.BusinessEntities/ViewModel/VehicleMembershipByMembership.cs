using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class VehicleMembershipByMembership
    {
        public int MembershipId { get; set; }
        public int LocationId { get; set; }
        public int ClientMembershipId { get; set; }
        public int ClientVehicleId { get; set; }
        public DateTime StartDate { get; set; }
    }
}
