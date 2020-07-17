using System;
namespace Strive.Core.Models.TimInventory
{
    public class ResultData
    {
        public ResultData()
        {
        }

        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public EmployeeDetails EmployeeDetails { get; set; }
    }
}
