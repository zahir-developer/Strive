using System;
namespace Strive.Core.Models.Employee.CheckList
{
    public class ChecklistRequest
    {
        public int role { get; set; }
        public string notificationDate { get; set; }
        public int EmployeeId { get; set; }
    }

}
