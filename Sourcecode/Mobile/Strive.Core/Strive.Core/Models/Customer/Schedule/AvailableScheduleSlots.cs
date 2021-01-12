using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Customer.Schedule
{
    public class AvailableScheduleSlots
    {
        public GetTimeInDetails GetTimeInDetails { get; set; }
    }
    public class GetTimeInDetails
    {
        public int BayId { get; set; }
        public string TimeIn { get; set; }
    }
}
