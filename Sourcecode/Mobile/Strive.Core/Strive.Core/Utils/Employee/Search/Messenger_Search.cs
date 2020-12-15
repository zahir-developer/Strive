using Strive.Core.Models.Employee.Messenger;
using Strive.Core.Models.Employee.Messenger.MessengerContacts;
using System;
using System.Collections.Generic;
using System.Text;
using EmployeeList = Strive.Core.Models.Employee.Messenger.MessengerContacts.EmployeeList;

namespace Strive.Core.Utils.Employee.Search
{
    public class Messenger_Search
    {

        #region Commands

        public List<EmployeeList> SearchContact(List<EmployeeList> employeeLists, string query )
        {
            var contacts = employeeLists;
            List<EmployeeList> filteredContacts = new List<EmployeeList>();

            foreach(var data in contacts)
            {
                var FirstName = data.FirstName;
                var LastName = data.LastName;
                var completeNameArray = query.ToCharArray();
                var CapLetter = completeNameArray[0].ToString().ToUpper();
                var LowLetter = completeNameArray[0].ToString().ToLower();

                var CapNameCharArray = query.ToCharArray();
                var LowNameCharArray = query.ToCharArray();

                CapNameCharArray[0] = CapLetter[0];
                LowNameCharArray[0] = LowLetter[0];

                var CapNameCharString = new string(CapNameCharArray);
                var LowNameCharString = new string(LowNameCharArray);

                if (FirstName.Contains(query) || LastName.Contains(query)) 
                {
                    filteredContacts.Add(data);
                }
                if(FirstName.Contains(CapNameCharString) || LastName.Contains(CapNameCharString))
                {
                    filteredContacts.Add(data);
                }
                if(FirstName.Contains(LowNameCharString) || LastName.Contains(LowNameCharString))
                {
                    filteredContacts.Add(data);
                }
            }
            return filteredContacts;
        }

        public List<ChatEmployeeList> SearchGroup(List<ChatEmployeeList> groupsLists, string query)
        {
            var groups = groupsLists;
            List<ChatEmployeeList> filteredGroups = new List<ChatEmployeeList>();

            foreach(var data in groups)
            {
                var FirstName = data.FirstName;
                var LastName = data.LastName;
                var completeNameArray = query.ToCharArray();
                var CapLetter = completeNameArray[0].ToString().ToUpper();
                var LowLetter = completeNameArray[0].ToString().ToLower();

                var CapNameCharArray = query.ToCharArray();
                var LowNameCharArray = query.ToCharArray();

                CapNameCharArray[0] = CapLetter[0];
                LowNameCharArray[0] = LowLetter[0];

                var CapNameCharString = new string(CapNameCharArray);
                var LowNameCharString = new string(LowNameCharArray);

                if (FirstName.Contains(query) || LastName.Contains(query))
                {
                    filteredGroups.Add(data);
                }
                if (FirstName.Contains(CapNameCharString) || LastName.Contains(CapNameCharString))
                {
                    filteredGroups.Add(data);
                }
                if (FirstName.Contains(LowNameCharString) || LastName.Contains(LowNameCharString))
                {
                    filteredGroups.Add(data);
                }
            }
            return filteredGroups;
        }

        #endregion Commands


    }
}
