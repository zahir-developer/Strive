using System;
namespace Strive.Core.Models.TimInventory
{
    public class EmployeeLoginBase
    {
        public EmployeeLoginBase()
        {
        }

        public string status { get; set; }
        public int statusCode { get; set; }
        public object exception { get; set; }
        public ResultData resultData { get; set; }

    }
}
