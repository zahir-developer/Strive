using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class MembershipServiceViewModel
    {
        public int MembershipServiceId { get; set; }
        public int MembershipId { get; set; }
        public int ServiceId { get; set; }
        public int ServiceTypeId { get; set; }
        public string ServiceType { get; set; }
        public string Upcharges { get; set; }
        public decimal? Price { get; set; }

    }
}
