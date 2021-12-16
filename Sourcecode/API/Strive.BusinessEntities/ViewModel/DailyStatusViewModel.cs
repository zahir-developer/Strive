using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class DailyStatusViewModel
    {
        public List<DailyStatusDetailInfoViewModel> DailyStatusDetailInfo { get; set; }

        public List<DailyStatusWashInfoViewModel> DailyStatusWashInfo { get; set; }

    }
}
