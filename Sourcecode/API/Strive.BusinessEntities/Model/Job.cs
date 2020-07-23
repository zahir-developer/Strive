namespace Strive.BusinessEntities.Model
{
    using Cocoon.ORM;
    using System;

    /// <summary>
    /// Defines the <see cref="Job" />.
    /// </summary>
    [OverrideName("tblJob")]
    public class Job
    {
        /// <summary>
        /// Gets or sets the JobId.
        /// </summary>
        [Column, IgnoreOnInsert, IgnoreOnUpdate]
        public int JobId { get; set; }

        /// <summary>
        /// Gets or sets the TicketNumber.
        /// </summary>
        [Column]
        public string TicketNumber { get; set; }

        /// <summary>
        /// Gets or sets the BarCode.
        /// </summary>
        [Column]
        public string BarCode { get; set; }

        /// <summary>
        /// Gets or sets the LocationId.
        /// </summary>
        [Column]
        public int? LocationId { get; set; }

        /// <summary>
        /// Gets or sets the ClientId.
        /// </summary>
        [Column]
        public int? ClientId { get; set; }

        /// <summary>
        /// Gets or sets the JobType.
        /// </summary>
        [Column]
        public int? JobType { get; set; }

        /// <summary>
        /// Gets or sets the VehicleId.
        /// </summary>
        [Column]
        public int? VehicleId { get; set; }

        /// <summary>
        /// Gets or sets the TimeIn.
        /// </summary>
        [Column]
        public DateTimeOffset? TimeIn { get; set; }

        /// <summary>
        /// Gets or sets the EstimatedTimeOut.
        /// </summary>
        [Column]
        public DateTimeOffset? EstimatedTimeOut { get; set; }

        /// <summary>
        /// Gets or sets the ActualTimeOut.
        /// </summary>
        [Column]
        public DateTimeOffset? ActualTimeOut { get; set; }

        /// <summary>
        /// Gets or sets the JobStatus.
        /// </summary>
        [Column]
        public int? JobStatus { get; set; }

        /// <summary>
        /// Gets or sets the CreatedBy.
        /// </summary>
        [Column]
        public int? CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the CreatedDate.
        /// </summary>
        [Column]
        public DateTimeOffset? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedBy.
        /// </summary>
        [Column]
        public int? UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedDate.
        /// </summary>
        [Column]
        public DateTimeOffset? UpdatedDate { get; set; }
    }
}
