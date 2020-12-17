using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Employee.MyTickets
{
    public class AllTickets
    {
       public List<Washes> Washes { get; set; }
    }

    public class Washes
    {
        public int JobId { get; set; }
        public string TicketNumber { get; set; }
        public string ClientName { get; set; }
        public string PhoneNumber { get; set; }
        public string VehicleName { get; set; }
        public string TimeIn { get; set; }
        public string EstimatedTimeOut { get; set; }
        public string ServiceName { get; set; }
    }
}
