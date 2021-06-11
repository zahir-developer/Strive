using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.ServiceSetup
{
    public class ServiceViewModel
    {
        public int ServiceId { get; set; }
        public string ServiceTypeId { get; set; }
        public string CommissionTypeId { get; set; }
        public string ParentServiceId { get; set; }       
        public string ServiceType { get; set; }
        public string CommisionType { get; set; }
        public bool? Commision { get; set; }
        public decimal? CommissionCost { get; set; }
        public string Upcharges { get; set; }
        public string ServiceName { get; set; }
        public decimal? Cost { get; set; }
        public decimal? Price { get; set; }
        public int LocationId { get; set; }

        public bool? IsActive { get; set; }
        public string Description { get; set; }
        public string DiscountType { get; set; }
        public string DiscountServiceType { get; set; }
        public bool? IsCeramic { get; set; }
        public int? ServiceCategory { get; set; }

        public string Hours { get; set; }


    }
}
