namespace Strive.BusinessEntities.Model
{
    using Cocoon.ORM;
    using System;

    /// <summary>
    /// Defines the <see cref="Client" />.
    /// </summary>
    [OverrideName("tblClient")]
    public class Client
    {
        /// <summary>
        /// Gets or sets the ClientId.
        /// </summary>
        [Column, IgnoreOnInsert, IgnoreOnUpdate]
        public int ClientId { get; set; }

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
        /// Gets or sets the MaritalStatus.
        /// </summary>
        [Column]
        public int? MaritalStatus { get; set; }

        /// <summary>
        /// Gets or sets the BirthDate.
        /// </summary>
        [Column]
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Gets or sets the IsActive.
        /// </summary>
        [Column]
        public bool? IsActive { get; set; }

        /// <summary>
        /// Gets or sets the Notes.
        /// </summary>
        [Column]
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the RecNotes.
        /// </summary>
        [Column]
        public string RecNotes { get; set; }

        /// <summary>
        /// Gets or sets the Score.
        /// </summary>
        [Column]
        public int? Score { get; set; }

        /// <summary>
        /// Gets or sets the NoEmail.
        /// </summary>
        [Column]
        public bool? NoEmail { get; set; }

        /// <summary>
        /// Gets or sets the ClientType.
        /// </summary>
        [Column]
        public int? ClientType { get; set; }

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
