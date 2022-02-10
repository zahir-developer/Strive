using System;
using System.Collections.Generic;

namespace Strive.Core.Models.Employee.Detailer
{
    public class status
    {
        public List<DetailerData> Status { get; set; }
    }

    public class DetailerData
    {
        public string TicketNumber { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleColor { get; set; }
        public string VehicleMake { get; set; }
        public string TimeIn { get; set; }
        public string EstimatedTimeOut { get; set; }
        public string AdditionalService { get; set; }
        public string DetailService { get; set; }
    }
}
