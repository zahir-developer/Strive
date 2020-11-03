using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class DashboardGridViewModel
    {
        public int? WashesCount { get; set; }
        public int? DetailCount { get; set; }
        public int? EmployeeCount { get; set; }
        public int? Score { get; set; }
        public string WashTime { get; set; }
        public int? Currents { get; set; }
        public int? ForecastedCar { get; set; }
        public decimal? WashSales { get; set; }
        public decimal? DetailSales { get; set; }
        public decimal? ExtraServiceSales { get; set; }
        public decimal? MerchandizeSales { get; set; }
        public decimal? TotalSales { get; set; }
        public int? MonthlyClientSales { get; set; }
        public int? AverageWashPerCar { get; set; }
        public int? AverageDetailPerCar { get; set; }
        public int? AverageExtraServicePerCar { get; set; }
        public int? AverageTotalPerCar { get; set; }
        public int? LabourCostPerCarMinusDetail { get; set; }
        public int? DetailCostPerCar { get; set; }
    }
}
