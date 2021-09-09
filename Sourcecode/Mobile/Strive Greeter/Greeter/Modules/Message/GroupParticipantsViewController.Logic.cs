using System.Collections.Generic;
using System.Threading.Tasks;
using Foundation;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Extensions;

namespace Greeter.Modules.Message
{
    public partial class GroupParticipantsViewController
    {
        List<ChatUserGroup> participants = new();
        List<ChatUserGroup> newlyAddedParticipants = new();

        readonly bool isCreateGroup;
        readonly long groupId;
        readonly string communicationId;
        readonly string groupName;

        public GroupParticipantsViewController(bool isCreateGroup, string communicationId = null, long groupId = -1, string groupName = null)
        {
            this.isCreateGroup = isCreateGroup;
            this.groupId = groupId;
            this.communicationId = communicationId;
            this.groupName = groupName;

            if(!isCreateGroup)
                _ = GetParticipants();
        }

        async Task GetParticipants()
        {
            ShowActivityIndicator();
            var result = await SingleTon.MessageApiService.GetGroupUsers(groupId);
            HideActivityIndicator();

            HandleResponse(result);

            if (!result.IsSuccess()) return;

            if (result.GroupUserObject?.Users is not null)
            {
                foreach(var item in result.GroupUserObject.Users)
                participants.Add(new ChatUserGroup
                {
                    UserID = item.ID,
                    ChatGroupUserID = item.ChatGroupUserId,
                    FirstName = item.FirstName,
                    LastName = item.LastName
                });

                ReloadParticipantTableView();
            }
        }

        public async void RemoveParticipantInApiAsync(ContactEmployee contact)
        {
            ShowActivityIndicator();
            var index = participants.FindIndex(obj => obj.UserID == contact.EmployeeId);
            var result = await SingleTon.MessageApiService.RemoveUserFromGroup(participants[index].ChatGroupUserID);
            HideActivityIndicator();

            HandleResponse(result);

            if (!result.IsSuccess()) return;

            RemoveUserObj(contact);

            ShowAlertMsg(Common.Messages.USER_REMOVED_SUCCESS_MSG, null, false, Common.Messages.REMOVE_USER_FROM_GROUP_TITLE);
        }

        void RemoveUserObj(ContactEmployee contact)
        {
            var index = participants.FindIndex(obj => obj.UserID == contact.EmployeeId);

            var newParticipantIndex = newlyAddedParticipants.FindIndex(obj => obj.UserID == contact.EmployeeId);
            if (newParticipantIndex != -1)
                newlyAddedParticipants.RemoveAt(index);

            participants.RemoveAt(index);

            ReloadParticipantTableView();
        }

        public void ContactSelectionDidCompleted(List<ContactEmployee> contacts)
        {
            //var participants = contacts.ConvertAll(obj => new ChatUserGroup { UserID = obj.EmployeeId });
            //participants.AddRange(participants);
            foreach(var contact in contacts)
            {
                var participant = new ChatUserGroup { UserID = contact.EmployeeId, FirstName = contact.FirstName, LastName = contact.LastName, CommunicationId = contact.CommunicationId };
                participants.Add(participant);
                newlyAddedParticipants.Add(participant);
            }
            ReloadParticipantTableView();
        }

        async void SaveParticipant()
        {
            ShowActivityIndicator();
            List<Task> TaskList = new List<Task>();

            var creategroupReq = new CreategroupRequest();
            creategroupReq.ChatGroup = null;
            creategroupReq.ChatUserGroup = new List<ChatUserGroup>();
            creategroupReq.GroupID = communicationId;

            if (newlyAddedParticipants.Count == 0)
            {
                HideActivityIndicator();
                ShowAlertMsg("Please add some users to the group and then click save");
                return;
            }

            foreach (var participant in newlyAddedParticipants)
            {
                var chatUserGroup = new ChatUserGroup();
                chatUserGroup.UserID = participant.UserID;
                chatUserGroup.ChatGroupId = groupId;
                creategroupReq.ChatUserGroup.Add(chatUserGroup);
            }

            var response = await SingleTon.MessageApiService.CreateGroup(creategroupReq);

            HideActivityIndicator();

            HandleResponse(response);

            if (!response.IsSuccess()) return;

            ShowAlertMsg(Common.Messages.USER_ADDED_SUCCESS_MSG, () => {
                NavigationController.PopViewController(true);
            }, false, Common.Messages.ADD_USER_FROM_TO_GROUP_TITLE);
        }

        async Task OnCreateGroup(string groupName)
        {
            if (!IsValidData(groupName)) return;

            ShowActivityIndicator();

            var req = new CreategroupRequest
            {
                ChatGroup = new ChatGroup { GroupName = groupName, CreatedBy = AppSettings.UserID, ChatGroupId = 0},
                ChatUserGroup = participants
            };

            var mineParticipant = new ChatUserGroup { UserID = AppSettings.UserID, FirstName = AppSettings.FirstName, LastName = AppSettings.LastName };
            req.ChatUserGroup.Add(mineParticipant);

            var result = await SingleTon.MessageApiService.CreateGroup(req);

            HideActivityIndicator();

            HandleResponse(result);

            if (!result.IsSuccess()) return;

            NSNotificationCenter.DefaultCenter.PostNotificationName(new NSString(GroupViewController.UPDATE_GROUPS_KEY), null);

            ShowAlertMsg(Common.Messages.GROUP_CREATED_MSG, () =>
            {
                NavigationController.PopViewController(true);
            }, false, Common.Messages.GROUP_TITLE);
        }

        bool IsValidData(string groupName)
        {
            var isValid = false;
            if (string.IsNullOrEmpty(groupName) || string.IsNullOrWhiteSpace(groupName))
            {
                ShowAlertMsg(Common.Messages.GROUP_NAME_EMPTY);
            }
            else if (participants.Count == 0)
            {
                ShowAlertMsg(Common.Messages.NO_GROUP_PARTICIPANTS);
            }
            else isValid = true;
            return isValid;
        }
    }
}