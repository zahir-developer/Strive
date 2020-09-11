using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class BaySchedulesDetails
    {
        public List<BayListViewModel> BayList { get; set; }

        public List<BayScheduleDetailsViewModel> BayScheduleDetails { get; set; }

    }
}
