using Strive.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Customer.Schedule
{
    public class AddDetailsModel
    {
        public job job { get; set; }
        public jobDetail jobDetail { get; set; }
        public baySchedule baySchedule { get; set; }
        public List<jobItem> jobItem { get; set; }
    }

    public class job
    {
        public int jobId { get; set; }
        public string ticketNumber { get; set; }
        public int locationId { get; set; }
        public int clientId { get; set; }
        public int vehicleId { get; set; }
        public int make { get; set; }
        public int model { get; set; }
        public int color { get; set; }
        public int jobType { get; set; }
        public string jobDate { get; set; } = DateUtils.ConvertDateTimeWithZ();
        public string timeIn { get; set; } = DateUtils.ConvertDateTimeWithZ();
        public string estimatedTimeOut { get; set; } = DateUtils.ConvertDateTimeWithZ();
        public string actualTimeOut { get; set; } = DateUtils.ConvertDateTimeWithZ();
        public int jobStatus { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public int createdBy { get; set; }
        public string createdDate { get; set; } = DateUtils.ConvertDateTimeWithZ();
        public int updatedBy { get; set; }
        public string updatedDate { get; set; } = DateUtils.ConvertDateTimeWithZ();
        public string notes { get; set; }
        public string checkOutTime { get; set; } = DateUtils.ConvertDateTimeWithZ();
        public int jobPaymentId { get; set; }
        public bool isHold { get; set; }


    }

    public class jobDetail
    {
        public int jobDetailId { get; set; }
        public int jobId { get; set; }
        public int bayId { get; set; }
        public int salesRep { get; set; }
        public int qaBy { get; set; }
        public int review { get; set; }
        public string reviewNote { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public int createdBy { get; set; }
        public string createdDate { get; set; } = DateUtils.ConvertDateTimeWithZ();
        public int updatedBy { get; set; }
        public string updatedDate { get; set; } = DateUtils.ConvertDateTimeWithZ();

    }

    public class baySchedule
    {
        public int bayScheduleID { get; set; }
        public int bayId { get; set; }
        public int jobId { get; set; }
        public string scheduleDate { get; set; } = DateUtils.ConvertDateTimeWithZ();
        public string scheduleInTime { get; set; }
        public string scheduleOutTime { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public int createdBy { get; set; }
        public string createdDate { get; set; } = DateUtils.ConvertDateTimeWithZ();
        public int updatedBy { get; set; }
        public string updatedDate { get; set; } = DateUtils.ConvertDateTimeWithZ();

    }
    public class jobItem
    {
        public int jobItemId { get; set; }
        public int jobId { get; set; }
        public int serviceId { get; set; }
        public int itemTypeId { get; set; }
        public int employeeId { get; set; }
        public int commission { get; set; }
        public int commissionType { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }
        public string reviewNote { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public int createdBy { get; set; }
        public string createdDate { get; set; } = DateUtils.ConvertDateTimeWithZ();
        public int updatedBy { get; set; }
        public string updatedDate { get; set; } = DateUtils.ConvertDateTimeWithZ();
    }
}
