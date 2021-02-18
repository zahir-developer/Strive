using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Employee.PersonalDetails
{
    public class UpdatePersonalDetails
    {
        public employee employee { get; set; }
        public employeeLocation employeeLocation { get; set; } = null;
        public employeeDetail employeeDetail { get; set; } = null;
        public employeeAddress employeeAddress { get; set; } = null;
        public employeeDocument employeeDocument { get; set; } = null;
        public employeeLiability employeeLiability { get; set; } = null;
        public employeeRole employeeRole { get; set; } = null;
    }
    public class employee
    {
        public  int employeeId { get; set; }
        public  string firstName { get; set; }
        public  string middleName { get; set; }
        public  string lastName { get; set; }
        public  int gender { get; set; }
        public  string ssNo { get; set; }
        public  int maritalStatus { get; set; }
        public  bool isCitizen { get; set; }
        public  string alienNo { get; set; }
        public  string birthDate { get; set; }
        public string workPermit { get; set; }
        public  int immigrationStatus { get; set; }
        public  bool isActive { get; set; }
        public  bool isDeleted { get; set; }
        public  int createdBy { get; set; }
        public  string createdDate { get; set; }
        public  int updatedBy { get; set; }
        public  string updatedDate { get; set; }
    }
    public class employeeLocation
    {
        public int employeeLocationId { get; set; }
        public int employeeId { get; set; }
        public int locationId { get; set; }
        public  bool isActive { get; set; }
        public  bool isDeleted { get; set; }
        public  int createdBy { get; set; }
        public  string createdDate { get; set; }
        public  int updatedBy { get; set; }
        public  string updatedDate { get; set; }

    }
    public class employeeDetail
    { }
    public class employeeAddress
    {
        public int employeeAddressId { get; set; }
        public int employeeId { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string phoneNumber { get; set; }
        public string phoneNumber2 { get; set; }
        public string email { get; set; }
        public int city { get; set; }
        public int state { get; set; }
        public string zip { get; set; }
        public int country { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public int createdBy { get; set; }
        public string createdDate { get; set; }
        public int updatedBy { get; set; }
        public string updatedDate { get; set; }


    }
    public class employeeDocument
    { }
    public class employeeLiability
    { }
    public class employeeRole
    { }
}
