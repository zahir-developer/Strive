using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class WashesDashboardViewModel
    {
        public int? WashesCount { get; set; }
        public int? DetailsCount { get; set; }
        public int? EmployeeCount { get; set; }
        public int? ForecastedCars { get; set; }
        public int? Current { get; set; }
        public decimal? Score { get; set; }
        public int? AverageWashTime { get; set; }
        public int? Washercount { get; set; }
        public int? DetailerCount { get; set; }
        public int? ForecastedEmployeeHours { get; set; }
    }
}
