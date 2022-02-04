using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblEmployeeDetail")]
    public class EmployeeDetail
    {

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int EmployeeDetailId { get; set; }

        [Column, PrimaryKey]
        public int? EmployeeId { get; set; }

        [Column]
        public string EmployeeCode { get; set; }

        [Column]
        public int AuthId { get; set; }

        [Column]
        public int ComType { get; set; }

        [Column]
        public string WashRate { get; set; }

        [Column]
        public string DetailRate { get; set; }

        [Column]
        public string SickRate { get; set; }

        [Column]
        public string VacRate { get; set; }

        [Column]
        public string ComRate { get; set; }

        [Column]
        public DateTime? HiredDate { get; set; }

        [Column]
        public decimal? Salary { get; set; }

        [Column]
        public string Tip { get; set; }

        [Column]
        public DateTime? LRT { get; set; }

        [Column]
        public string Exemptions { get; set; }

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
        public bool? IsSalary { get; set; }

    }
}