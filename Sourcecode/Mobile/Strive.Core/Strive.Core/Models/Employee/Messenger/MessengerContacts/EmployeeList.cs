using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Employee.Messenger.MessengerContacts
{
    public class EmployeeList
    {
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string CommunicationId { get; set; }
        public bool Collisions { get; set; }
        public bool Documents { get; set; }
        public bool Schedules { get; set; }
        public bool Status { get; set; }
    }
}
