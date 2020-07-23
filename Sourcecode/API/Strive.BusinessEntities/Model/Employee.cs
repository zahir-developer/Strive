namespace Strive.BusinessEntities.Model
{
    using Cocoon.ORM;
    using System;

    /// <summary>
    /// Defines the <see cref="Employee" />.
    /// </summary>
    [OverrideName("tblEmployee")]
    public class Employee
    {
        /// <summary>
        /// Gets or sets the EmployeeId.
        /// </summary>
        [Column, IgnoreOnInsert, IgnoreOnUpdate]
        public int EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the FirstName.
        /// </summary>
        [Column]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the MiddleName.
        /// </summary>
        [Column]
        public string MiddleName { get; set; }

        /// <summary>
        /// Gets or sets the LastName.
        /// </summary>
        [Column]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the Gender.
        /// </summary>
        [Column]
        public int? Gender { get; set; }

        /// <summary>
        /// Gets or sets the SSNo.
        /// </summary>
        [Column]
        public string SSNo { get; set; }

        /// <summary>
        /// Gets or sets the MaritalStatus.
        /// </summary>
        [Column]
        public int? MaritalStatus { get; set; }

        /// <summary>
        /// Gets or sets the IsCitizen.
        /// </summary>
        [Column]
        public bool? IsCitizen { get; set; }

        /// <summary>
        /// Gets or sets the AlienNo.
        /// </summary>
        [Column]
        public string AlienNo { get; set; }

        /// <summary>
        /// Gets or sets the BirthDate.
        /// </summary>
        [Column]
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Gets or sets the ImmigrationStatus.
        /// </summary>
        [Column]
        public int? ImmigrationStatus { get; set; }

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
