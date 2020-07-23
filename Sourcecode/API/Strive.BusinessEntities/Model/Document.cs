namespace Strive.BusinessEntities.Model
{
    using Cocoon.ORM;
    using System;

    /// <summary>
    /// Defines the <see cref="Document" />.
    /// </summary>
    [OverrideName("tblDocument")]
    public class Document
    {
        /// <summary>
        /// Gets or sets the DocumentId.
        /// </summary>
        [Column, IgnoreOnInsert, IgnoreOnUpdate]
        public int DocumentId { get; set; }

        /// <summary>
        /// Gets or sets the EmployeeId.
        /// </summary>
        [Column]
        public int? EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the Filename.
        /// </summary>
        [Column]
        public string Filename { get; set; }

        /// <summary>
        /// Gets or sets the Filepath.
        /// </summary>
        [Column]
        public string Filepath { get; set; }

        /// <summary>
        /// Gets or sets the Password.
        /// </summary>
        [Column]
        public string Password { get; set; }

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
