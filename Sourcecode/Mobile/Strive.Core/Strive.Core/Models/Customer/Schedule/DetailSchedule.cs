using System;
using System.Collections.Generic;

namespace Strive.Core.Models.Customer.Schedule
{
    public class DetailSchedule
    {       
        public Job job { get; set; }
        public List<JobItem> jobItem { get; set; }
        public JobDetail jobDetail { get; set; }
        public List<BaySchedule> BaySchedule { get; set; }        
    }

    public class Job
    {
        public int jobId { get; set; }
        public int ticketNumber { get; set; }
        public string barcode { get; set; }
        public string locationId { get; set; }
        public int clientId { get; set; }
        public int vehicleId { get; set; }
        public int make { get; set; }
        public int model { get; set; }
        public int color { get; set; }
        public int jobType { get; set; }
        public string jobDate { get; set; }
        public int jobStatus { get; set; }
        public DateTime timeIn { get; set; }
        public DateTime estimatedTimeOut { get; set; }
        public DateTime actualTimeOut { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public int createdBy { get; set; }
        public int updatedBy { get; set; }
        public string notes { get; set; }
    }

    public class JobItem
    {
        public int jobItemId { get; set; }
        public int jobId { get; set; }
        public int serviceId { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public int commission { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }
        public int createdBy { get; set; }
        public int updatedBy { get; set; }
    }

    public class JobDetail
    {
        public int jobDetailId { get; set; }
        public int jobId { get; set; }
        public int bayId { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public int createdBy { get; set; }
        public int updatedBy { get; set; }
    }

    public class BaySchedule
    {
        public int bayScheduleId { get; set; }
        public int bayId { get; set; }
        public int jobId { get; set; }
        public string scheduleDate { get; set; }
        public string scheduleInTime { get; set; }
        public string scheduleOutTime { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public int createdBy { get; set; }
        public int updatedBy { get; set; }
    }
}
