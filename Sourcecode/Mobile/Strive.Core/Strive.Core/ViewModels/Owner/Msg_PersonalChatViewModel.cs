using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCross.ViewModels;
using Strive.Core.Models.Employee.Messenger;
using Strive.Core.Models.Employee.Messenger.PersonalChat;
using Strive.Core.Utils;
using Strive.Core.Utils.Employee;

namespace Strive.Core.ViewModels.Owner
{
    public class Msg_PersonalChatViewModel : BaseViewModel
    {
        public List<string> Chats = new List<string>();

        public Msg_PersonalChatViewModel()
        {
            //_ = GetChatsAsync();
        }

        async Task GetChatsAsync()
        {
            //TODO get real time data from server and add here
            Chats.Add("");
            Chats.Add("");
            Chats.Add("");
            Chats.Add("");
            Chats.Add("");
            //ReloadChatTableView();
            await Task.CompletedTask;
        }

        #region Properties

        public string Message { get; set; }

        public bool SentSuccess { get; set; }
        public PersonalChatMessages personalChatMessages { get; set; }
        public SendChatMessage sendChat { get; set; }
        public MvxObservableCollection<ChatMessageDetail> ChatMessages = new MvxObservableCollection<ChatMessageDetail>();


        ChatCommunication Chat = new ChatCommunication();
        #endregion Properties

        #region Commands

        public async Task GetAllMessages(ChatDataRequest chatData)
        {
            var result = await MessengerService.GetPersonalChatMessages(chatData);
            //if (result == null || result.ChatMessage == null || result.ChatMessage.ChatMessageDetail == null || result.ChatMessage.ChatMessageDetail.Count == 0)
            //{
            //    chatMessages = null;
            //}
            //else
            //{ }
            personalChatMessages = new PersonalChatMessages();
            personalChatMessages.ChatMessage = new ChatMessage();
            personalChatMessages.ChatMessage.ChatMessageDetail = new MvxObservableCollection<ChatMessageDetail>();
            personalChatMessages = result;
            ChatMessages = personalChatMessages.ChatMessage.ChatMessageDetail;

        }

        public async Task SendMessage()
        {
            if (!CheckEmptyChat())
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

        public void EmptyChatMessageError()
        {
            _userDialog.Toast("Enter a message");
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
            sendChat.chatMessage.messagebody = Message;
            sendChat.chatMessage.parentChatMessageId = null;
            sendChat.chatMessage.expiryDate = null;
            sendChat.chatMessage.isReminder = true;
            sendChat.chatMessage.nextRemindDate = null;
            sendChat.chatMessage.reminderFrequencyId = null;
            sendChat.chatMessage.createdBy = 0;
            sendChat.chatMessage.createdDate = DateUtils.ConvertDateTimeWithZ();
            sendChat.chatMessageRecipient.chatRecipientId = 0;
            sendChat.chatMessageRecipient.chatMessageId = 0;
            sendChat.chatMessageRecipient.senderId = EmployeeTempData.EmployeeID;

            if (MessengerTempData.IsGroup)
            {
                sendChat.chatMessageRecipient.recipientId = null;
                sendChat.firstName = MessengerTempData.RecipientName;
                sendChat.chatMessageRecipient.recipientGroupId = MessengerTempData.GroupID;
                sendChat.groupId = MessengerTempData.GroupUniqueID;
                sendChat.connectionId = null;
                sendChat.fullName = MessengerTempData.FirstName;
            }
            else
            {
                sendChat.chatMessageRecipient.recipientId = MessengerTempData.RecipientID;
                sendChat.firstName = MessengerTempData.RecipientName;
                if (MessengerTempData.RecipientsConnectionID != null)
                {
                    if (MessengerTempData.RecipientsConnectionID.ContainsKey(MessengerTempData.RecipientID.ToString()))
                    {
                        sendChat.connectionId = MessengerTempData.RecipientsConnectionID[MessengerTempData.RecipientID.ToString()];
                    }

                }
                else
                {
                    sendChat.connectionId = MessengerTempData.ConnectionID;
                }

                sendChat.chatMessageRecipient.recipientGroupId = null;
                sendChat.groupId = null;
                sendChat.fullName = MessengerTempData.FirstName;
            }
        }
        #endregion Commands
    }
}
