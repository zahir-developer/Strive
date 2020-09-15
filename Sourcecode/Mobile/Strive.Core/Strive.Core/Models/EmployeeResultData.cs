using System;
using Strive.Core.Models.TimInventory;

namespace Strive.Core.Models
{
    public class EmployeeResultData
    {
        public EmployeeResultData()
        {
        }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public EmployeeDetails EmployeeDetails { get; set; }
    }
}
