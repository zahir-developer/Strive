using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class EmployeeAllLocationPayCheckViewModel
    {

        public int LocationId { get; set; }
        public int EmployeeCount { get; set; }
        public decimal? TotalWashHours { get; set; }
        public decimal? BonusAmt { get; set; }
        public decimal? BonusPerHour { get; set; }

    }
}
