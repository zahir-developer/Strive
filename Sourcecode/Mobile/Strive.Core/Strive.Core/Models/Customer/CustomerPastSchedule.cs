using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Customer
{
    public class CustomerPastSchedule
    {
        public int scheduleId { get; set; }
        public int employeeId { get; set; }
        public int locationId { get; set; }
        public int roleId { get; set; }
        public DateTime scheduledDate { get; set; }
        public DateTimeOffset startTime { get; set; }
        public DateTimeOffset endTime { get; set; }
        public int? scheduleType { get; set; }
        public string comments { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public int createdBy { get; set; }
        public string createdDate { get; set; }
        public int updatedBy { get; set; }
        public string updatedDate { get; set; }

    }
}
