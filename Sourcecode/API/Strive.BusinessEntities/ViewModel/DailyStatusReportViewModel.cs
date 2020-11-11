using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class DailyStatusReportViewModel
    {
        public int Number {get; set;}
        public int JobId { get; set; }
        public string ServiceName { get; set; }
        public int JobTypeId { get; set; }
        public string JobType { get; set; }
        public DateTime JobDate { get; set; }
    }
}
