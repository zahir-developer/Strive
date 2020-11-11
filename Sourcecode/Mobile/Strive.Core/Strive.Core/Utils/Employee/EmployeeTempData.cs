using Strive.Core.Models.Employee.Messenger.MessengerContacts;
using System;
using System.Collections.Generic;
using System.Text;
using EmployeeList = Strive.Core.Models.Employee.Messenger.EmployeeList;
namespace Strive.Core.Utils.Employee
{
    public static class EmployeeTempData
    {
        public static int EmployeeID { get; set; }
        public static string MessengerTabHeader { get; set; }
    }
    public static class MessengerTempData
    {
        #region Properties
        public static int ClickAction { get; set; } = 0;
        public static string RecipientName { get; set; }
        public static int RecipientID { get; set; }
        public static int GroupID { get; set; }
        public static bool IsGroup { get; set; }
        public static Dictionary<int, int> ChatParticipants { get; set; }
        public static EmployeeLists SelectedParticipants { get; set; }
        public static EmployeeLists EmployeeLists { get; set; }
        public static EmployeeList RecentEmployeeLists { get; set; }
        public static EmployeeList GroupLists { get; set; }

        #endregion Properties

        #region Commands

        public static void resetChatData()
        {
            RecipientID = 0;
            GroupID = 0;
            IsGroup = false;
            RecipientName = "";
        }

        #endregion Commands
    }
}
