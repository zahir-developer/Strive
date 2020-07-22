using System;
using Strive.Core.Models.TimInventory;

namespace Strive.Core.Utils.TimInventory
{
    public static class EmployeeData
    {
        public static EmployeeDetails EmployeeDetails { get; set; }

        public static string CurrentRole { get; set; } = "Washer";

        public static DateTime ClockInTime { get; set; } = DateTime.Now;

        public static DateTime ClockOutTime { get; set; } = DateTime.Now;
    }
}
