using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Customer.Schedule
{
    public class AvailableServicesModel
    {
        public List<AllServiceDetail> AllServiceDetail { get; set; }

    }
    public class AllServiceDetail
    {
        public int ServiceId { get; set; }
        public int DiscountServiceTypeId { get; set; }
        public string ServiceName { get; set; }
        public int ServiceTypeId { get; set; }
        public string ServiceTypeName { get; set; }
        public string Upcharges { get; set; }
        public float Price { get; set; }
        public string DiscountType { get; set; }
        public double? EstimatedTime { get; set; }
    }

}
