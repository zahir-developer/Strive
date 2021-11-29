using System;
using System.Collections.Generic;

namespace Strive.Core.Models.Customer.Schedule
{
    public class GetTicketNumber
    {
        public string TicketNumber { get; set; }
        public int JobId { get; set; }
    }

    public class ticketNumber
    {
        public GetTicketNumber GetTicketNumber { get; set; }
    }

    public class modelUpcharge
    {
        public int upchargeServiceType { get; set; }
        public int modelId { get; set; }
    }

    public class upchargeList
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string Upcharges { get; set; }
        public double Price { get; set; }
        public int ServiceTypeId { get; set; }
    }
    
    public class modelUpchargeResponse
    {
        public List<upchargeList> upcharge { get; set; }
    }
}
