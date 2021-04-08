﻿using Strive.Core.Models.Employee.Messenger.MessengerContacts;
using Strive.Core.Resources;
using Strive.Core.Utils.Employee;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmployeeList = Strive.Core.Models.Employee.Messenger.MessengerContacts.Contacts.EmployeeList;
using EmployeesList = Strive.Core.Models.Employee.Messenger.MessengerContacts.EmployeeLists;



namespace Strive.Core.ViewModels.Employee
{
    public class MessengerCreateGroupViewModel : BaseViewModel
    {
        #region Properties
        public EmployeeList EmployeeLists { get; set; }

        #endregion Properties


        #region Commands

        public async Task GetContactsList()
        {
            _userDialog.ShowLoading(Strings.Loading);
            var contactList = await MessengerService.GetContacts(new GetAllEmployeeDetail_Request
            {
                startDate = null,
                endDate = null,
                locationId = null,
                pageNo = null,
                pageSize = null,
                query = "",
                sortOrder = null,
                sortBy = null,
                status = true,
            });
            if (contactList == null)
            {
                EmployeeLists = null;
            }
            else
            {
                EmployeeLists = new EmployeeList();
                EmployeeLists.Employee = new List<Models.Employee.Messenger.MessengerContacts.Contacts.Employee>();
                MessengerTempData.employeeList_Contact = new Models.Employee.Messenger.MessengerContacts.Contacts.EmployeeList();
                MessengerTempData.employeeList_Contact.Employee = new List<Models.Employee.Messenger.MessengerContacts.Contacts.Employee>();
                EmployeeLists = contactList;
                MessengerTempData.employeeList_Contact = contactList;
                MessengerTempData.ChatParticipants = new Dictionary<int, int>();

                if (MessengerTempData.SelectedParticipants == null)
                {
                    MessengerTempData.SelectedParticipants = new EmployeeLists();
                    MessengerTempData.SelectedParticipants.EmployeeList = new List<Strive.Core.Models.Employee.Messenger.MessengerContacts.EmployeeList>();
                }
               
            }
            _userDialog.HideLoading();
        }

        public void NotEnough()
        {
            _userDialog.Alert("Please add participants to the group");
        }

        #endregion Commands
    }
}
