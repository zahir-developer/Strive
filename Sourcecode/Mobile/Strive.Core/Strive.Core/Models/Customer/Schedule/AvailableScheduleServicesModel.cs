using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Customer.Schedule
{
    public class AvailableScheduleServicesModel
    {
        public List<ServicesWithPrice> ServicesWithPrice { get; set; }
    }
    public class ServicesWithPrice
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int ServiceType { get; set; }
        public string ServiceTypeName { get; set; }
        public string Upcharges { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
    }
}
