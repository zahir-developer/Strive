using Strive.BusinessEntities.DTO.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class MoneyOwedReportDetailViewModel
    {
        public List<MonthlyMoneyOwedReportDetailViewModel> MoneyOwedReport { get; set; }

        public List<ClientNameViewModel> Client { get; set; }

        public List<LocationNameViewModel> Location { get; set; }
    }
}
