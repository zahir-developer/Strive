using Cocoon.ORM;
using System;
using System.Collections.Generic;

namespace Strive.BusinessEntities.Employee
{
    [OverrideName("tblEmployeeDetail")]
    public class EmployeeDetail
    {
        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int EmployeeDetailId { get; set; }
        [Column]
        public int EmployeeId { get; set; }
        [Column]
        public string EmployeeCode { get; set; }
        [Column]
        public int AuthId { get; set; }
        [Column]
        public int LocationId { get; set; }
        [Column]
        public string PayRate { get; set; }
        [Column]
        public string SickRate { get; set; }
        [Column]
        public string VacRate { get; set; }
        [Column]
        public string ComRate { get; set; }
        [Column]
        public DateTime? HiredDate { get; set; }
        [Column]
        public string Salary { get; set; }
        [Column]
        public string Tip { get; set; }
        [Column]
        public DateTime LRT { get; set; }
        [Column]
        public short Exemptions { get; set; }
        [Column]
        public bool IsActive { get; set; }
    }
}
