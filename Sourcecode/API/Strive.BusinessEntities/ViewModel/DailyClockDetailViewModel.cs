using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class DailyClockDetailViewModel
    {
        public string EmployeeName { get; set; }
        public DateTime EventDate { get; set; }
        public int HoursPerDay { get; set; }
        public DateTimeOffset InTime { get; set; }
        public DateTimeOffset OutTime { get; set; }
        public string Checked { get; set; }
    }
}
