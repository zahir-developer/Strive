using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Customer
{
    public class CustomerPersonalInfo
    {
        public List<Status> Status { get; set; }
    }
    public class Status
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        public int MaritalStatus { get; set; }
        public DateTime BirthDate { get; set; }
        public string Notes { get; set; }
        public string RecNotes { get; set; }
        public int Score { get; set; }
        public bool NoEmail { get; set; }
        public int ClientType { get; set; }
        public int ClientAddressId { get; set; }
        public int ClientRelatioshipId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumber2 { get; set; }
        public string Email { get; set; }
        public int City { get; set; }
        public int State { get; set; }
        public int Country { get; set; }
        public string Zip { get; set; }
        public bool IsActive { get; set; }
    }
}
