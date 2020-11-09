using Strive.Core.Models.Employee.Messenger.MessengerContacts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Utils.Employee
{
    public static class EmployeeTempData
    {
        public static int EmployeeID { get; set; }
        public static string MessengerTabHeader { get; set; }
    }
    public static class MessengerTempData
    {
        public static string RecipientName { get; set; }
        public static int RecipientID { get; set; }
        public static int GroupID { get; set; }
        public static EmployeeLists EmployeeLists { get; set; }
    }
}
