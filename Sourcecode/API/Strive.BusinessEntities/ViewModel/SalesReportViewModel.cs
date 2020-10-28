using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class SalesReportViewModel
    {
        public DateTime? JobDate { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public int? EmployeeId { get; set; }
        public int? Number { get; set; }
        public string Total { get; set; }
        
    }
}
