using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Strive.Core.Models.Employee.Messenger;
using Strive.Core.Models.Employee.Messenger.MessengerContacts;
using EmployeeList = Strive.Core.Models.Employee.Messenger.MessengerContacts.EmployeeList;
using EmployeeLists = Strive.Core.Models.Employee.Messenger.MessengerContacts.EmployeeList;

namespace StriveOwner.Android.Adapter
{
    public class MessengerSearchAdapter
    {
        private List<ChatEmployeeList> recentContacts = new List<ChatEmployeeList>();
        private List<ChatEmployeeList> sortedRecentContacts { get; set; }
        public List<EmployeeList> sortedContacts { get; set; }
        private string queryString { get; set; }

        public MessengerSearchAdapter() 
        {
           

        }
    
        public List<ChatEmployeeList> SearchRecentContacts(List<ChatEmployeeList> recentContacts, string queryString)
        {
            sortedRecentContacts = new List<ChatEmployeeList>();
            this.queryString = queryString;

            var AllSmall = queryString.ToLower();

            foreach(var data in recentContacts)
            {
                var firstName = data.FirstName.ToLower();
                var lastName = data.LastName.ToLower();
                if(firstName.Contains(AllSmall) || lastName.Contains(AllSmall))
                {
                    sortedRecentContacts.Add(data);
                }
            }
            return sortedRecentContacts;
        }
        public List<EmployeeList> SearchContacts(List<Strive.Core.Models.Employee.Messenger.MessengerContacts.Contacts.Employee> contacts, string queryString)
        {
            sortedContacts = new List<EmployeeList>();
            this.queryString = queryString;

            var AllSmall = queryString.ToLower();

            foreach(var data in contacts)
            {
                var firstName = data.FirstName.ToLower();
                var lastName = data.LastName.ToLower();
                if (firstName.Contains(AllSmall) || lastName.Contains(AllSmall))
                {
                   // sortedContacts.Add(data);
                }
            }

            return sortedContacts;

        }

    }
}