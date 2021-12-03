using System;
using System.Collections.Generic;

namespace Strive.Core.Models.Customer
{
    public class ClientDeal
    {
        public List<ClientDealDetail> ClientDealDetail { get; set; }
    }
    
    public class ClientDealDetail
    {
        public int ClientDealId { get; set; }
        public int DealId{ get; set; }
        public string DealName { get; set; }
        public int ClientId { get; set; }
        public int DealCount { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
