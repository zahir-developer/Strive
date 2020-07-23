namespace Strive.BusinessEntities.Model
{
    using Cocoon.ORM;
    using System;

    /// <summary>
    /// Defines the <see cref="JobDetail" />.
    /// </summary>
    [OverrideName("tblJobDetail")]
    public class JobDetail
    {
        /// <summary>
        /// Gets or sets the JobDetailId.
        /// </summary>
        [Column, IgnoreOnInsert, IgnoreOnUpdate]
        public int JobDetailId { get; set; }

        /// <summary>
        /// Gets or sets the JobId.
        /// </summary>
        [Column]
        public int? JobId { get; set; }

        /// <summary>
        /// Gets or sets the BayId.
        /// </summary>
        [Column]
        public int? BayId { get; set; }

        /// <summary>
        /// Gets or sets the SalesRep.
        /// </summary>
        [Column]
        public int? SalesRep { get; set; }

        /// <summary>
        /// Gets or sets the QABy.
        /// </summary>
        [Column]
        public int? QABy { get; set; }

        /// <summary>
        /// Gets or sets the Labour.
        /// </summary>
        [Column]
        public int? Labour { get; set; }

        /// <summary>
        /// Gets or sets the Review.
        /// </summary>
        [Column]
        public int? Review { get; set; }

        /// <summary>
        /// Gets or sets the ReviewNote.
        /// </summary>
        [Column]
        public string ReviewNote { get; set; }

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
