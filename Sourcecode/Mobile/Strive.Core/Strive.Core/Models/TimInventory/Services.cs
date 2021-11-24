using System;
using System.Collections.Generic;

namespace Strive.Core.Models.TimInventory
{
    public class ServiceDetail
    {
        public int MembershipServiceId { get; set; }
        public int MembershipId { get; set; }
        public int ServiceId { get; set; }
        public int ServiceTypeId { get; set; }
        public object ServiceType { get; set; }
        public string Upcharges { get; set; }

        public string ServiceName { get; set; }
        public string ServiceTypeName { get; set; }
        public double? Price { get; set; }

        
    }

    public class ServiceList
    {
        public List<ServiceDetail> ServicesWithPrice { get; set; }
    }


}
