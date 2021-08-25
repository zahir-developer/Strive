using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class DashboardGridViewModel
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public int? WashesCount { get; set; }
        public int? DetailCount { get; set; }
        public int? EmployeeCount { get; set; }
        public decimal? Score { get; set; }
        public decimal? WashTime { get; set; }
        public int? Currents { get; set; }
        public int? ForecastedCar { get; set; }
        public decimal? WashSales { get; set; }
        public decimal? DetailSales { get; set; }
        public decimal? ExtraServiceSales { get; set; }
        public decimal? MerchandizeSales { get; set; }
        public decimal? TotalSales { get; set; }
        public decimal? MonthlyClientSales { get; set; }
        public decimal? AverageWashPerCar { get; set; }
        public decimal? AverageDetailPerCar { get; set; }
        public decimal? AverageExtraServicePerCar { get; set; }
        public decimal? AverageTotalPerCar { get; set; }
        public decimal? LabourCostPerCarMinusDetail { get; set; }
        public decimal? DetailCostPerCar { get; set; }
    }
}
