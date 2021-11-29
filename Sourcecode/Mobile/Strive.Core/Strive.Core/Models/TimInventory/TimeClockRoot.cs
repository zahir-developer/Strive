using System;
using System.Collections.Generic;

namespace Strive.Core.Models.TimInventory
{
    public class TimeClockRoot
    {
        public TimeClock TimeClock { get; set; }
    }

    public class TimeClockRootList
    {
        public List<TimeClock> timeClock { get; set; }
    }

    public class TimeClockSave
    {
        public TimeClockRootList timeClock { get; set; }
    }


    public class TimeClockRequest
    {
        public int locationId { get; set; }
        public int employeeId { get; set; }
        public int roleId { get; set; }
        public string date { get; set; }
    }
}
