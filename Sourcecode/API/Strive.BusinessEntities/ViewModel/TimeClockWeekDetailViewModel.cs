using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class TimeClockWeekDetailViewModel
    {
        public List<TimeClockViewModel> TimeClock { get; set; }

        public TimeClockWeekSummary TimeClockWeek { get; set; }
    }
}
