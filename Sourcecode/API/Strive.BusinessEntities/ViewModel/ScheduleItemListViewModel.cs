using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class ScheduleItemListViewModel
    {
        public List<ScheduleItemViewModel> ScheduleItemViewModel { get; set; }
        public List<JobProductItemViewModel> ProductItemViewModel { get; set; }
        public ScheduleItemSummaryViewModel ScheduleItemSummaryViewModels { get; set; }
        public JobPaymentViewModel JobPaymentViewModel { get; set; }

    }
}