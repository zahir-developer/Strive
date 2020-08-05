using System;
using Strive.Core.Models.TimInventory;

namespace Strive.Core.Utils.TimInventory
{
    public static class EmployeeData
    {
        public static EmployeeDetails EmployeeDetails { get; set; }

        public static string CurrentRole { get; set; }

        public static TimeClock ClockInStatus { get; set; }
    }
}
