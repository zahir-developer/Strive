
using Strive.Core.Models.Employee.Messenger;
using Strive.Core.Models.Employee.Messenger.MessengerGroups;
using Strive.Core.Utils;
using Strive.Core.Utils.Employee;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Employee
{
    public class MessengerViewParticipantsViewModel : BaseViewModel
    {

        #region Properties

        public EmployeeLists EmployeeList { get; set; }
        public CreateGroupChat groupChatInfo { get; set; }
        public bool Confirm { get; set; }
        public string GroupName { get; set; }
        public int? GroupUserId { get; set; }

        #endregion Properties


        #region Commands

        public async Task GetParticipants()
        {
            var result = await MessengerService.GetParticipants(MessengerTempData.GroupID);
            if(result == null)
            {
                EmployeeList = null;
            }
            else
            {
                EmployeeList = new EmployeeLists();
                EmployeeList.EmployeeList = new EmployeeList();
                EmployeeList.EmployeeList.ChatEmployeeList = new List<ChatEmployeeList>();
                EmployeeList = result;

                MessengerTempData.ExistingParticipants = new Models.Employee.Messenger.MessengerContacts.Contacts.EmployeeMessengerContacts();
                MessengerTempData.ExistingParticipants.EmployeeList = new Models.Employee.Messenger.MessengerContacts.Contacts.EmployeeList();
                //MessengerTempData.ExistingParticipants.EmployeeList.Employee = ;
            }
        }
        public async Task UpdateGroup()
        {
            groupChatInfo = new CreateGroupChat();
            groupChatInfo.chatGroup = null;
            groupChatInfo.chatUserGroup = new List<chatUserGroup>();

            foreach (var data in MessengerTempData.SelectedParticipants.EmployeeList.Employee)
            {
                var results = MessengerTempData.ExistingParticipants.EmployeeList.Employee.Find(x => x.EmployeeId == data.EmployeeId);
                var participants = new chatUserGroup()
                {
                    chatGroupId = MessengerTempData.GroupID,
                    chatGroupUserId = 0,
                    CommunicationId = null,
                    createdBy = EmployeeTempData.EmployeeID,
                    createdDate = DateUtils.ConvertDateTimeWithZ(),
                    isActive = true,
                    isDeleted = false,//results == null ? true : false,
                    userId = data.EmployeeId,
                };
                groupChatInfo.chatUserGroup.Add(participants);  
            }
            groupChatInfo.GroupId = null;

            GroupChatResponse groupChatResponse = new GroupChatResponse();
            groupChatResponse.Result = new Result();
            groupChatResponse = await MessengerService.CreateChatGroup(groupChatInfo);
            if (groupChatResponse == null)
            {
                _userDialog.Toast("Group chat not created");
            }
            else
            {
                _userDialog.Toast("User added successfully");
            }
        }

        public void NoParticipants()
        {
            _userDialog.Alert("There are no participants to add");
        }

        public async Task DeleteGroupUser()
        {

            Confirm = await _userDialog.ConfirmAsync("Are you sure you want to delete this user from Group?");
            if(Confirm)
            {
                var result = await MessengerService.DeleteGroupUser(GroupUserId);
                if (result.ChatGroupUserDelete)
                {
                    _userDialog.Toast("User has been removed");
                }
            }
        }
        public void AddCreatingUser()
        {
            var creatingUser = new chatUserGroup()
            {
                chatGroupId = 0,
                chatGroupUserId = 0,
                CommunicationId = null,
                createdBy = EmployeeTempData.EmployeeID,
                createdDate = DateUtils.ConvertDateTimeWithZ(),
                isActive = true,
                isDeleted = false,
                userId = EmployeeTempData.EmployeeID,
            };
            groupChatInfo.chatUserGroup.Add(creatingUser);
        }



        #endregion Commands

    }
}
