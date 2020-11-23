using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Employee.Collisions
{
    public class AddCollisions
    {
        public employeeLiability employeeLiability { get; set; }
        public employeeLiabilityDetail employeeLiabilityDetail { get; set; }
    }
    public class employeeLiability
    {
        public int liabilityId { get; set; }
        public int employeeId { get; set; }
        public int liabilityType { get; set; }
        public string liabilityDescription { get; set; }
        public int productId { get; set; }
        public float totalAmount { get; set; }
        public int status { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public int createdBy { get; set; }
        public string createdDate { get; set; }
        public int updatedBy { get; set; }
        public string updatedDate { get; set; }
        public string vehicleId { get; set; }
        public int? clientId { get; set; }
    }
    public class employeeLiabilityDetail
    {
        public int liabilityDetailId { get; set; }
        public int liabilityId { get; set; }
        public int liabilityDetailType { get; set; }
        public float amount { get; set; }
        public int paymentType { get; set; }
        public string documentPath { get; set; }
        public string description { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public int createdBy { get; set; }
        public string createdDate { get; set; }
        public int updatedBy { get; set; }
        public string updatedDate { get; set; }
    }
}

