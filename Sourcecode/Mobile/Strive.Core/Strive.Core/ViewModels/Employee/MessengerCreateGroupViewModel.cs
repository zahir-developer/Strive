using Strive.Core.Models.Employee.Messenger.MessengerContacts;
using Strive.Core.Resources;
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
            _userDialog.ShowLoading(Strings.Loading);
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

                if (MessengerTempData.SelectedParticipants == null)
                {
                    MessengerTempData.SelectedParticipants = new EmployeeLists();
                    MessengerTempData.SelectedParticipants.EmployeeList = new List<EmployeeList>();
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
