using Strive.Core.Models.Employee.Messenger.MessengerGroups;
using Strive.Core.Utils;
using Strive.Core.Utils.Employee;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Employee
{
    public class MessengerFinalizeGroupViewModel : BaseViewModel
    {

        #region Properties

        public CreateGroupChat groupChatInfo { get; set; }
        public string GroupName { get; set; }

        #endregion Properties

        #region Commands

        public async Task CreateGroup()
        {
            groupChatInfo = new CreateGroupChat();
            groupChatInfo.chatGroup = new chatGroup()
            {
                chatGroupId = 0,
                groupId = null,
                groupName = GroupName,
                comments = null,
                isActive = true,
                isDeleted = false,
                createdBy = EmployeeTempData.EmployeeID,
                createdDate = DateUtils.ConvertDateTimeWithZ(),
                updatedBy = EmployeeTempData.EmployeeID,
                updatedDate = DateUtils.ConvertDateTimeWithZ()
            };
            groupChatInfo.chatUserGroup = new List<chatUserGroup>();
            AddCreatingUser();

            foreach(var data in MessengerTempData.SelectedParticipants.EmployeeList)
            {
                var participants = new chatUserGroup()
                {
                    chatGroupId = 0,
                    chatGroupUserId = 0,
                    communicationId = null,
                    createdBy = EmployeeTempData.EmployeeID,
                    createdDate = DateUtils.ConvertDateTimeWithZ(),
                    isActive = true,
                    isDeleted = false,
                    userId = data.EmployeeId,
                };
                groupChatInfo.chatUserGroup.Add(participants);
            }
            groupChatInfo.groupId = null;

            GroupChatResponse groupChatResponse = new GroupChatResponse();
            groupChatResponse.Result = new Result();
            groupChatResponse = await MessengerService.CreateChatGroup(groupChatInfo);
            if(groupChatResponse == null)
            {

            }
            else
            {

            }
        }
        public void AddCreatingUser()
        {
            var creatingUser = new chatUserGroup()
            { 
                chatGroupId = 0,
                chatGroupUserId = 0,
                communicationId = null,
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
