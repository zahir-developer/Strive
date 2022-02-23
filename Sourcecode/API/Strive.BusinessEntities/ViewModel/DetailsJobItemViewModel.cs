using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class DetailsJobItemViewModel
    {
        public int JobItemId { get; set; }
        public int JobId { get; set; }
        public int ServiceId { get; set; }
        public int ServiceTypeId { get; set; }
        public string CommissionType { get; set; }
        public decimal? CommissionCost { get; set; }
        public string ServiceName { get; set; }
        public string ServiceType { get; set; }
        public decimal? Cost { get; set; }
        public decimal? Price { get; set; }
        public bool? IsCeramic { get; set; }

    }
}
