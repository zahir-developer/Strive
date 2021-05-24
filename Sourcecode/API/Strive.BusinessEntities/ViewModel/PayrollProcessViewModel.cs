using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class PayrollProcessViewModel
    {
        public int PayrollProcessId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int PayrollEmployeeId { get; set; }
        public int EmployeeId { get; set; }
        public int LocationId { get; set; }

    }
}
