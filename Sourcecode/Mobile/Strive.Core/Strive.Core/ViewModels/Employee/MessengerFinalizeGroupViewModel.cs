using Acr.UserDialogs;
using Strive.Core.Models.Employee.Messenger.MessengerContacts;
using Strive.Core.Models.Employee.Messenger.MessengerGroups;
using Strive.Core.Models.Employee.Messenger.PersonalChat;
using Strive.Core.Resources;
using Strive.Core.Utils;
using Strive.Core.Utils.Employee;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmployeeList = Strive.Core.Models.Employee.Messenger.MessengerContacts.Contacts.EmployeeMessengerContacts;

namespace Strive.Core.ViewModels.Employee
{
    public class MessengerFinalizeGroupViewModel : BaseViewModel
    {

        #region Properties
        public string Message { get; set; }
        public string chatgroupsID { get; set; }
        public int groupsID { get; set; }
        public bool SentSuccess { get; set; }
        public PersonalChatMessages chatMessages { get; set; }
        public SendChatMessage sendChat { get; set; }
        public CreateGroupChat groupChatInfo { get; set; }
        public string GroupName { get; set; }
        public EmployeeList EmployeeLists { get; set; }

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

            foreach(var data in MessengerTempData.SelectedParticipants.EmployeeList.Employee)
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

            _userDialog.ShowLoading(Strings.Loading, MaskType.Gradient);

            GroupChatResponse groupChatResponse = new GroupChatResponse();
            groupChatResponse.Result = new Result();
            groupChatResponse = await MessengerService.CreateChatGroup(groupChatInfo);
            if(groupChatResponse == null)
            {
                _userDialog.Toast("Group chat not created");
            }
            else
            {
                _userDialog.Toast("Group chat created successfully");
                chatgroupsID = groupChatResponse.Result.GroupId;
                groupsID = groupChatResponse.Result.ChatGroupId;

            }
            await SendMessage();
            await GetContactsList("%20");
            _userDialog.HideLoading();
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

        public async Task GetContactsList(string employeeName)
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
        }
        public void EmptyGroupName()
        {
            _userDialog.Alert("Please enter group name to save");
        }


        public async Task SendMessage()
        {
            if (true)
            {
                sendChat = new SendChatMessage();
                sendChat.chatMessage = new chatMessage();
                sendChat.chatMessageRecipient = new chatMessageRecipient();
                sendChat.chatGroupRecipient = null;
                FillChatDetails();
                var result = await MessengerService.SendChatMessage(sendChat);
                if (result == null || !result.Status)
                {
                    SentSuccess = false;
                    _userDialog.Toast("Message not sent");
                }
                else
                {
                    SentSuccess = result.Status;
                    //if(MessengerTempData.IsGroup)
                    //{
                    //   ChatHubMessagingService.SendMessageToGroup(sendChat);
                    //}
                }
            }
        }
        public bool CheckEmptyChat()
        {
            bool result = true;
            if (String.IsNullOrEmpty(Message))
            {
                _userDialog.Toast("Enter a message to send");
                return result;
            }
            return result = false;
        }

        public void FillChatDetails()
        {
            sendChat.chatMessage.chatMessageId = 0;
            sendChat.chatMessage.subject = null;
            sendChat.chatMessage.messagebody = "CheckMessage";
            sendChat.chatMessage.parentChatMessageId = null;
            sendChat.chatMessage.expiryDate = null;
            sendChat.chatMessage.isReminder = true;
            sendChat.chatMessage.nextRemindDate = null;
            sendChat.chatMessage.reminderFrequencyId = null;
            sendChat.chatMessage.createdBy = 0;
            sendChat.chatMessage.createdDate = DateUtils.ConvertDateTimeWithZ();

            sendChat.chatMessageRecipient.chatRecipientId = 0;
            sendChat.chatMessageRecipient.chatMessageId = 0;
            sendChat.chatMessageRecipient.isRead = false;
            sendChat.chatMessageRecipient.senderId = EmployeeTempData.EmployeeID;
            sendChat.chatMessageRecipient.createdDate = DateUtils.ConvertDateTimeWithZ();
            sendChat.chatMessageRecipient.recipientGroupId = groupsID;
            sendChat.chatMessageRecipient.recipientId = null;
            sendChat.firstName = MessengerTempData.RecipientName;
            sendChat.chatMessageRecipient.recipientGroupId = groupsID;
            sendChat.groupId = chatgroupsID;
            sendChat.connectionId = chatgroupsID;
            sendChat.groupName = null;
            sendChat.fullName = MessengerTempData.FirstName;

        }
        #endregion Commands
    }
}
