using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class ScheduleItemListViewModel
    {
        public ScheduleItemViewModel ScheduleItemViewModel { get; set; }
        public List<ScheduleItemSummaryViewModel> ScheduleItemSummaryViewModels { get; set; }

    }
}