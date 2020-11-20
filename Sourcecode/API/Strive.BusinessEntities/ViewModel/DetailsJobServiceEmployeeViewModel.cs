using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class DetailsJobServiceEmployeeViewModel
    {
        public int JobServiceEmployeeId { get; set; }
        public int? JobItemId { get; set; }
        public int? ServiceId { get; set; }
        public string ServiceName { get; set; }
        public decimal? Cost { get; set; }
        public int? EmployeeId { get; set; }
        public decimal? CommissionAmount { get; set; }
        public string EmployeeName { get; set; }

    }
}
