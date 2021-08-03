using System;
namespace Strive.Core.Models.TimInventory
{
    public class TimeClock
    {
        public int timeClockId { get; set; }
        public int employeeId { get; set; }
        public int locationId { get; set; }
        public int roleId { get; set; }
        public string eventDate { get; set; }
        public string inTime { get; set; }
        public string outTime { get; set; }
        public int? eventType { get; set; }
        public string updatedFrom { get; set; }
        public bool status { get; set; }
        public string comments { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public int createdBy { get; set; }
        public string createdDate { get; set; }
        public int updatedBy { get; set; }
        public string updatedDate { get; set; }
    }
}
