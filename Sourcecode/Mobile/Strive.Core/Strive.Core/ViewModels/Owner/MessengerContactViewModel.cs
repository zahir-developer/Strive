using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Strive.Core.Models.Employee.Messenger.MessengerContacts;
using Strive.Core.Resources;
using Strive.Core.Utils.Employee;
using EmployeeList = Strive.Core.Models.Employee.Messenger.MessengerContacts.Contacts.EmployeeMessengerContacts;

namespace Strive.Core.ViewModels.Owner
{
    public class MessengerContactViewModel : BaseViewModel
    {
        #region Properties

        public EmployeeList EmployeeLists { get; set; }

        #endregion Properties

        #region Commands
        public MessengerContactViewModel()
        {
        }

        public async Task<EmployeeList> GetContactsList()
        {
            _userDialog.ShowLoading(Strings.Loading, MaskType.Gradient);
            if (MessengerTempData.EmployeeLists == null)
            {
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
                if (contactList == null || contactList.EmployeeList == null || contactList.EmployeeList.Employee.Count == 0)
                {
                    EmployeeLists = null;
                }
                else
                {
                    EmployeeLists = new EmployeeList();
                    EmployeeLists.EmployeeList = new Models.Employee.Messenger.MessengerContacts.Contacts.EmployeeList();
                    EmployeeLists.EmployeeList.Employee = new List<Models.Employee.Messenger.MessengerContacts.Contacts.Employee>();
                    MessengerTempData.employeeList_Contact = new EmployeeList();
                    MessengerTempData.employeeList_Contact.EmployeeList = new Models.Employee.Messenger.MessengerContacts.Contacts.EmployeeList();
                    MessengerTempData.employeeList_Contact.EmployeeList.Employee = new List<Models.Employee.Messenger.MessengerContacts.Contacts.Employee>();
                    EmployeeLists = contactList;
                    MessengerTempData.employeeList_Contact = contactList;
                }

            }
            _userDialog.HideLoading();
            return EmployeeLists;
        }

        public void navigateToChat()
        {
            _navigationService.Navigate<Msg_PersonalChatViewModel>();
        }
        #endregion Commands
    }
}
