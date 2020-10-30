using Strive.Core.Models.Employee.Messenger.MessengerContacts;
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

        public async Task GetContactsList()
        {
           

            var contactList = await MessengerService.GetContacts("%20");
            if(contactList == null)
            {
                EmployeeLists = null;
            }
            else
            {
                EmployeeLists = new EmployeeLists();
                EmployeeLists.EmployeeList = new List<EmployeeList>();
                EmployeeLists = contactList;
            }
        }

        #endregion Commands
    }
}
