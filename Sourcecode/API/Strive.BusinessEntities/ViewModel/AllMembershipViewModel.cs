using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class AllMembershipViewModel
    {
        public int MembershipId { get; set; }
        public string MembershipName { get; set; }
        public decimal Price { get; set; }
        public string Notes { get; set; }
        public string Services { get; set; }
        public int LocationId { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public decimal? DiscountedPrice { get; set; }
    }
}
