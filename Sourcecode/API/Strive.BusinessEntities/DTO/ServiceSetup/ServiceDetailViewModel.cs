using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.ServiceSetup
{
    public class ServiceDetailViewModel
    {
        public int ServiceId { get; set; }
        public int  DiscountServiceType { get; set; }
        public string ServiceName { get; set; }
        public int ServiceTypeId { get; set; }
        public string ServiceTypeName { get; set; }
        public string Upcharges { get; set; }
        public decimal Price { get; set; }
        public string DiscountType { get; set; }
        public bool IsCeramic { get; set; }

        public decimal? EstimatedTime { get; set; }


    }
}
