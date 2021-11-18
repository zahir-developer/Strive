using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Employee.PersonalDetails
{
    public class PersonalDetails
    {
        public Employee Employee { get; set; }
    }
    public class Employee
    {
        public EmployeeInfo EmployeeInfo { get; set; }
        public List<EmployeeDocument> EmployeeDocument { get; set; }
        public List<EmployeeCollision> EmployeeCollision { get; set; }
        public List<EmployeeRoles> EmployeeRoles { get; set; }
        public List<EmployeeLocations> EmployeeLocations { get; set; }
        public string errorMessage { get; set; }
    }
    public class EmployeeInfo
    {
        public int EmployeeId { get; set; }
        public int EmployeeDetailId { get; set; }
        public int EmployeeAddressId { get; set; }
        public string EmployeeCode { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public int AuthId { get; set; }
        public int? Gender { get; set; }
        public string Address1 { get; set; }
        public string PhoneNumber { get; set; }
        public int ImmigrationStatus { get; set; }
        public string AlienNo { get; set; }
        public string WorkPermit { get; set; }
        public string SSNo { get; set; }
        public string Email { get; set; }
        public string HiredDate { get; set; }
        public string WashRate { get; set; }
        public string DetailRate { get; set; }
        public string ComRate { get; set; }
        public int? ComType { get; set; }
        public bool Status { get; set; }
        public string Tip { get; set; }
        public int Exemptions { get; set; }
    }
    public class EmployeeDocument
    {
        public int DocumentSequence { get; set; }
        public int EmployeeId { get; set; }
        public int EmployeeDocumentId { get; set; }
        public string FileName { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
    }
    public class EmployeeCollision
    {
        public int CollisionSequence { get; set; }
        public int EmployeeId { get; set; } 
        public int LiabilityId { get; set; }
        public int liabilityDetailId { get; set; }
        public string LiabilityType { get; set; }
        public string LiabilityDetailType { get; set; }
        public string LiabilityDescription { get; set; }
        public double Amount { get; set; }
        public string LiabilityDetailTypeId { get; set; }
        public string CreatedDate { get; set; }
    }
    public class EmployeeRoles
    {
        public int RoleMasterId { get; set; }
        public int EmployeeId { get; set; }
        public int EmployeeRoleId { get; set; }
        public int Roleid { get; set; }
        public string RoleName { get; set; }
    }
    public class EmployeeLocations
    {
        public int EmployeeLocationId { get; set; }
        public int EmployeeId { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
    }
}
