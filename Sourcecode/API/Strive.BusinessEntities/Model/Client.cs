using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblClient")]
    public class Client
    {

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int ClientId { get; set; }

        [Column]
        public string FirstName { get; set; }

        [Column]
        public string MiddleName { get; set; }

        [Column]
        public string LastName { get; set; }

        [Column]
        public int? Gender { get; set; }

        [Column]
        public int? MaritalStatus { get; set; }

        [Column]
        public DateTime? BirthDate { get; set; }

        [Column]
        public string Notes { get; set; }

        [Column]
        public string RecNotes { get; set; }

        [Column]
        public int? Score { get; set; }

        [Column]
        public bool? NoEmail { get; set; }

        [Column]
        public int? ClientType { get; set; }

        [Column, IgnoreOnUpdate]
        public int? AuthId { get; set; }

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
        public decimal? Amount { get; set; }

        [Column]
        public bool? IsCreditAccount { get; set; }

        [Column]
        public int? LocationId { get; set; }

    }
}