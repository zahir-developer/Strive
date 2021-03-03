using Acr.UserDialogs;
using Strive.Core.Models.Employee.Messenger.MessengerContacts;
using Strive.Core.Resources;
using Strive.Core.Utils.Employee;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Employee
{
    public class MessengerContactViewModel : BaseViewModel
    {
        #region Properties
        public EmployeeLists EmployeeLists { get; set; }
        
        #endregion Properties


        #region Commands

        public async Task GetContactsList(string employeeName)
        {
            _userDialog.ShowLoading(Strings.Loading, MaskType.Gradient);
            if (MessengerTempData.EmployeeLists == null)
            {
                var contactList = await MessengerService.GetContacts(employeeName);
                if(contactList == null || contactList.EmployeeList == null || contactList.EmployeeList.Count == 0)
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
                }
            }          
            _userDialog.HideLoading();
        }

        #endregion Commands
    }
}
