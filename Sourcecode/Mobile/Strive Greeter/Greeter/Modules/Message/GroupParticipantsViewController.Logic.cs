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
                    FirstName = item.FirstName,
                    LastName = item.LastName
                });

                ReloadParticipantTableView();
            }
        }

        public async void RemoveParticipant(ContactEmployee contact)
        {
            ShowActivityIndicator();
            var result = await SingleTon.MessageApiService.RemoveUserFromGroup(contact.EmployeeId);
            HideActivityIndicator();

            HandleResponse(result);

            if (!result.IsSuccess()) return;

            var index = participants.FindIndex(obj => obj.UserID == contact.EmployeeId);
            participants.RemoveAt(index);
            ReloadParticipantTableView();
        }

        public void ContactSelectionDidCompleted(List<ContactEmployee> contacts)
        {
            //var participants = contacts.ConvertAll(obj => new ChatUserGroup { UserID = obj.EmployeeId });
            //participants.AddRange(participants);
            foreach(var contact in contacts)
            {
                participants.Add(new ChatUserGroup { UserID = contact.EmployeeId, FirstName = contact.FirstName, LastName = contact.LastName });
            }
            ReloadParticipantTableView();
        }

        async void SaveParticipant()
        {
        }

        async Task OnCreateGroup(string groupName)
        {
            if (!IsValidData(groupName)) return;

            ShowActivityIndicator();
            var result = await SingleTon.MessageApiService.CreateGroup(
                new CreategroupRequest
                {
                    ChatGroup = new ChatGroup { GroupName = groupName },
                    ChatUserGroup = participants
                }
            );
            HideActivityIndicator();

            HandleResponse(result);

            if (!result.IsSuccess()) return;

            NavigationController.PopViewController(true);
        }

        bool IsValidData(string groupName)
        {
            var isValid = false;
            if (string.IsNullOrEmpty(groupName) || string.IsNullOrWhiteSpace(groupName))
            {
                //TODO show message 
            }
            else if (participants.Count == 0)
            {
                //TODO show message 
            }
            else isValid = true;
            return isValid;
        }
    }
}