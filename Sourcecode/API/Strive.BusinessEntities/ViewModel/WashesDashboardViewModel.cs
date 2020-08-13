using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class WashesDashboardViewModel
    {
        public WashesCountViewModel WashesCount { get; set; }
        public DetailsCountViewModel DetailsCount { get; set; }
        public EmployeeCountViewModel EmployeeCount { get; set; }
        public ForecastedCarsViewModel ForecastedCars { get; set; }
        public CurrentViewModel Current { get; set; }
        public AverageWashTimeViewModel AverageWashTime { get; set; }
    }
}
