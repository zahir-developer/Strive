using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class WashItemViewModel
    {
        public int JobItemId { get; set; }
        public int? JobId { get; set; }
        public int? ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int? ServiceTypeId { get; set; }
        public string ServiceType { get; set; }
        public Decimal? Commission { get; set; }
        public Decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public string ReviewNote { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedDate { get; set; }
    }
}
