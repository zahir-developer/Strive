using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class ScheduleTotalWashHoursViewModel
    {
        public decimal? Totalhours { get; set; }

        public DateTime ScheduledDate { get; set; }

        public int? TotalEmployees { get; set; }
    }
}
