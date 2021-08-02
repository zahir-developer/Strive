using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblJob")]
    public class Job
    {

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int JobId { get; set; }

        [Column]
        public Int64 TicketNumber { get; set; }

        [Column]
        public int LocationId { get; set; }

        [Column, PrimaryKey]
        public int? ClientId { get; set; }

        [Column]
        public int? VehicleId { get; set; }

        [Column]
        public int? Make { get; set; }

        [Column]
        public int? Model { get; set; }

        [Column]
        public int? Color { get; set; }

        [Column]
        public int? JobType { get; set; }

        [Column, PrimaryKey]
        public DateTime JobDate { get; set; }

        [Column]
        public DateTimeOffset? TimeIn { get; set; }

        [Column]
        public DateTimeOffset? EstimatedTimeOut { get; set; }

        [Column]
        public DateTimeOffset? ActualTimeOut { get; set; }

        [Column]
        public int? JobStatus { get; set; }

        [Column]
        public bool? IsActive { get; set; }

        [Column]
        public bool? IsDeleted { get; set; }

        [Column]
        public int? CreatedBy { get; set; }

        [Column]
        public DateTimeOffset? CreatedDate { get; set; }

        [Column]
        public int? UpdatedBy { get; set; }

        [Column]
        public DateTimeOffset? UpdatedDate { get; set; }

        [Column]
        public string Notes { get; set; }

        [Column]
        public DateTime? CheckOutTime { get; set; }

        [Column]
        public int? JobPaymentId { get; set; }

        [Column]
        public bool? IsHold { get; set; }
        [Column]
        public string BarCode { get; set; }

    }
}