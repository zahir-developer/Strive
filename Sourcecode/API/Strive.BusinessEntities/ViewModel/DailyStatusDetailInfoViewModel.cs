using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class DailyStatusDetailInfoViewModel
    {
        public int LocationId { get; set; }
        public string TicketNumber { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public decimal Commision { get; set; }
    }
}
