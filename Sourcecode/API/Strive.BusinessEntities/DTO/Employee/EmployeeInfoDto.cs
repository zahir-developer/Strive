﻿using System;

namespace Strive.BusinessEntities.DTO.Employee
{
    public class EmployeeInfoDto
    {
        public int EmployeeId { get; set; }
        public int EmployeeDetailId { get; set; }
        public string EmployeeCode { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public int? Gender { get; set; }
        public string Address1 { get; set; }
        public string PhoneNumber { get; set; }
        public int? ImmigrationStatus { get; set; }
        public string SSNo { get; set; }
        public string Email { get; set; }
        public DateTime? HiredDate { get; set; }
        public string PayRate { get; set; }
        public string ComRate { get; set; }
        public bool Status { get; set; }
        public string Tip { get; set; }
        public int? Exemptions { get; set; }
    }
}