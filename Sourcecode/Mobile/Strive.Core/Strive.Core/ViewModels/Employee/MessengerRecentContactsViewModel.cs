﻿using Strive.Core.Models.Employee;
using Strive.Core.Models.Employee.Messenger;
using Strive.Core.Utils.Employee;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Employee
{
    public class MessengerRecentContactsViewModel : BaseViewModel
    {

        #region Properties
        public EmployeeList EmployeeList { get; set; }


        #endregion Properties



        #region Commands

        public async Task GetRecentContactsList()
        {
            var recentContact = await MessengerService.GetRecentContacts(EmployeeTempData.EmployeeID);
            if(recentContact == null || recentContact.EmployeeList.ChatEmployeeList.Count == 0)
            {
                EmployeeList = null;
            }
            else
            {
                EmployeeList = new EmployeeList();
                EmployeeList.ChatEmployeeList = new List<ChatEmployeeList>();
                EmployeeList = recentContact.EmployeeList;
            }
        }

        #endregion Commands

    }
}
