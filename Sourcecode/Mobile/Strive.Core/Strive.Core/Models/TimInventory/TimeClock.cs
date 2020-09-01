using System;
namespace Strive.Core.Models.TimInventory
{
    public class TimeClock
    {
        public int id { get; set; }
        public int userId { get; set; }
        public int locationId { get; set; }
        public int roleId { get; set; }
        public string eventDate { get; set; }
        public string inTime { get; set; }
        public string outTime { get; set; }
        public int eventType { get; set; }
        public int updatedBy { get; set; }
        public string updatedFrom { get; set; }
        public string updatedDate { get; set; }
        public bool status { get; set; }
        public string comments { get; set; }
    }
}
