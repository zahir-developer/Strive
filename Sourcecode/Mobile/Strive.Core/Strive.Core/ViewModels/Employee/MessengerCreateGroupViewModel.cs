using Strive.Core.Models.Employee.Messenger.MessengerContacts;
using Strive.Core.Utils.Employee;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Employee
{
    public class MessengerCreateGroupViewModel : BaseViewModel
    {
        #region Properties
        public EmployeeLists EmployeeLists { get; set; }

        #endregion Properties


        #region Commands

        public async Task GetContactsList()
        {
            var contactList = await MessengerService.GetContacts("%20");
            if (contactList == null)
            {
                EmployeeLists = null;
            }
            else
            {
                EmployeeLists = new EmployeeLists();
                EmployeeLists.EmployeeList = new List<EmployeeList>();
                MessengerTempData.EmployeeLists = new EmployeeLists();
                MessengerTempData.EmployeeLists.EmployeeList = new List<EmployeeList>();
                EmployeeLists = contactList;
                MessengerTempData.EmployeeLists = contactList;
                MessengerTempData.ChatParticipants = new Dictionary<int, int>();
                MessengerTempData.SelectedParticipants = new EmployeeLists();
                MessengerTempData.SelectedParticipants.EmployeeList = new List<EmployeeList>();
            }
        }

        #endregion Commands
    }
}
