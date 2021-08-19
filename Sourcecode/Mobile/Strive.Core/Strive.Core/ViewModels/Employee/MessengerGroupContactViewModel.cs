using Acr.UserDialogs;
using Strive.Core.Models.Employee.Messenger;
using Strive.Core.Resources;
using Strive.Core.Utils.Employee;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Employee
{
    public class MessengerGroupContactViewModel : BaseViewModel
    {
        #region Properties

        public EmployeeList GroupList { get; set; }


        #endregion Properties

        #region Commands

        public async Task GetGroupsList()
        {
            _userDialog.ShowLoading(Strings.Loading, MaskType.Gradient);
            var recentContact = await MessengerService.GetRecentContacts(EmployeeTempData.EmployeeID);
            if (recentContact == null || recentContact.EmployeeList == null || recentContact.EmployeeList.ChatEmployeeList == null || recentContact.EmployeeList.ChatEmployeeList.Count == 0)
            {
                GroupList = null;
            }
            else
            {
                GroupList = new EmployeeList();
                GroupList.ChatEmployeeList = new List<ChatEmployeeList>();
                MessengerTempData.GroupLists = new EmployeeList();
                MessengerTempData.GroupLists.ChatEmployeeList = new List<ChatEmployeeList>();

                foreach (var group in recentContact.EmployeeList.ChatEmployeeList)
                {
                    if(group.IsGroup)
                    {
                        GroupList.ChatEmployeeList.Add(group);
                        MessengerTempData.GroupLists.ChatEmployeeList.Add(group);
                    }
                }
            }
            _userDialog.HideLoading();
        }

        public void navigateToChat()
        {
            _navigationService.Navigate<MessengerPersonalChatViewModel>();
        }
        #endregion Commands
    }
}
