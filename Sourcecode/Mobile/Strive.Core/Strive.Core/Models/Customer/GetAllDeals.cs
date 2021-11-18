using System;
using System.Collections.Generic;

namespace Strive.Core.Models.Customer
{
    public class GetAllDeal
    {
        public int DealId { get; set; }
        public string DealName { get; set; }
        public int TimePeriod { get; set; }
        public bool Deals { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }

    public class DealsList
    {
        public List<GetAllDeal> GetAllDeals { get; set; }
    }
}
