namespace Strive.BusinessEntities.Model
{
    using Cocoon.ORM;
    using System;

    /// <summary>
    /// Defines the <see cref="EmployeeDetail" />.
    /// </summary>
    [OverrideName("tblEmployeeDetail")]
    public class EmployeeDetail
    {
        /// <summary>
        /// Gets or sets the EmployeeDetailId.
        /// </summary>
        [Column, IgnoreOnInsert, IgnoreOnUpdate]
        public int EmployeeDetailId { get; set; }

        /// <summary>
        /// Gets or sets the EmployeeId.
        /// </summary>
        [Column]
        public int? EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the EmployeeCode.
        /// </summary>
        [Column]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Gets or sets the AuthId.
        /// </summary>
        [Column]
        public int AuthId { get; set; }

        /// <summary>
        /// Gets or sets the LocationId.
        /// </summary>
        [Column]
        public int LocationId { get; set; }

        /// <summary>
        /// Gets or sets the PayRate.
        /// </summary>
        [Column]
        public string PayRate { get; set; }

        /// <summary>
        /// Gets or sets the SickRate.
        /// </summary>
        [Column]
        public string SickRate { get; set; }

        /// <summary>
        /// Gets or sets the VacRate.
        /// </summary>
        [Column]
        public string VacRate { get; set; }

        /// <summary>
        /// Gets or sets the ComRate.
        /// </summary>
        [Column]
        public string ComRate { get; set; }

        /// <summary>
        /// Gets or sets the HiredDate.
        /// </summary>
        [Column]
        public DateTimeOffset? HiredDate { get; set; }

        /// <summary>
        /// Gets or sets the Salary.
        /// </summary>
        [Column]
        public string Salary { get; set; }

        /// <summary>
        /// Gets or sets the Tip.
        /// </summary>
        [Column]
        public string Tip { get; set; }

        /// <summary>
        /// Gets or sets the LRT.
        /// </summary>
        [Column]
        public DateTime? LRT { get; set; }

        /// <summary>
        /// Gets or sets the Exemptions.
        /// </summary>
        [Column]
        public short? Exemptions { get; set; }

        /// <summary>
        /// Gets or sets the IsActive.
        /// </summary>
        [Column]
        public bool? IsActive { get; set; }

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
