using System.Collections.Generic;
using System.Threading.Tasks;
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

        public GroupParticipantsViewController(bool isCreateGroup, long groupId = -1)
        {
            this.isCreateGroup = isCreateGroup;
            this.groupId = groupId;

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

        public async void RemoveParticipant(ContactEmployee contact)
        {
            ShowActivityIndicator();
            var index = participants.FindIndex(obj => obj.UserID == contact.EmployeeId);
            var result = await SingleTon.MessageApiService.RemoveUserFromGroup(participants[index].ChatGroupUserID);
            HideActivityIndicator();

            HandleResponse(result);

            if (!result.IsSuccess()) return;

            participants.RemoveAt(index);

            var newParticipantIndex = newlyAddedParticipants.FindIndex(obj => obj.UserID == contact.EmployeeId);
            if(newParticipantIndex != -1)
                newlyAddedParticipants.RemoveAt(index);

            ReloadParticipantTableView();
        }

        public void ContactSelectionDidCompleted(List<ContactEmployee> contacts)
        {
            //var participants = contacts.ConvertAll(obj => new ChatUserGroup { UserID = obj.EmployeeId });
            //participants.AddRange(participants);
            foreach(var contact in contacts)
            {
                var participant = new ChatUserGroup { UserID = contact.EmployeeId, FirstName = contact.FirstName, LastName = contact.LastName };
                participants.Add(participant);
                newlyAddedParticipants.Add(participant);
            }
            ReloadParticipantTableView();
        }

        async void SaveParticipant()
        {
            ShowActivityIndicator();
            List<Task> TaskList = new List<Task>();
            foreach (var participant in newlyAddedParticipants)
            {
                var task = SingleTon.MessageApiService.AddUserToGroup(participant.UserID, null);
                TaskList.Add(task);
            }

            await Task.WhenAll(TaskList.ToArray());
            HideActivityIndicator();

            NavigationController.PopViewController(true);
        }

        async Task OnCreateGroup(string groupName)
        {
            if (!IsValidData(groupName)) return;

            ShowActivityIndicator();
            var result = await SingleTon.MessageApiService.CreateGroup(
                new CreategroupRequest
                {
                    ChatGroup = new ChatGroup { GroupName = groupName, CreatedBy = AppSettings.UserID },
                    ChatUserGroup = participants
                }
            );
            HideActivityIndicator();

            HandleResponse(result);

            if (!result.IsSuccess()) return;

            ShowAlertMsg(Common.Messages.GROUP_CREATED_MSG, () =>
            {
                NavigationController.PopViewController(true);
            });
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