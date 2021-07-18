using System;
using System.Collections.Generic;

namespace Strive.Core.Models.Owner
{
    public class ScheduleRequest
    {        
        public string startDate { get; set; }
        public string endDate { get; set; }
        public int locationId { get; set; }
    }

    public class ScheduleDetailViewModel
    {
        public int ScheduleId { get; set; }
        public int EmployeeId { get; set; }
        public object RoleId { get; set; }
        public object EmployeeRole { get; set; }
        public string EmployeeName { get; set; }
        public object ColorCode { get; set; }
        public string LocationName { get; set; }
        public int LocationId { get; set; }
        public bool IsEmployeeAbscent { get; set; }
        public string ScheduledDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int ScheduleType { get; set; }
        public object Comments { get; set; }
    }

    public class ScheduleHoursViewModel
    {
        public double Totalhours { get; set; }
        public string ScheduledDate { get; set; }
        public int? TotalEmployees { get; set; }
    }

    public class ScheduleEmployeeViewModel
    {
        public int TotalEmployees { get; set; }
        public string ScheduledDate { get; set; }
    }

    public class ScheduleDetail
    {
        public List<ScheduleDetailViewModel> ScheduleDetailViewModel { get; set; }
        public ScheduleHoursViewModel ScheduleHoursViewModel { get; set; }
        public ScheduleEmployeeViewModel ScheduleEmployeeViewModel { get; set; }
    }

    public class employeeSchedule
    {
        public ScheduleDetail ScheduleDetail { get; set; }
    }
}
