using System;
using System.Collections.Generic;

namespace Strive.Core.Models.TimInventory
{
    public class MembershipServices
    {
        public int MembershipId { get; set; }
        public string MembershipName { get; set; }
        public double Price { get; set; }
        public string Notes { get; set; }
        public string Services { get; set; }
        public int LocationId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public double? DiscountedPrice { get; set; }


    }

    public class MembershipServiceList
    {
        public List<MembershipServices> Membership { get; set; }
        

    }

    
    public class SelectedServiceList
    {
        public List<ServiceDetail> MembershipDetail { get; set; }
    }
}
