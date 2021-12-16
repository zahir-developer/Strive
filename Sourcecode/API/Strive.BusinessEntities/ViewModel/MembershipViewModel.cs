using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class MembershipViewModel
    {
        public int MembershipId { get; set; }
        public string MembershipName { get; set; }
        public decimal? Price { get; set; }
        public string Notes { get; set; }
        public int ServiceId { get; set; }
        public int LocationId { get; set; }
        public int ClientMembershipId { get; set; }
        public int ClientVehicleId { get; set; }
        public DateTime StartDate { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public DateTime EndDate { get; set; }
        public bool Status { get; set; }
    }
}
