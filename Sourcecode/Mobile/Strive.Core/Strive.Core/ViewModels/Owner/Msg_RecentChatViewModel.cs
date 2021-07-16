using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Strive.Core.Models.Employee.Messenger;
using Strive.Core.Resources;
using Strive.Core.Utils.Employee;

namespace Strive.Core.ViewModels.Owner
{
    public class Msg_RecentChatViewModel : BaseViewModel
    {
        #region Properties

        public EmployeeList EmployeeList { get; set; }

        #endregion Properties

        #region Commands
        public async Task GetRecentContactsList()
        {
            _userDialog.ShowLoading(Strings.Loading, MaskType.Gradient);
            var recentContact = await MessengerService.GetRecentContacts(EmployeeTempData.EmployeeID);
            if (recentContact == null || recentContact.EmployeeList == null || recentContact.EmployeeList.ChatEmployeeList == null || recentContact.EmployeeList.ChatEmployeeList.Count == 0)
            {
                EmployeeList = null;
            }
            else
            {
                EmployeeList = new EmployeeList();
                EmployeeList.ChatEmployeeList = new List<ChatEmployeeList>();
                MessengerTempData.RecentEmployeeLists = new EmployeeList();
                MessengerTempData.RecentEmployeeLists.ChatEmployeeList = new List<ChatEmployeeList>();
                EmployeeList = recentContact.EmployeeList;
                MessengerTempData.RecentEmployeeLists = recentContact.EmployeeList;
            }
            _userDialog.HideLoading();
        }

        #endregion Commands
    }   
}
