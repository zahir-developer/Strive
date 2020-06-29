using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Strive.BusinessEntities
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string MiddleName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        public int? Gender { get; set; }
        [MaxLength(50)]
        public string SSNo { get; set; }
        public int? MaritalStatus { get; set; }
        public bool? IsCitizen { get; set; }
        [MaxLength(50)]
        public string AlienNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? BirthDate { get; set; }
        public int? ImmigrationStatus { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }

        public List<EmployeeAddress> EmployeeAddress { get; set; }
        public EmployeeDetail EmployeeDetail { get; set; }

        public List<EmployeeRole> EmployeeRole { get; set; }
    }
}
