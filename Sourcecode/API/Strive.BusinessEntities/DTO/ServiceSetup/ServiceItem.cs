using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.ServiceSetup
{
    public class ServiceItem
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int ServiceType { get; set; }
        public string ServiceTypeName { get; set; }
        public string Upcharges { get; set; }
        public decimal? Price { get; set; }

    }
}
