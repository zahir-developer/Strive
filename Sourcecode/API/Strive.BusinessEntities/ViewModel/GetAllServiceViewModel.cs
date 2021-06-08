using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class GetAllServiceViewModel
    {
        public int ServiceId { get; set; }
        public string ServiceTypeId { get; set; }
        public string ServiceType { get; set; }
        public string ServiceName { get; set; }
        public string Upcharges { get; set; }
        public decimal? Price { get; set; }
        public decimal? Cost { get; set; }
        public bool? IsActive { get; set; }
        public int? DiscountServiceType  {get;set;}

        public TimeSpan? Hours { get; set; }
    }
}
