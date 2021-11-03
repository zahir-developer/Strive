using System;
using System.Collections.Generic;

namespace Strive.Core.Models.Owner
{
    public class StatisticRequest
    {
        public int locationId { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string CurrentDate { get; set; }

    }

    public class GetDashboardStatisticsForLocationId
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public int WashesCount { get; set; }
        public int DetailCount { get; set; }
        public int EmployeeCount { get; set; }
        public double? Score { get; set; }
        public string WashTime { get; set; }
        public int Currents { get; set; }
        public int ForecastedCar { get; set; }
        public double WashSales { get; set; }
        public double DetailSales { get; set; }
        public double ExtraServiceSales { get; set; }
        public double MerchandizeSales { get; set; }
        public double? TotalSales { get; set; }
        public double MonthlyClientSales { get; set; }
        public double AverageWashPerCar { get; set; }
        public double AverageDetailPerCar { get; set; }
        public double AverageExtraServicePerCar { get; set; }
        public double AverageTotalPerCar { get; set; }
        public double LabourCostPerCarMinusDetail { get; set; }
        public double DetailCostPerCar { get; set; }
    }

    public class StatisticsData
    {
        public List<GetDashboardStatisticsForLocationId> GetDashboardStatisticsForLocationId { get; set; }
    }
}
