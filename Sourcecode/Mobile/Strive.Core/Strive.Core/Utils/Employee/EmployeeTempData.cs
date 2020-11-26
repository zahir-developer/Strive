using Strive.Core.Models.Employee.Messenger.MessengerContacts;
using Strive.Core.Models.Employee.PersonalDetails;
using System;
using System.Collections.Generic;
using System.Text;
using EmployeeList = Strive.Core.Models.Employee.Messenger.EmployeeList;
namespace Strive.Core.Utils.Employee
{
    // Employee Temp Data
    public static class EmployeeTempData
    {
        public static int EmployeeID { get; set; }
        public static string MessengerTabHeader { get; set; }
        public static PersonalDetails EmployeePersonalDetails { get; set; }
    }

    // Messenger Temp Data
    public static class MessengerTempData
    {
        #region Properties
        public static int ClickAction { get; set; } = 0;
        public static string RecipientName { get; set; }
        public static int RecipientID { get; set; }
        public static int GroupID { get; set; }
        public static string GroupUniqueID { get; set; }
        public static string GroupName { get; set; }
        public static bool IsGroup { get; set; }
        public static bool IsCreateGroup { get; set; } = false;
        public static Dictionary<int, int> ChatParticipants { get; set; }
        public static EmployeeLists SelectedParticipants { get; set; }
        public static EmployeeList ExistingParticipants { get; set; }
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
            GroupName = "";
            GroupUniqueID = "";
        }

        public static void resetParticipantInfo()
        {
            SelectedParticipants = null;
            ExistingParticipants = null;
            ChatParticipants = null;
            IsCreateGroup = false;
        }

        #endregion Commands
    }


    // My Profile Temp Data
    public static class MyProfileTempData
    {
        public static int LiabilityID { get; set; }

    }
}
