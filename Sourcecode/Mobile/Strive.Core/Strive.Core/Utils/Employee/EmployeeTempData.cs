using Strive.Core.Models.Employee.Messenger.MessengerContacts;
using Strive.Core.Models.Employee.Messenger.MessengerContacts.Contacts;
using Strive.Core.Models.Employee.PersonalDetails;
using Strive.Core.Models.TimInventory;
using System;
using System.Collections.Generic;
using System.Text;
using EmployeeList = Strive.Core.Models.Employee.Messenger.EmployeeList;
using EmployeeList_Contact = Strive.Core.Models.Employee.Messenger.MessengerContacts.Contacts.EmployeeList;
namespace Strive.Core.Utils.Employee
{
    // Employee Temp Data
    public static class EmployeeTempData
    {
        public static int EmployeeID { get; set; }
        public static  string LocationName { get; set; }
        public static int LocationId { get; set; }
        public static string MessengerTabHeader { get; set; }
        public static PersonalDetails EmployeePersonalDetails { get; set; }
        public static List<EmployeeLocation> employeeLocationdata { get; set; }
        public static List<EmployeeRoleApi> EmployeeRoles { get; set; }
        public static int EmployeeRole { get; set; }
        public static bool FromNotification { get; set; }
        public static void ResetAll()
        {
            MessengerTempData.FirstName = null;
            MessengerTempData.LastName = null;
            MessengerTempData.resetChatData();
            MessengerTempData.resetParticipantInfo();
            MessengerTempData.EmployeeLists = null;
            MessengerTempData.RecentEmployeeLists = null;
            MessengerTempData.GroupLists = null;
        }
    }

    // Messenger Temp Data
    public static class MessengerTempData
    {
        #region Properties
        public static int ClickAction { get; set; } = 0;
        public static string SenderConnectionID { get; set; }
        public static string FirstName { get; set; }
        public static string LastName { get; set; }
        public static string RecipientName { get; set; }
        public static int RecipientID { get; set; }
        public static int GroupID { get; set; }
        public static string GroupUniqueID { get; set; }
        public static string ConnectionID { get; set; }
        public static string GroupName { get; set; }
        public static bool IsGroup { get; set; }
        public static int ContactsCount { get; set; }
        public static bool IsCreateGroup { get; set; } = false;
        public static Dictionary<int, int> ChatParticipants { get; set; }
        public static EmployeeMessengerContacts SelectedParticipants { get; set; }
        public static EmployeeMessengerContacts ExistingParticipants { get; set; }
        public static EmployeeLists EmployeeLists { get; set; }
        public static EmployeeList RecentEmployeeLists { get; set; }
        public static EmployeeList GroupLists { get; set; }
        public static EmployeeMessengerContacts employeeList_Contact { get; set; }
        public static EmployeeMessengerContacts createGroup_Contact { get; set; }
        public static Dictionary<string, string> RecipientsConnectionID { get; set; }

        #endregion Properties

        #region Commands

        public static void resetChatData()
        {
            RecipientID = 0;
            GroupID = 0;
            IsGroup = false;
            RecipientName = null;
            GroupName = null;
            GroupUniqueID = null;
            ConnectionID = null;
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
        public static int EmployeeDocumentID { get; set; }
        public static string DocumentPassword { get; set; }
        public static string DocumentString { get; set; }

    }
    public static class EmployeePersonalDetails
    { 
        public static string FirstName { get; set; }
        public static string LastName { get; set; }
        public static int? GenderCodeID { get; set; } = -1;
        public static int GenderSpinnerPosition { get; set; } = -1;
        public static int ImmigrationCodeID { get; set; } = -1;
        public static int ImmigrationSpinnerPosition { get; set; } = -1;
        public static string Address { get; set; }
        public static int AddressID { get; set; }
        public static string ContactNumber { get; set; }
        public static string SSN { get; set; }
        public static string ImmigrationStatus { get; set; }


        public static void clearData()
        {
            FirstName = null;
            LastName = null;
            GenderCodeID = -1;
            GenderSpinnerPosition = -1;
            ImmigrationCodeID = -1;
            ImmigrationSpinnerPosition = -1;
            Address = null;
            AddressID = -1;
            ContactNumber = null;
            SSN = null;
            ImmigrationStatus = null;

        }
    }
    public static class EmployeeLoginDetails
    {
        public static string LoginID { get; set; }
        public static int AuthID { get; set; }
        public static int DetailID { get; set; }
        public static string WashRate { get; set; }
        public static string DateofHire { get; set; }
        public static int Status { get; set; } = -1;
        public static string Exemptions { get; set; }
        public static bool IsActive { get; set; } = true;
        public static void clearData()
        {
            LoginID = null;
            AuthID = -1;
            DetailID = -1;
            WashRate = null;
            DateofHire = null;
            Status = -1;
            Exemptions = null;
            IsActive = true;

        }

    }
}
