using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class BayScheduleDetailsViewModel
    {
        public int BayId { get; set; }
        public int JobId { get; set; }
        public string ScheduleInTime { get; set; }
    }
}
